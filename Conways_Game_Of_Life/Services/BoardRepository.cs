using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;

namespace Conways_Game_Of_Life.Services
{
    public class BoardRepository : IBoardRepository
    {
        private readonly Dictionary<Guid, Board> _boards = new();

        public Guid SaveBoard(BoardIn board)
        {
            var newBoard = new Board();
            newBoard.Id = Guid.NewGuid();
            newBoard.Rows = board.Rows;
            newBoard.Columns = board.Columns;
            newBoard.Grid = FillGrid(board);
            _boards[newBoard.Id] = newBoard;
            return newBoard.Id;
        }

        public Board GetBoard(Guid id)
        {
            return _boards.ContainsKey(id) ? _boards[id] : null;
        }

        public BoardOut GetBoardOut(Board board)
        {
            return board is null ? new BoardOut() : new BoardOut()
            {
                Id = board.Id,
                Rows = board.Rows,
                Columns = board.Columns,
                Grid = FillGridList(board)
            };
        }

        private bool[,] FillGrid(BoardIn board)
        {
            bool[,] result = new bool[board.Rows, board.Columns];
            for (int x = 0; x < board.Rows; x++)
            {
                for (int y = 0; y < board.Columns; y++)
                {
                    try
                    {
                        result[x, y] = board.Grid is null ? false : board.Grid[x].Count <= 0 ? false : board.Grid[x][y];
                    }
                    catch(ArgumentOutOfRangeException ex)
                    {
                        continue;
                    }
                }
            }

            return result;
        }

        private List<List<bool>> FillGridList(Board board)
        {
            //List<List<bool>> result = Enumerable.Repeat(Enumerable.Repeat(false, board.Columns).ToList(), board.Rows).ToList();
            var result = new List<List<bool>>();
            for (int x = 0; x < board.Rows; x++)
            {
                var tempRowList = new List<bool>();
                for (int y = 0; y < board.Columns; y++)
                {
                    tempRowList.Add(board.Grid[x, y]);
                }
                result.Add(tempRowList);
            }

            return result;
        }
    }
}
