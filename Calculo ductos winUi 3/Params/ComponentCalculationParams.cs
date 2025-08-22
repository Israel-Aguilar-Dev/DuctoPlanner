using Calculo_ductos.Params;
using Calculo_ductos_winUi_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Params
{
    public class ComponentCalculationParams
    {
        public ObservableCollection<FloorDescription> Floors { get; set; }
        public Dictionary<DuctPiece.TypeDuct, int> Ducts { get; set; }
    }
}
