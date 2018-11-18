using System;
using FluentAssertions;
using NUnit.Framework;
using thegame;
using thegame.Models;
using thegame.Services;

namespace thegame.tests
{
    [TestFixture]
    public class TestClass
    {
        Game game;
        
        [SetUp]
        public void SetGame()
        {
            game = new Game(GamesRepo.GetLevel(1));
        }
        
        [Test]
        public void Success_MovePlayerUp()
        {
            game.MovePlayer(new Vec(0, -1));
            game.GetPlayerPos().Should().Be(new Vec(0, 0));
        }
    }
}
