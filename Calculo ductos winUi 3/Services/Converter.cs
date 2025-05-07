using Calculo_ductos_winUi_3.Models;
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
                            NeedChimmey = floorDescription.NeedChimney
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
                                NeedChimmey = floorDescription.NeedChimney
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
    }
}
