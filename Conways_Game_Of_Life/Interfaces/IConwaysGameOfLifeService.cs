using Conways_Game_Of_Life.Models;

namespace Conways_Game_Of_Life.Interfaces
{
    public interface IConwaysGameOfLifeService
    {
        Board GetNextState(Board board);
        List<Board> GetStates(Board board, int steps);
        Board GetFinalState(Board board, int maxSteps);
    }
}
