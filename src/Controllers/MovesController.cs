using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputForMovesPost userInput)
        {
            Vec move = null;
            switch (userInput.KeyPressed)
            {
                case 'W':
                    move = new Vec(0,-1);
                    break;
                case 'D':
                    move = new Vec(1, 0);
                    break;
                case 'S':
                    move = new Vec(0, 1);
                    break;
                case 'A':
                    move = new Vec(-1, 0);
                    break;
                default:
                    move = new Vec(0, 0);
                    break;
            }
            var game = TestData.AGameDto(move);
            if (userInput.ClickedPos != null)
                game.Cells.First(c => c.Type == "color4").Pos += move;
            return new ObjectResult(game);
        }
    }
}