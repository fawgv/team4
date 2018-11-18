using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class Game
    {
        private CellGameObject[,] GameMatrix;

        public Game(CellGameObject[,] gameMatrix)
        {
            GameMatrix = gameMatrix;
        }

        public CellGameObject FindCellGameObject(Vec vec)
        {
            return GameMatrix[vec.X, vec.Y];
        }


    }
}
