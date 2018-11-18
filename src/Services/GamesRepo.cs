using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        static GamesRepo()
        {
            allLevels =  new[]
            {
                simpleLevel,
                defaultLevel
            };
        }

        static readonly Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public Game GetGame(Guid id) => games[id];

        public int CountLevels => allLevels.Length;

        public Guid CreateGame(int numLevel)
        {
            Guid id = Guid.NewGuid();
            games.Add(id, new Game(allLevels[numLevel]));
            return id;
        }

        static readonly TypeCellGame[,] simpleLevel =
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

        static readonly TypeCellGame[][,] allLevels;

        public static readonly TypeCellGame[,] defaultLevel =
        {
            {
                TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall,
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Empty
            },
            {
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Empty,
                TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty
            },
            {
                TypeCellGame.Wall, TypeCellGame.Target, TypeCellGame.Player, TypeCellGame.Box, TypeCellGame.Empty,
                TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty
            },
            {
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Box,
                TypeCellGame.Target, TypeCellGame.Wall, TypeCellGame.Empty
            },
            {
                TypeCellGame.Wall, TypeCellGame.Target, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Box,
                TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty
            },
            {
                TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Target,
                TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Wall
            },
            {
                TypeCellGame.Wall, TypeCellGame.Box, TypeCellGame.Empty, TypeCellGame.BoxInTarget, TypeCellGame.Box,
                TypeCellGame.Box, TypeCellGame.Target, TypeCellGame.Wall
            },
            {
                TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Target,
                TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Wall
            },
            {
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall,
                TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall
            }
        };
    }
}