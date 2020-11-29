using MessageBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Responses
{
    public class ClientResponse : BaseResponse
    {
        public Client Client { get; set; }
    }
}
