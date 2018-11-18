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
        
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputForMovesPost userInput)
        {
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
            
            var game = gamesRepo.GetGame(gameId);
            if (move != new Vec(0, 0))
                game.MovePlayer(move);
            var t = new TransformGameToGameDTO();
            return new ObjectResult(t.TransformGame(game, gameId));
        }
    }
}