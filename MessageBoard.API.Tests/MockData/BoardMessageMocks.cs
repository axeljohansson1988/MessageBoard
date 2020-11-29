using MessageBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.UnitTests.MockData
{
    public static class BoardMessageMocks
    {
        public static readonly BoardMessage BoardMessageWithEmptyMessage = new BoardMessage() { Message = string.Empty };
        public static readonly BoardMessage BoardMessageWithWhiteSpaceMessage = new BoardMessage() { Message = "    " };
        public static readonly BoardMessage BoardMessageWithoutMessage = new BoardMessage() { Message = null };
        public static readonly List<BoardMessage> EmptyBoardMessagesList = new List<BoardMessage>();
        public static readonly List<BoardMessage> ThreeFirstBoardMessagesList = new List<BoardMessage>()
        {
            new BoardMessage()
            {
                Id = 1
            },
            new BoardMessage ()
            {
                Id = 2
            },
            new BoardMessage ()
            {
                Id = 3
            }
        };
        public static readonly List<BoardMessage> AllBoardMessagesList = ThreeFirstBoardMessagesList.Concat(new List<BoardMessage>()
        {
            new BoardMessage ()
            {
                Id = 4
            },
            new BoardMessage ()
            {
                Id = 5
            },
            new BoardMessage ()
            {
                Id = 6
            }
        }).ToList();
    }
}
