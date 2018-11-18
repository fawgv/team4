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
        
        
        [SetUp]
        public void SetGame()
        {
            game = new Game(firstLevel);
        }
        
        [Test]
        public void Success_MovePlayerUp()
        {
            game.MovePlayer(new Vec(0, -1));
            game.GetPlayerPos().Should().Be(new Vec(0, 0));
        }
    }
}
