using MessageBoard.API.Entities;
using MessageBoard.API.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Providers
{
    public class IdProvider : IIdProvider
    {
        public int? GetNextId<T>(List<T> list)
        {
            int? highestId = null;
            switch (list)
            {
                case List<BoardMessage> messageList:
                    highestId = messageList.Max(m => m.Id);
                    break;
                case List<Client> clientList:
                    highestId = clientList.Max(m => m.Id);
                    break;
                default:
                    throw new NotImplementedException($"GetNextId is not implemented for list of type: {list.GetType().Name}");
            }

            if (highestId == null)
            {
                highestId = 0;
            }

            return highestId + 1;
        }
    }
}
