using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Entities
{
    public class BoardMessage : BaseEntity
    {
        public int? ClientId { get; set; }
        public string Message { get; set; }
        public bool? IsMine { get; set; }
    }
}
