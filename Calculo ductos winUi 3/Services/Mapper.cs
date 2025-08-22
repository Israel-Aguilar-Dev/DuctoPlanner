using Calculo_ductos_winUi_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculo_ductos.Params;
using DocumentFormat.OpenXml.Bibliography;
using Calculo_ductos_winUi_3.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Calculo_ductos_winUi_3.Services
{
    public static class Mapper
    {
        public static ObservableCollection<DuctModel> MapDuctsFromList(this List<DuctPiece> ducts)
        {
            return new ObservableCollection<DuctModel>(
                ducts.Select(kvp => new DuctModel
                {
                    Type = kvp.Type,
                    Count = kvp.Count
                }));
        }
        public static ObservableCollection<FloorDuctDetailModel> MapToFloorDuctDetails(this List<Floor> floors)
        {
            var result = new ObservableCollection<FloorDuctDetailModel>();

            foreach (var floor in floors)
            {
                foreach (var duct in floor.Ducts)
                {
                    result.Add(new FloorDuctDetailModel
                    {
                        FloorName = floor.Name,
                        DuctType = duct.Type,
                        Count = duct.Count
                    });
                }
            }

            return result;
        }
        public static ObservableCollection<ComponentModel> MapComponents(this List<Component> components) 
        {
            return new ObservableCollection<ComponentModel>(
                components.Select(component => new ComponentModel
                {
                    Name = component.Name,
                    Count = component.Count,
                    Type = component.Type
                }));
        }
        public static Dictionary<DuctPiece.TypeDuct, int> MapDuctsToDictionary(this ObservableCollection<DuctModel> ducts)
        {
            return ducts
                .GroupBy(duct => duct.Type)
                .ToDictionary(group => group.Key, group => group.Sum(d => d.Count));
        }
        public static QuoteDetailModel MapStateAppToQuoteDetail(this StateViewModel stateApp)
        {
            QuoteDetailModel quote = new QuoteDetailModel();
            try
            {
                quote =
                    new QuoteDetailModel
                    {
                        CotizacionId = 0,
                        PT = stateApp.CompleteDuctVm.PT,
                        NombreEjecutivo = stateApp.CompleteDuctVm.ExecutiveName,
                        TipoLaminaId = stateApp.CompleteDuctVm.SheetTypeId,
                        PropositoId = stateApp.CompleteDuctVm.PurposeId,
                        Diametro = "24\"",
                        SiteRef ="VERS",
                        NecesitaAspersor = stateApp.CompleteDuctVm.NeedSprinkler,
                        NecesitaSistemaDD = stateApp.CompleteDuctVm.NeedDesinfectionSystem,
                        Niveles = stateApp.FloorVM.FloorList.Select(
                            floor => new FloorDetailModel
                            {
                                TipoNivelId = Convert.ToInt32(floor.Type),
                                Cantidad = floor.FloorCount,
                                Altura = floor.FloorHeight,
                                NecesitaPuerta = floor.NeedGate,
                                NecesitaChimenea = floor.NeedChimney,
                                NecesitaAntiImpactos = floor.NeedAntiImpact,
                                TipoPuertaId = floor.TypeDoor.Id,
                                TipoDescargaId = Convert.ToInt32(floor.Discharge)
                            }
                            ).ToList()
                    };
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
            return quote;
        }
        public static ObservableCollection<FloorDescription> MapQuoteDetailToFloorList(this QuoteDetailModel quote, FloorDescriptionViewModel floorVm)
        {
            ObservableCollection<FloorDescription> map = new ObservableCollection<FloorDescription>();
            var floorList = quote.Niveles.Select(floor =>
                new FloorDescription {
                    Uuid = Guid.NewGuid(),
                    Type = (Floor.TypeFloor)floor.TipoNivelId,
                    FloorCount = floor.Cantidad,
                    FloorHeight = floor.Altura,
                    NeedGate = floor.NecesitaPuerta,
                    NeedChimney = floor.NecesitaChimenea,
                    NeedAntiImpact = floor.NecesitaAntiImpactos,
                    TypeDoor = floorVm.AllDoorType.Where(type => type.Id == floor.TipoPuertaId).FirstOrDefault(),
                    Discharge = (Floor.TypeDischarge)floor.TipoDescargaId
                }
            ).ToList();

            foreach (var floor in floorList)
                map.Add(floor);

            return map;
        }
        

    }
}
