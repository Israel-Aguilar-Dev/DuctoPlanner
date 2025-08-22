using Calculo_ductos.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public class ComponentModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public Component.TypeComponent Type { get; set; }
    }
}
