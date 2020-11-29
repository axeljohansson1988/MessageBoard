using MessageBoard.API.Entities;
using MessageBoard.API.ErrorHandling;
using MessageBoard.API.Extensions;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Responses;
using MessageBoard.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoard.API.Services
{
    public class ClientService : IClientService
    {
        private readonly ICacheService cacheService;
        private readonly IIdProvider idProvider;
        public ClientService(ICacheService cacheService, IIdProvider idProvider)
        {
            this.cacheService = cacheService;
            this.idProvider = idProvider;
        }

        public bool ClientExists(int? clientId)
        {
            if (!clientId.IsValidId())
            {
                throw new ArgumentException($"{clientId} is not a valid {nameof(clientId)}");
            }

            return this.GetClients().Exists(client => client.Id == clientId);
        }

        public List<Client> GetClients()
        {
            var clients = this.cacheService.Get<List<Client>>(Constants.Constants.CacheKeys.AllClients);
            if (clients == null)
            {
                return new List<Client>();
            }

            return clients.OrderBy(client => client.Id).ToList();
        }

        public ClientResponse SetClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var response = this.ValidateClient(client);
            response.StorageOperation = new StorageOperation()
            {
                Id = Enums.StorageOperationEnum.Create,
                Name = Constants.Constants.StorageOperations.Create
            };
            if (!response.OperationSuccess)
            {
                response.Client = client;
                throw new HttpResponseException(response);
            }

            var clients = this.GetClients();
            client.Id = this.idProvider.GetNextId(clients);
            client.Created = DateTime.UtcNow;
            client.Modified = null;
            client.Name = client.Name.Trim();
            clients.Add(client);
            this.cacheService.Store(Constants.Constants.CacheKeys.AllClients, clients);
            response.Status = $"Successfully stored client with id: {client.Id}";
            response.Client = client;
            return response;
        }

        private ClientResponse ValidateClient(Client client)
        {
            var response = new ClientResponse();
            if (client == null)
            {
                response.OperationSuccess = false;
                response.Status = "Client is null.";
                return response;
            }

            if (string.IsNullOrWhiteSpace(client.Name))
            {
                response.OperationSuccess = false;
                response.Status = "Client.Name cannot be empty";
                return response;
            }

            response.OperationSuccess = true;
            response.Status = "Client is valid.";
            return response;
        }
    }
}
