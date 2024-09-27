using Conways_Game_Of_Life.Interfaces;
using Conways_Game_Of_Life.Models;
using Microsoft.AspNetCore.Mvc;

namespace Conways_Game_Of_Life.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConwaysGameOfLifeController : ControllerBase
    {
        private readonly IConwaysGameOfLifeService _conwaysGameOfLifeService;
        private readonly IBoardRepository _boardRepository;
        private readonly ILogger<ConwaysGameOfLifeController> _logger;

        public ConwaysGameOfLifeController(IConwaysGameOfLifeService conwaysGameOfLifeService, IBoardRepository boardRepository, ILogger<ConwaysGameOfLifeController> logger)
        {
            _conwaysGameOfLifeService = conwaysGameOfLifeService;
            _boardRepository = boardRepository;
            _logger = logger;
        }
        [HttpPost("upload")]
        public IActionResult UploadBoard([FromBody] BoardIn board)
        {
            var id = _boardRepository.SaveBoard(board);
            return Ok(new { Id = id });
        }

        [HttpGet("{id}/next")]
        public IActionResult GetNextState(Guid id)
        {
            var board = _boardRepository.GetBoard(id);
            if (board == null) return NotFound();

            var nextBoard = _conwaysGameOfLifeService.GetNextState(board);
            return Ok(_boardRepository.GetBoardOut(nextBoard));
        }

        [HttpGet("{id}/steps/{steps}")]
        public IActionResult GetStates(Guid id, int steps)
        {
            var board = _boardRepository.GetBoard(id);
            if (board == null) return NotFound();

            var states = _conwaysGameOfLifeService.GetStates(board, steps);
            return Ok(states.Select(x => _boardRepository.GetBoardOut(x)));
        }

        [HttpGet("{id}/final/{maxSteps}")]
        public IActionResult GetFinalState(Guid id, int maxSteps)
        {
            var board = _boardRepository.GetBoard(id);
            if (board == null) return NotFound();

            try
            {
                var finalBoard = _conwaysGameOfLifeService.GetFinalState(board, maxSteps);
                return Ok(_boardRepository.GetBoardOut(finalBoard));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
