using MessageBoard.API.Entities;
using MessageBoard.API.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services.Interfaces
{
    public interface IClientService
    {
        ClientResponse SetClient(Client client);
        List<Client> GetClients();
        bool ClientExists(int? clientId);
    }
}
