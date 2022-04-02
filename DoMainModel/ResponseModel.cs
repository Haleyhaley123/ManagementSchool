using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMainModel
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
