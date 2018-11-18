using FluentAssertions;
using NUnit.Framework;
using thegame.Models;

namespace thegame.tests
{
    [TestFixture]
    public class TestClass
    {
        Game game;

        readonly TypeCellGame[,] firstLevel =
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

        [TestCase(MoveDirection.Right, 2, 1)]
        [TestCase(MoveDirection.Left, 0, 1)]
        [TestCase(MoveDirection.Up, 1, 0)]
        [TestCase(MoveDirection.Down, 1, 2)]
        public void Success_MovePlayer(MoveDirection moveDirection, int newPlayerPosX, int newPlayerPosY)
        {
            game = new Game(new[,]
            {
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Player, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty
                }
            });

            game.MovePlayer(moveDirection);

            game.GetPlayerPos().Should().Be(new Vec(newPlayerPosY, newPlayerPosX));
        }

        [TestCase(MoveDirection.Right)]
        [TestCase(MoveDirection.Left)]
        [TestCase(MoveDirection.Up)]
        [TestCase(MoveDirection.Down)]
        public void DontMoveThroughWall(MoveDirection moveDirection)
        {
            game = new Game(new[,]
            {
                {
                    TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall
                },
                {
                    TypeCellGame.Wall, TypeCellGame.Player, TypeCellGame.Wall
                },
                {
                    TypeCellGame.Wall, TypeCellGame.Wall, TypeCellGame.Wall
                }
            });

            game.MovePlayer(moveDirection);

            game.GetPlayerPos().Should().Be(new Vec(1, 1));
        }

        [TestCase(MoveDirection.Right, 3, 2, 4, 2)]
        [TestCase(MoveDirection.Left, 1, 2, 0, 2)]
        [TestCase(MoveDirection.Up, 2, 1, 2, 0)]
        [TestCase(MoveDirection.Down, 2, 3, 2, 4)]
        public void Success_MovePlayerBox(MoveDirection moveDirection, int newPlayerPosX, int newPlayerPosY,
                                          int newBoxPosX, int newBoxPosY)
        {
            game = new Game(new[,]
            {
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Player, TypeCellGame.Box, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Empty
                }
            });

            game.MovePlayer(moveDirection);

            game.GetMap()[newPlayerPosY, newPlayerPosX].Should().Be(TypeCellGame.Player);
            game.GetMap()[newBoxPosY, newBoxPosX].Should().Be(TypeCellGame.Box);
        }

        [TestCase(MoveDirection.Right, 3, 2)]
        [TestCase(MoveDirection.Left, 1, 2)]
        [TestCase(MoveDirection.Up, 2, 1)]
        [TestCase(MoveDirection.Down, 2, 3)]
        public void Success_MovePlayerBox(MoveDirection moveDirection, int oldBoxPosX, int oldBoxPosY)
        {
            game = new Game(new[,]
            {
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Wall, TypeCellGame.Box, TypeCellGame.Player, TypeCellGame.Box, TypeCellGame.Wall
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Box, TypeCellGame.Empty, TypeCellGame.Empty
                },
                {
                    TypeCellGame.Empty, TypeCellGame.Empty, TypeCellGame.Wall, TypeCellGame.Empty, TypeCellGame.Empty
                }
            });

            game.MovePlayer(moveDirection);

            game.GetMap()[2, 2].Should().Be(TypeCellGame.Player);
            game.GetMap()[oldBoxPosY, oldBoxPosX].Should().Be(TypeCellGame.Box);
        }
    }
}