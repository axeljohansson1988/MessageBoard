using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Constants
{
    public class Constants
    {
        public class CacheKeys
        {
            public const string AllBoardMessages = "AllBoardMessages";
            public const string AllClients = "AllClients";
        }

        public class StorageOperations
        {
            public const string Create = "Create";
            public const string Update = "Update";
            public const string Delete = "Delete";
        }
    }
}
