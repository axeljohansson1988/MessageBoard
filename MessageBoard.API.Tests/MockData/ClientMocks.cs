using MessageBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.MockData
{
    public static class ClientMocks
    {
        public static readonly Client ClientWithEmptyName = new Client() { Name = string.Empty };
        public static readonly Client ClientWithWhiteSpaceName = new Client() { Name = "    " };
        public static readonly Client ClientWithoutName = new Client() { Name = null };
        public static readonly Client NewValidClient = new Client() { Name = "Valid Client" };
        public static readonly List<Client> EmptyClientsList = new List<Client>();
        public static readonly Client ExistingValidClient = new Client()
        {
            Id = 4,
            Name = "Existing Valid Client"
        };
        public static readonly List<Client> ThreeFirstClientsList = new List<Client>()
        {
            new Client()
            {
                Id = 1,
                Name = "Björn Borg"
            },
            new Client ()
            {
                Id = 2,
                Name = "Håkan Juholt"
            },
            new Client ()
            {
                Id = 3,
                Name = "Garry Kasparov"
            }
        };
        public static readonly List<Client> AllClientsList = ThreeFirstClientsList
            .Concat(new List<Client>() { ExistingValidClient })
            .Concat(new List<Client>()
        {
            new Client ()
            {
                Id = 5,
                Name = "Lady Gaga"
            },
            new Client ()
            {
                Id = 6,
                Name = "Bruce Springsteen"
            },
            new Client ()
            {
                Id = 7,
                Name = "Dr Fauci"
            }
        }).ToList();



    }
}
