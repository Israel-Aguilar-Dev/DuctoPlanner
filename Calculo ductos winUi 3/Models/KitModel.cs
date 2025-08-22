using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public class KitModel
    {
        public string Kit { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal InstalationTime { get; set; }
    }
    public class KitCollection
    {
        public List<KitModel> Ducts { get; set; } = new List<KitModel>();
        public List<KitModel> Guillotine { get; set; } = new List<KitModel>();
        public List<KitModel> Container { get; set; } = new List<KitModel>();
        public List<KitModel> General { get; set; } = new List<KitModel>();
        public List<KitModel> Clothes { get; set; } = new List<KitModel>();
    }
}
