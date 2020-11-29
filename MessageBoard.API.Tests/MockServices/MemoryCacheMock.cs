using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.MockServices
{
    class MemoryCacheMock : IMemoryCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            switch (key)
            {
                case Constants.Constants.CacheKeys.AllClients:
                    value = MockData.ClientMocks.AllClientsList;
                    return true;
                case Constants.Constants.CacheKeys.AllBoardMessages:
                    value = MockData.BoardMessageMocks.AllBoardMessagesList;
                    return true;
                default:
                    value = null;
                    return false;
            }
        }
    }
}
