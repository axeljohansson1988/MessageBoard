using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Entities
{
    public class BaseEntity
    {
        public int? Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
