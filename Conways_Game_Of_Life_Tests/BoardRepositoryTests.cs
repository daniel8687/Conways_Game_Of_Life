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
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
            };

            var id = _boardRepository.SaveBoard(board);
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public void GetBoard_ShouldReturnCorrectBoard()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
            };

            var id = _boardRepository.SaveBoard(board);
            var retrievedBoard = _boardRepository.GetBoard(id);

            Assert.Equal(board.Grid, retrievedBoard.Grid);
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
    }
}
