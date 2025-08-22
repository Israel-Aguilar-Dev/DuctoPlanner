using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public enum ZoneType { A_zone,B_zone}
    
    public class FreightService
    {
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; } = false;
        public string ImagePath { get; set; } = string.Empty;
        public FreightCity FreightCity { get; set; }
    }
    public class City
    {
        public int CityCode { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public ZoneType ZoneType { get; set; }
        public string ZonTypeDescription {
            get {
                switch (ZoneType)
                {
                    case ZoneType.A_zone: return "Zona A";
                    case ZoneType.B_zone: return "Zona B";
                    default: return "Zona A";
                }
            }
        }
    }
    public class FreightCity
    {
        public City City { get; set; }
        public decimal Price { get; set; }
        public decimal EstimatedTimeArrival { get; set; }
    }
}
