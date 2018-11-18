using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        GamesRepo gamesRepo;
        
        public GamesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }
        
        [HttpPost]
        public IActionResult Index()
        {
            var t = new TransformGameToGameDTO();
            var guid = gamesRepo.CreateGame(1);
            var game = t.TransformGame(gamesRepo.GetGame(guid), guid);
            return new ObjectResult(game);
        }
    }
}
