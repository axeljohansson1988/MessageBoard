using MessageBoard.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Entities
{
    public class StorageOperation
    {
        public StorageOperationEnum Id { get; set; }
        public string Name { get; set; }
    }
}
