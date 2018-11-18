using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class TransformGameToGameDTO
    {
        //public Game TransformGame(GameDto gameDto)
        //{

        //}

        public CellGameObject TransformToCellDTO(CellDto cell)
        {
            return new CellGameObject(cell.Pos, cell.TypeCellGame);
        }
    }
}
