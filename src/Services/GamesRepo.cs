using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        TypeCellGame[,] firstLevel =
        {
            {
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall,
                TypeCellGame.Wall
            },
            {
                TypeCellGame.Wall, TypeCellGame.Player, TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Target,
                TypeCellGame.Wall
            },
            {
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall,
                TypeCellGame.Wall,
            }
        };
        
        static readonly Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public Game GetGame(Guid id) => games[id];

        public Guid CreateGame()
        {
            Guid id = Guid.NewGuid();
            games.Add(id, new Game(firstLevel));
            return id;
        }
    }
}