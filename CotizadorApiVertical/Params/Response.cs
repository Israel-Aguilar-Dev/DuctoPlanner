using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CotizadorApiVertical.Params
{
    public class Response
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
    }
}