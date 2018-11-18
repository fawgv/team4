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
        public IActionResult Moves(Guid gameId, [FromBody] UserInputForMovesPost userInput)
        {
            MoveDirection? move = null;
            switch (userInput.KeyPressed)
            {
                case 'W':
                    move = MoveDirection.Up;
                    break;
                case 'D':
                    move = MoveDirection.Right;
                    break;
                case 'S':
                    move = MoveDirection.Down;
                    break;
                case 'A':
                    move = MoveDirection.Left;
                    break;
            }

            var game = gamesRepo.GetGame(gameId);
            if (move != null)
                game.MovePlayer(move.Value);
            var t = new TransformGameToGameDTO();
            return new ObjectResult(t.TransformGame(game, gameId));
        }
    }
}