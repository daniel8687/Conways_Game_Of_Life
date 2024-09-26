using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;

namespace Conways_Game_Of_Life.Services
{
    public class BoardRepository : IBoardRepository
    {
        private readonly Dictionary<Guid, Board> _boards = new();

        public Guid SaveBoard(Board board)
        {
            board.Id = Guid.NewGuid();
            _boards[board.Id] = board;
            return board.Id;
        }

        public Board GetBoard(Guid id)
        {
            return _boards.ContainsKey(id) ? _boards[id] : null;
        }
    }
}
