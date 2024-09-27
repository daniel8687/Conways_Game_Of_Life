using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;
using Conways_Game_Of_Life.Services;

namespace Conways_Game_Of_Life_Tests
{
    public class ConwaysGameOfLifeServiceTests
    {
        private readonly IConwaysGameOfLifeService _conwaysGameOfLifeService;

        public ConwaysGameOfLifeServiceTests()
        {
            _conwaysGameOfLifeService = new ConwaysGameOfLifeService();
        }

        #region GetNextState

        [Fact]
        public void GetNextState_ShouldReturnCorrectNextState()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            var expectedNextState = new bool[3, 3]
            {
                { false, false, false },
                { true, true, true },
                { false, false, false }
            };

            var nextBoard = _conwaysGameOfLifeService.GetNextState(board);

            Assert.Equal(expectedNextState, nextBoard.Grid);
        }

        [Fact]
        public void GetNextState_ShouldHandleEmptyBoard()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
            };

            var nextBoard = _conwaysGameOfLifeService.GetNextState(board);

            Assert.All(nextBoard.Grid.Cast<bool>(), cell => Assert.False(cell));
        }

        [Fact]
        public void GetNextState_ShouldHandleFullyAliveBoard()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true }
                }
            };

            var nextBoard = _conwaysGameOfLifeService.GetNextState(board);

            // After applying rules, edges will die from overpopulation, but center will survive
            var expectedNextState = new bool[3, 3]
            {
                { true, false, true },
                { false, false, false },
                { true, false, true }
            };

            Assert.Equal(expectedNextState, nextBoard.Grid);
        }

        #endregion

        #region GetStates

        [Fact]
        public void GetStates_ShouldReturnCorrectNumberOfSteps()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            var states = _conwaysGameOfLifeService.GetStates(board, 3);

            // We expect 4 boards: initial state + 3 steps
            Assert.Equal(4, states.Count);
        }

        [Fact]
        public void GetStates_ShouldReturnCorrectIntermediateStates()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            var states = _conwaysGameOfLifeService.GetStates(board, 2); // Get 2 intermediate states

            // Initial state (step 0)
            var expectedFirstState = new bool[3, 3]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };
            Assert.Equal(expectedFirstState, states[0].Grid);

            // Second state (step 1 - horizontal blinker)
            var expectedSecondState = new bool[3, 3]
            {
                { false, false, false },
                { true, true, true },
                { false, false, false }
            };
            Assert.Equal(expectedSecondState, states[1].Grid);

            // Third state (step 2 - returns to vertical blinker)
            var expectedThirdState = new bool[3, 3]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };
            Assert.Equal(expectedThirdState, states[2].Grid);
        }

        #endregion

        #region GetFinalState

        [Fact]
        public void GetFinalState_ShouldReturnStableState()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            // Run the simulation for a few steps (more than the oscillator period, which is 2 for the blinker)
            var finalBoard = _conwaysGameOfLifeService.GetFinalState(board, 10);

            // The final state should be the same as the starting state after every two steps (blinker oscillation)
            var expectedStableState = new bool[3, 3]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };

            Assert.Equal(expectedStableState, finalBoard.Grid);
        }

        [Fact]
        public void GetFinalState_ShouldThrowErrorIfMaxStepsReached()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Grid = new bool[3, 3]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            var exception = Assert.Throws<InvalidOperationException>(() =>
                _conwaysGameOfLifeService.GetFinalState(board, 1)); // Give only 1 step, though the pattern oscillates

            Assert.Equal("Reached max steps without reaching a stable state.", exception.Message);
        }

        #endregion
    }
}
