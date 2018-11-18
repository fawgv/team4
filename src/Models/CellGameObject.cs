using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public enum TypeCellGame
    {
        Empty,
        Box,
        BoxInTarget,
        Player,
        Target,
        Wall
    }
    public class CellGameObject
    {
        public TypeCellGame TypeCell { get; set; }
        public Vec Vec { get; set; }

        public CellGameObject(Vec vec, TypeCellGame typeCell)
        {
            Vec = vec;
            typeCell = TypeCell;
        }
    }

}
