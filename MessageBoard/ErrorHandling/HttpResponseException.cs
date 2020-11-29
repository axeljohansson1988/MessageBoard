using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.ErrorHandling
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(object value)
        {
            this.Value = value;
        }

        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
