using Calculo_ductos.Params;
using Calculo_ductos_winUi_3.Models;
using Calculo_ductos_winUi_3.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Services
{
    public static class Converter
    {
        public static string ToJsonString(this ObservableCollection<FloorDescription> source)
        {
            string json = string.Empty;
            List<object> floors = new List<object>();
            int counter = 0;
            try
            {
                foreach (FloorDescription floorDescription in source)
                {
                    if (floorDescription.FloorCount == 1)
                    {
                        floors.Add(new
                        {
                            Name = $"N{counter}",
                            Height = floorDescription.FloorHeight,
                            NeedGate = floorDescription.NeedGate,
                            Type = floorDescription.Type,
                            NeedChimmey = floorDescription.NeedChimney,
                            Discharge = floorDescription.Discharge
                        });
                        counter++;
                    }
                    else
                    {
                        for (int i = 1; i <= floorDescription.FloorCount; i++)
                        {
                            floors.Add(new
                            {
                                Name = $"N{counter}",
                                Height = floorDescription.FloorHeight,
                                NeedGate = floorDescription.NeedGate,
                                Type = floorDescription.Type,
                                NeedChimmey = floorDescription.NeedChimney,
                                Discharge = floorDescription.Discharge
                            });
                            counter++;
                        }
                    }
                }
                counter++;
                json = JsonConvert.SerializeObject(floors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return json;
            
            
        }
        public static string ToJsonString(this StateViewModel state)
        {
            string json = string.Empty;
            List<object> floors = new List<object>();
            int counter = 0;
            var firsFloor = state.FloorVM.FloorList.Where(f => f.Type == Floor.TypeFloor.discharge).FirstOrDefault();
            var lastFloor = state.FloorVM.FloorList.Where(f => f.Type == Floor.TypeFloor.last).FirstOrDefault();
            try
            {
                floors.Add(new
                {
                    Name = $"N{counter}",
                    Height = firsFloor.FloorHeight,
                    NeedGate = firsFloor.NeedGate,
                    NeedAntiImpact = firsFloor.NeedAntiImpact,
                    Type = firsFloor.Type,
                    NeedChimney = firsFloor.NeedChimney,
                    Discharge = firsFloor.Discharge
                });
                counter++;

                foreach (FloorDescription floorDescription in state.FloorVM.FloorList.Where(f => f.Type != firsFloor.Type && f.Type != lastFloor.Type).ToList())
                {
                    if (floorDescription.FloorCount == 1)
                    {
                        floors.Add(new
                        {
                            Name = $"N{counter}",
                            Height = floorDescription.FloorHeight,
                            NeedGate = floorDescription.NeedGate,
                            NeedAntiImpact = floorDescription.NeedAntiImpact,
                            Type = floorDescription.Type,
                            NeedChimney = floorDescription.NeedChimney,
                            Discharge = floorDescription.Discharge
                        });
                        counter++;
                    }
                    else
                    {
                        for (int i = 1; i <= floorDescription.FloorCount; i++)
                        {
                            floors.Add(new
                            {
                                Name = $"N{counter}",
                                Height = floorDescription.FloorHeight,
                                NeedGate = floorDescription.NeedGate,
                                NeedAntiImpact = floorDescription.NeedAntiImpact,
                                Type = floorDescription.Type,
                                NeedChimney = floorDescription.NeedChimney,
                                Discharge = floorDescription.Discharge
                            });
                            counter++;
                        }
                    }
                }
                floors.Add(new
                {
                    Name = $"N{counter}",
                    Height = lastFloor.FloorHeight,
                    NeedGate = lastFloor.NeedGate,
                    NeedAntiImpact = lastFloor.NeedAntiImpact,
                    Type = lastFloor.Type,
                    NeedChimney = lastFloor.NeedChimney,
                    Discharge = lastFloor.Discharge
                });
                //counter++;
                var duct = new {
                    Purpose = (Duct.TypePurpose)state.CompleteDuctVm.PurposeId,
                    NeedChimmey = lastFloor?.NeedChimney,
                    NeedSprinkler = state.CompleteDuctVm.NeedSprinkler,
                    NeedDesinfectionSystem = state.CompleteDuctVm.NeedDesinfectionSystem,
                    TipoLamina = state.CompleteDuctVm.SheetTypeId,
                    floors = floors

                };

                json = JsonConvert.SerializeObject(duct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return json;
        }
        public static ZoneType ToZoneType(this string description)
        {
            switch (description)
            {
                case "Zona A":return ZoneType.A_zone;break;
                case "Zona B":return ZoneType.B_zone;break;
                default : return ZoneType.A_zone;
            }
        }
    }
}
