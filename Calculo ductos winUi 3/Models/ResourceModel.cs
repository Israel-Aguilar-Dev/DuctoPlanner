using DocumentFormat.OpenXml.Office.CoverPageProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;

namespace Calculo_ductos_winUi_3.Models
{
    //public class ResourceModel
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public decimal SalaryPerWorkDay { get; set; }
    //}
    //public class ResourceTypeModel
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    public class HumanResourceModel
    {
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public CatalogResourceModel Recurso { get; set; } = new CatalogResourceModel();
        public CatalogResourceTypeModel TipoRecurso { get; set; } = new CatalogResourceTypeModel();
        public int JornadasEfectivas { get; set; } = 0;
        public int DiasNoLaborales { get; set; } = 0;
        public decimal PrecioTotal {
            get {
                return JornadasEfectivas * Recurso.SalaryPerWorkday;
            }
        }
    }
    public class SubtotalHumaResource
    {
        public string Descripcion { get; set; }
        public decimal Subtotal { get; set; }
    }


}
