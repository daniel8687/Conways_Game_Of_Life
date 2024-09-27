using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;
using Conways_Game_Of_Life.Services;

namespace Conways_Game_Of_Life_Tests
{
    public class BoardRepositoryTests
    {
        private readonly IBoardRepository _boardRepository;

        public BoardRepositoryTests()
        {
            _boardRepository = new BoardRepository();
        }

        [Fact]
        public void SaveBoard_ShouldGenerateNewId()
        {
            var board = new BoardIn
            {
                Rows = 3,
                Columns = 3,
                Grid = new List<List<bool>>()
            };

            var id = _boardRepository.SaveBoard(board);
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public void GetBoard_ShouldReturnCorrectBoard()
        {
            var board = new BoardIn
            {
                Rows = 3,
                Columns = 3,
                Grid = new List<List<bool>>()
            };

            var id = _boardRepository.SaveBoard(board);
            var retrievedBoard = _boardRepository.GetBoard(id);
            var expectedGrid = new bool[3, 3]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false }
            };

            Assert.Equal(expectedGrid, retrievedBoard.Grid);
            Assert.Equal(board.Rows, retrievedBoard.Rows);
            Assert.Equal(board.Columns, retrievedBoard.Columns);
        }

        [Fact]
        public void GetBoard_ShouldReturnNullIfNotFound()
        {
            var nonExistentId = Guid.NewGuid();
            var result = _boardRepository.GetBoard(nonExistentId);

            Assert.Null(result);
        }

        [Fact]
        public void GetBoard_ShouldReturnCorrectBoardGrid_IfGridListIsNull()
        {
            var board = new BoardIn
            {
                Rows = 3,
                Columns = 3,
                Grid = null
            };

            var id = _boardRepository.SaveBoard(board);
            var retrievedBoard = _boardRepository.GetBoard(id);
            var expectedBoardGrid = new bool[3, 3]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false }
            };

            Assert.Equal(retrievedBoard.Grid, expectedBoardGrid);
        }

        [Fact]
        public void GetBoard_ShouldReturnCorrectBoardGrid_IfGridListHaveLowerIndexs()
        {
            var board = new BoardIn
            {
                Rows = 3,
                Columns = 3,
                Grid = new List<List<bool>>()
                {
                    new() { true },
                    new() { true },
                    new() { true }
                }
            };

            var id = _boardRepository.SaveBoard(board);
            var retrievedBoard = _boardRepository.GetBoard(id);
            var expectedBoardGrid = new bool[3, 3]
            {
                { true, false, false },
                { true, false, false },
                { true, false, false }
            };

            Assert.Equal(retrievedBoard.Grid, expectedBoardGrid);
        }

        [Fact]
        public void GetBoard_ShouldReturnCorrectBoardGrid_IfGridListHaveGreaterIndexs()
        {
            var board = new BoardIn
            {
                Rows = 3,
                Columns = 3,
                Grid = new List<List<bool>>()
                {
                    new() { false, true, false, true, false },
                    new() { false, true, false, true, false },
                    new() { false, true, false, true, false }
                }
            };

            var id = _boardRepository.SaveBoard(board);
            var retrievedBoard = _boardRepository.GetBoard(id);
            var expectedBoardGrid = new bool[3, 3]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };

            Assert.Equal(retrievedBoard.Grid, expectedBoardGrid);
        }

        [Fact]
        public void GetBoardOut_ShouldReturnCorrectBoardGridList()
        {
            var board = new Board
            {
                Id = Guid.Empty,
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            var retrievedBoardOut = _boardRepository.GetBoardOut(board);
            var expectedBoardGridList = new List<List<bool>>()
            {
                new() { false, true, false },
                new() { false, true, false },
                new() { false, true, false }
            };

            Assert.Equal(board.Id, retrievedBoardOut.Id);
            Assert.Equal(board.Columns, retrievedBoardOut.Columns);
            Assert.Equal(board.Rows, retrievedBoardOut.Rows);
            Assert.Equal(expectedBoardGridList, retrievedBoardOut.Grid);
        }

        [Fact]
        public void GetBoardOut_ShouldReturnCorrectBoardGridList_IfIsNull()
        {
            var retrievedBoardOut = _boardRepository.GetBoardOut(null);

            Assert.True(retrievedBoardOut is not null);
        }
    }
}
