using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Store<T>(string key, T cacheObject);
    }
}
