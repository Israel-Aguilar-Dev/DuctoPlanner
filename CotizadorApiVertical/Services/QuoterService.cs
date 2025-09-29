﻿using CotizadorApiVertical.Data;
using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using CotizadorVerticalApi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CotizadorVerticalApi.Services
{
    public class QuoterService : IQuoterFacade
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IQuoteRepository _quoteRepository;
        private readonly IFreightRepository _freightRepository;
        private readonly IManPowerRepository _manPowerRepository;

        public QuoterService() 
        {
            _quoteRepository = new QuoteRepository();
            _freightRepository = new FreightRepository();
            _manPowerRepository = new ManPowerRepository();
        }
        public async Task<Response> GetLastQuotes()
        {
            Response response = new Response();
            try
            {
                var quotes = _quoteRepository.GetLastQuotes().Select(q => new { PT = q.PT, Id = q.CotizacionId, Date = q.Fecha}).ToList();
                response.Data = quotes;
                response.StatusCode = 200;
                response.Message = "Éxito";
            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = "No se pudieron cargar las ultimas cotizaciones";

            }
            return response;
            
        }

        public async Task<Response> GetQuoteById(int cotizacionId)
        {
            Response response = new Response();
            try
            {
                var detail = _quoteRepository.GetQuoteById(cotizacionId);
                var manpowerQuery = _manPowerRepository.GetManPower(cotizacionId);
                List<HumanResource> manpower = new List<HumanResource>();

                if (manpowerQuery.Success) { manpower = (List<HumanResource>)manpowerQuery.Data; }
                else { log.Error(manpowerQuery.Message); }

                var quote = new
                {
                    CotizacionId = cotizacionId,
                    PT = detail.FirstOrDefault().PT,
                    Diametro = detail.FirstOrDefault().Diametro,
                    NombreEjecutivo = detail.FirstOrDefault().NombreEjecutivo,
                    TipoLaminaId = detail.FirstOrDefault().TipoLaminaId,
                    TipoLamina = detail.FirstOrDefault().TipoLamina,
                    PropositoId = detail.FirstOrDefault().PropositoId,
                    Proposito = detail.FirstOrDefault().Proposito,
                    NecesitaAspersor = detail.FirstOrDefault().NecesitaAspersor,
                    NecesitaSistemaDD = detail.FirstOrDefault().NecesitaSistemaDD,
                    NumeroVersion = detail.FirstOrDefault().NumeroVersion,
                    EntidadId = detail.FirstOrDefault().EntidadId,
                    MunicipioId = detail.FirstOrDefault().MunicipioId,
                    LocalidadId = detail.FirstOrDefault().LocalidadId,
                    RentabilidadMOId = detail.FirstOrDefault().RentabilidadMOId,
                    Niveles = detail.Select(d=> new {
                        TipoNivelId = d.TipoNivelId,
                        Cantidad = d.Cantidad,
                        Altura = d.Altura,
                        NecesitaPuerta = d.NecesitaPuerta,
                        NecesitaChimenea = d.NecesitaChimenea,
                        NecesitaAntiImpactos = d.NecesitaAntiImpacto,
                        TipoPuertaId = d.TipoPuertaId,
                        TipoDescargaId = d.TipoDescargaId
                    }).ToList(),
                    ManoDeObra = manpower

                };
                response.Data = quote;
                response.StatusCode = 200;
                response.Message = "Éxito";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "No se pudo obtener la cotizacion";

            }
            return response;
        }
        public async Task<Response> InsertQuote(QuoteParam quote)
        {
            log.Info("========== Dentro de InsertQuote ==========");
            var response = new Response();
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                log.Debug($"Conexión usada: {connectionString}");
                //log.Debug($"Coneccion usada: {connectionString}");
                using (var connection = new SqlConnection(connectionString))
                {
                    log.Debug("Intentando abrir la conexión...");
                    connection.Open();
                    log.Debug("Conexion abierta correctamente");
                    using (var transaction = connection.BeginTransaction())
                    {
                        log.Debug("Inicio de transacción");
                        try
                        {
                            var resultOperation = new ResultOperationModel();
                            log.Debug($"Quote: {JsonConvert.SerializeObject(quote)}");
                            var quoteResult = _quoteRepository.InsertQuote(connection, transaction, quote);
                            quote.CotizacionId = quoteResult.CotizacionId;
                            log.Debug($"Se guardo contización con exito id:{quote.CotizacionId}");

                            _quoteRepository.InsertLevels(connection, transaction, quote);
                            log.Debug($"Insert Levels ejecutado");

                            resultOperation = _freightRepository.InsertFreight(connection,transaction,quote.LocalidadId,quote.CotizacionId);
                            if (resultOperation.Success)
                                log.Debug($"InsertFreight ejecutado");
                            else 
                                log.Debug(resultOperation.Message);

                            resultOperation = _manPowerRepository.InsertManPower(connection,transaction,quote.ManoDeObra,quote.CotizacionId);
                            if(resultOperation.Success)
                                log.Debug($"InsertManPower ejecutado");
                            else
                                log.Error(resultOperation.Message);

                            transaction.Commit();

                            response.Data = new { Id = quoteResult.CotizacionId, Version = quoteResult.NumeroVersion };
                            response.StatusCode = 200;
                            response.Message = "Éxito";
                            log.Info("Se guardo con exito");
                            log.Debug($"Parametros: {JsonConvert.SerializeObject(response.Data)}");
                        }
                        catch (Exception ex)
                        {
                            var message = ex.Message;
                            log.Error($"No se pudo guardar la cotización. Error: {message}");
                            transaction.Rollback();
                            response.StatusCode = 500;
                            response.Message = "No se pudo guardar la cotización";
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                log.Error($"Ocurrio un error: {ex.Message}");
            }
            return response;
        }

       
    }
}
