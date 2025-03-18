using Calculo_ductos.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculo_ductos.Params.Floor;

namespace Calculo_ductos_winUi_3.Models
{
    public class FloorDescription
    {
        public Floor.TypeFloor Type { get; set; }
        public Guid Uuid { get; set; }
        public int FloorCount { get; set; }
        public bool NeedGate { get; set; }
        public bool NeedChimney { get; set; }
        public decimal FloorHeight { get; set; }
        public void SetFloorType(int selection)
        {
            switch (selection)
            {
                case 0: Type = Floor.TypeFloor.discharge; break;
                case 1: Type = Floor.TypeFloor.common; break;
                case 2: Type = Floor.TypeFloor.last; break;
            }
        }
        public string GetDescription()
        {
            string description = "";
            switch (Type)
            { 
                case Floor.TypeFloor.discharge: description = "Descarga";break;
                case Floor.TypeFloor.common:description = "Normal";break;
                case Floor.TypeFloor.last:description = "Ventilación";break;

            }
            return description;
        }
    }
}
