using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.ErrorHandling
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(string message) : base ()
        {
            this.Message = message;
        }

        public HttpResponseException(object value, string message) : base()
        {
            this.Value = value;
            this.Message = message;
        }

        public HttpResponseException(object value)
        {
            this.Value = value;
        }


        public int Status { get; set; } = 500;

        public override string Message { get; }

        public object Value { get; set; }
    }
}
