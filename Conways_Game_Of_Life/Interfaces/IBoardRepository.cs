using Conways_Game_Of_Life.Models;

namespace Conways_Game_Of_Life.Interfaces
{
    public interface IBoardRepository
    {
        Guid SaveBoard(BoardIn board);
        Board GetBoard(Guid id);
        BoardOut GetBoardOut(Board board);
    }
}
