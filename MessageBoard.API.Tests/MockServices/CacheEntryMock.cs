using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.MockServices
{
    class CacheEntryMock : ICacheEntry
    {
        public CacheEntryMock()
        {
            this.Value = new object();
        }

        public DateTimeOffset? AbsoluteExpiration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<IChangeToken> ExpirationTokens => throw new NotImplementedException();

        public object Key { get;  set; }

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks => throw new NotImplementedException();

        public CacheItemPriority Priority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long? Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan? SlidingExpiration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Value { get; set; }

        public void Dispose()
        {
            return;
        }

    }
}
