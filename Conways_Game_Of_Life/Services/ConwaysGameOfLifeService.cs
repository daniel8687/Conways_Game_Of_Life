using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;
using System.Text;

namespace Conways_Game_Of_Life.Services
{
    public class ConwaysGameOfLifeService : IConwaysGameOfLifeService
    {
        public Board GetNextState(Board board)
        {
            var nextBoard = new Board
            {
                Rows = board.Rows,
                Columns = board.Columns,
                Grid = new bool[board.Rows, board.Columns]
            };

            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    int liveNeighbors = CountLiveNeighbors(board, row, col);

                    if (board.GridEnumerable[row][col])
                    {
                        // Rule 1 & 2: A live cell with 2 or 3 neighbors survives, otherwise it dies
                        nextBoard.Grid[row, col] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        // Rule 4: A dead cell with exactly 3 neighbors comes to life
                        nextBoard.Grid[row, col] = liveNeighbors == 3;
                    }
                }
            }

            return nextBoard;
        }

        public List<Board> GetStates(Board board, int steps)
        {
            var states = new List<Board> { board };
            for (int i = 0; i < steps; i++)
            {
                var nextState = GetNextState(states.Last());
                states.Add(nextState);
            }
            return states;
        }

        public Board GetFinalState(Board board, int maxSteps)
        {
            var currentBoard = board;
            var seenBoards = new HashSet<string>(); // Store unique states by stringifying the grid
            seenBoards.Add(BoardToString(currentBoard));

            for (int i = 0; i < maxSteps; i++)
            {
                var nextBoard = GetNextState(currentBoard);

                var nextStateString = BoardToString(nextBoard);
                if (seenBoards.Contains(nextStateString))
                {
                    // Oscillation detected: The next state has been seen before
                    return nextBoard; // Return the current state as the final oscillating state
                }

                seenBoards.Add(nextStateString);
                currentBoard = nextBoard;
            }

            throw new InvalidOperationException("Reached max steps without reaching a stable state.");
        }

        private string BoardToString(Board board)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    sb.Append(board.Grid[i, j] ? "1" : "0");
                }
                sb.Append("|"); // Use '|' as row separator
            }
            return sb.ToString();
        }

        // Helper method to count live neighbors
        private int CountLiveNeighbors(Board board, int row, int col)
        {
            int liveNeighbors = 0;
            int[] directions = { -1, 0, 1 }; // To check all 8 neighbors

            foreach (int dRow in directions)
            {
                foreach (int dCol in directions)
                {
                    if (dRow == 0 && dCol == 0) continue; // Skip the current cell

                    int newRow = row + dRow;
                    int newCol = col + dCol;

                    // Check if the neighboring cell is within bounds
                    if (newRow >= 0 && newRow < board.Rows && newCol >= 0 && newCol < board.Columns)
                    {
                        if (board.Grid[newRow, newCol]) liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
    }
}
