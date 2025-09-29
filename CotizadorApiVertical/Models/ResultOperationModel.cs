using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CotizadorApiVertical.Models
{
    public class ResultOperationModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}