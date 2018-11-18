using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        static readonly TypeCellGame[,] firstLevel =
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

        public static TypeCellGame[,] GetLevel(int numLevel)
        {
            switch (numLevel)
            {
                case 1:
                    return firstLevel;
                default:
                    throw new Exception("Введен неверный уровень");
            }
        }
        
        public void CreateGame()
        {
            Guid id = Guid.NewGuid();
            games.Add(id, new Game(firstLevel));
        }
    }
}