using Calculo_ductos_winUi_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculo_ductos.Params;
using DocumentFormat.OpenXml.Bibliography;

namespace Calculo_ductos_winUi_3.Services
{
    public static class Mapper
    {
        public static ObservableCollection<DuctModel> MapDuctsFromDictionary(this Dictionary<Duct.TypeDuct, int> ducts)
        {
            return new ObservableCollection<DuctModel>(
                ducts.Select(kvp => new DuctModel
                {
                    Type = kvp.Key,
                    Count = kvp.Value
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
                        DuctType = duct.Key,
                        Count = duct.Value
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

        public static Dictionary<Duct.TypeDuct, int> MapDuctsToDictionary(this ObservableCollection<DuctModel> ducts)
        {
            return ducts
                .GroupBy(duct => duct.Type)
                .ToDictionary(group => group.Key, group => group.Sum(d => d.Count));
        }

    }
}
