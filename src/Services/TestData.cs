using System;
using thegame.Models;

namespace thegame.Services
{
    public class TestData
    {
        public static GameDto AGameDto(Vec movingObjectPosition)
        {
            var width = 6;
            var height = 3;
            var testCells = new[]
            {
                new CellDto("1", new Vec(0, 0), "wall", "", 0),
                new CellDto("2", new Vec(0, 1), "wall", "", 0),
                new CellDto("3", new Vec(0, 2), "wall", "", 0),
                new CellDto("4", new Vec(1, 0), "wall", "", 0),
                new CellDto("5", new Vec(1, 2), "wall", "", 0),
                new CellDto("6", new Vec(2, 0), "wall", "", 0),
                new CellDto("7", new Vec(2, 2), "wall", "", 0),
                new CellDto("8", new Vec(3, 0), "wall", "", 0),
                new CellDto("9", new Vec(3, 2), "wall", "", 0),
                new CellDto("10", new Vec(4, 0), "wall", "", 0),
                new CellDto("11", new Vec(4, 2), "wall", "", 0),
                new CellDto("12", new Vec(5, 0), "wall", "", 0),
                new CellDto("13", new Vec(5, 1), "wall", "", 0),
                new CellDto("14", new Vec(5, 2), "wall", "", 0),
                new CellDto("15", new Vec(3, 1), "box", "", 20),
                new CellDto("16", new Vec(4, 1), "target", "", 20),
                new CellDto("17", movingObjectPosition, "player", "", 10),
            };
            return new GameDto(testCells, true, true, width, height, Guid.Empty, movingObjectPosition.X == 0, movingObjectPosition.Y);
        }
    }
}