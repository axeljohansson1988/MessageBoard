using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Providers.Interfaces
{
    public interface IIdProvider
    {
        int? GetNextId<T>(List<T> list);
    }
}
