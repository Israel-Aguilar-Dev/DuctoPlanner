using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public class Response<T>
    {
        public T Result { get; set; }
    }

    public class ResultData<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }

    public class CatalogModel
    {
        public string Name { get; set; }
        public List<CatalogRowModel> Data { get; set; }
    }

    public class CatalogRowModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string IdSyteLine { get; set; }  
    }

}
