﻿using CotizadorApiVertical.Data;
using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Config;
namespace CotizadorApiVertical.Services
{
    public class CatalogService : ICatalogFacade
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICatalogRepository _catalogRepository;
        public CatalogService() 
        {
            _catalogRepository = new CatalogRepository();
        }
        public async Task<Response> GetCatalogs()
        {
            Response response = new Response();
            try
            {
                log.Info("========== Dentro de GetCatalogs ==========");
                var purposeCatalog = _catalogRepository.GetPurposeCatalog();
                var doorTypeCatalog = _catalogRepository.GetDoorTypeCatalog();
                var sheetTypeCatalog = _catalogRepository.GetSheetTypeCatalog();
                var floorTypeCatalog = _catalogRepository.GetFloorTypeCatalog();
                var truckTypeCatalog = _catalogRepository.GetTruckTypeCatalog();
                var EntityCatalog = _catalogRepository.GetEntityCatalog();
                var MunicipalityCatalog = _catalogRepository.GetMunicipalityCatalog();
                var LocalityCatalog = _catalogRepository.GetLocalityCatalog();
                var Resources = _catalogRepository.GetResourceCatalog();
                var ResourceTypes = _catalogRepository.GetResourceTypeCatalog();
                var Rentabilities = _catalogRepository.GetRentabilitiesCatalog();
                CatalogModelList catalogs = new CatalogModelList
                {
                    PurposeCatalog = purposeCatalog.Select(p => new CatalogRowModel { Id = p.PropositoId, Description = p.Descripcion }).ToList(),
                    DoorTypeCatalog = doorTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoPuertaId, Description = p.Descripcion, Class = p.Clase, IdSyteLine = p.ItemIdSyteLine }).ToList(),
                    SheetTypeCatalog = sheetTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoLaminaId, Description = p.Descripcion }).ToList(),
                    FloorTypeCatalog = floorTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoNivelId, Description = p.Descripcion }).ToList(),
                    TruckTypeCatalog = truckTypeCatalog.Select(p => new CatalogRowTruckTypeModel { Id = p.TipoCamionId, Description = p.Descripcion, Name = p.Clase, MinCapacity = p.CapacidadMinima, MaxCapacity = p.CapacidadMaxima, HandlingCost = p.CostoManiobra}).ToList(),
                    Entities = EntityCatalog.Select(p => new CatalogRowEntityModel { Id = p.EntidadId, Name = p.Nombre}).ToList(),
                    Municipalities = MunicipalityCatalog.Select(p => new CatalogRowEntityModel { Id = p.MunicipioId, ParentId = p.EntidadId, Name = p.Nombre}).ToList(),
                    Localities = LocalityCatalog.Select(p => new CatalogRowEntityModel { Id = p.LocalidadId, ParentId = p.MunicipioId, Name = p.Nombre}).ToList(),
                    Resources = Resources.Select(p => new CatalogResourceModel { Id = p.RecursoId, Description = p.Descripcion, SalaryPerWorkday = p.SalarioPorJornada}).ToList(),
                    ResourceTypes = ResourceTypes.Select(p => new CatalogResourceTypeModel { Id = p.TipoRecursoId, Description = p.Descripcion}).ToList(),
                    Rentabilities = Rentabilities.Select(p => new CatalogRentabilityModel { Id = p.RentabilidadMOId, Rentability = p.Rentabilidad, Description = p.Descripcion}).ToList(),
                };
                response.Data = catalogs;
                response.StatusCode = 200;
                response.Message = "Éxito";
                log.Info("Se consulto con exito los catalogos");
            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = "Ocurrio un error al obtener los catalogos";
                log.Error(ex.Message);
            }
            return response;
        }
    }
}
