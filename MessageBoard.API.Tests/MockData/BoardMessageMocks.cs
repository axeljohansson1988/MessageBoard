using MessageBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.MockData
{
    public static class BoardMessageMocks
    {
        public static readonly BoardMessage BoardMessageWithEmptyMessage = new BoardMessage() { Message = string.Empty };
        public static readonly BoardMessage BoardMessageWithWhiteSpaceMessage = new BoardMessage() { Message = "    " };
        public static readonly BoardMessage BoardMessageWithoutMessage = new BoardMessage() { Message = null };
        public static readonly BoardMessage NewValidBoardMessage = new BoardMessage()
        {
            Message = "This was written and stored.",
            ClientId = ClientMocks.ExistingValidClient.Id
        };
        public static readonly BoardMessage BoardMessageWitNonExistantClient = new BoardMessage()
        {
            Message = "Im a message from a non-existant client.",
            ClientId = 1337
        };
        public static readonly BoardMessage BoardMessageWitInvalidClientId = new BoardMessage()
        {
            Message = "Im a message that cannot exist.",
            ClientId = -42
        };
        public static readonly List<BoardMessage> EmptyBoardMessagesList = new List<BoardMessage>();
        public static readonly BoardMessage ExistingValidBoardMessage = new BoardMessage()
        {
            Id = 4,
            Message = "This message is very much existing.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
        };
        public static readonly List<BoardMessage> ThreeFirstBoardMessagesList = new List<BoardMessage>()
        {
            new BoardMessage()
            {
                Id = 1,
                Message = "This message has id 1.",
                ClientId = ClientMocks.ExistingValidClient.Id,
                Created = new DateTime(2020, 11, 27)
            },
            new BoardMessage ()
            {
                Id = 2,
                Message = "This message is has id 2.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
            },
            new BoardMessage ()
            {
                Id = 3,
                Message = "This message has id 3.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
            }
        };
        public static readonly List<BoardMessage> AllBoardMessagesList = ThreeFirstBoardMessagesList
            .Concat(new List<BoardMessage>() { ExistingValidBoardMessage })
            .Concat(new List<BoardMessage>()
        {
            new BoardMessage ()
            {
                Id = 5,
                Message = "This message has id 5.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
            },
            new BoardMessage ()
            {
                Id = 6,
                Message = "This message has id 6.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
            },
            new BoardMessage ()
            {
                Id = 7,
                Message = "This message has id 7.",
            ClientId = ClientMocks.ExistingValidClient.Id,
            Created = new DateTime(2020, 11, 27)
            }
        }).ToList();
    }
}
