using Calculo_ductos.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public class FloorDuctDetailModel
    {
        public string FloorName { get; set; }
        public Duct.TypeDuct DuctType { get; set; }
        public int Count { get; set; }

    }
}
