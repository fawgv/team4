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
        GamesRepo gamesRepo;
        
        public MovesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }
        
        public static Vec player;
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputForMovesPost userInput)
        {
            if (player == null)
                player = new Vec(1, 1);
            Vec move;
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
            player += move;
            var game = gamesRepo.GetGame(gameId);
            if (userInput.ClickedPos != null)
            {
                game.Cells.First(c => c.Type == "player").Pos = player;
            }

            return new ObjectResult(game);
        }
    }
}