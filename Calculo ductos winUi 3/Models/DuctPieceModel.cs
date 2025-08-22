using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculo_ductos.Params;

namespace Calculo_ductos_winUi_3.Models
{
    public class DuctModel
    {
        public DuctPiece.TypeDuct Type { get; set; }
        public int Count { get; set; }
        public string TypeString => GetType();
        public string GetType()
        {
            return Type.ToString();
        }

    }
}
