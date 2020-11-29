using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageBoard.API.ErrorHandling;
using MessageBoard.API.Providers;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Responses;
using MessageBoard.API.Services;
using MessageBoard.API.Services.Interfaces;
using MessageBoard.API.Tests.MockServices;
using System;
using Newtonsoft.Json;

namespace MessageBoard.API.Tests.Tests
{
    [TestClass]
    public class ClientTests
    {
        private static readonly IIdProvider idProvider = new IdProvider();
        private static readonly ICacheService cacheService = new CacheService(new MemoryCacheMock());
        private static readonly IClientService clientService = new ClientService(cacheService, idProvider); 

        [TestMethod]
        public void FirstClientIdIs1()
        {
            var expected = 1;
            var firstId = idProvider.GetNextId(MockData.ClientMocks.EmptyClientsList);
            Assert.AreEqual(expected, firstId);
        }

        [TestMethod]
        public void NextClientIdIs4()
        {
            var expected = 4;
            var nextId = idProvider.GetNextId(MockData.ClientMocks.ThreeFirstClientsList);
            Assert.AreEqual(expected, nextId);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetClientWithEmptyNameThrowsHttpResponseException()
        {
            clientService.SetClient(MockData.ClientMocks.ClientWithEmptyName);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetClientWithoutNameThrowsHttpResponseException()
        {
            clientService.SetClient(MockData.ClientMocks.ClientWithoutName);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetClientWithWhiteSpaceNameThrowsHttpResponseException()
        {
            clientService.SetClient(MockData.ClientMocks.ClientWithWhiteSpaceName);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNullClientThrowsArgumentNullException()
        {
            clientService.SetClient(null);
            Assert.Fail();
        }

        [TestMethod]
        public void SetValidClientReturnsValidClientResponse()
        {
            var validClient = MockData.ClientMocks.NewValidClient;
            var expected = new ClientResponse()
            {
                Client = validClient,
                OperationSuccess = true,
                StorageOperation = new Entities.StorageOperation()
                {
                    Id = Enums.StorageOperationEnum.Create,
                    Name = Constants.Constants.StorageOperations.Create
                }
            };
            var response = clientService.SetClient(validClient);
            
            // setting not tested properties
            expected.Status = response.Status;
            expected.Client.Created = response.Client.Created;
            
            // convert to json strings for comparison
            var jsonExpected = JsonConvert.SerializeObject(expected);
            var jsonResponse = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonExpected, jsonResponse);
        }
    }
}
