using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class TransformGameToGameDTO
    {
        public GameDto TransformGame(Game game)
        {
            var someVector = new Vec(1, 1);
            var width = 6;
            var height = 3;
            var listCellDto = new List<CellDto>();
            var r = new Random();
            var map = game.GetMap();
            for(int y = 0; y < map.GetLength(0); y++)
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    var id = y * map.GetLength(1) + x;
                    listCellDto.Add(new CellDto(id.ToString(), new Vec(x, y), ConvertEnumToString(map[y,x]), "", 0));
                }
            return new GameDto(listCellDto.ToArray(), true, true, width, height, Guid.Empty, someVector.X == 0, someVector.Y);
        }

        public String ConvertEnumToString(TypeCellGame typeCellGame)
        {
            switch (typeCellGame)
            {
                case TypeCellGame.Player:
                    return "player";
                case TypeCellGame.Box:
                    return "box";
                case TypeCellGame.Wall:
                    return "wall";
                case TypeCellGame.Empty:
                    return "empty";
                case TypeCellGame.Target:
                    return "target";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
