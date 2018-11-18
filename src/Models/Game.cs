using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class Game
    {
        private TypeCellGame[,] GameMatrix;
        
        public Game(TypeCellGame[,] gameMatrix)
        {
            GameMatrix = gameMatrix;
        }

        public TypeCellGame FindCellGameObject(Vec vec)
        {
            return GameMatrix[vec.X, vec.Y];
        }

        public Vec GetPlayerPos()
        {
            return new Vec(0, 0);
        }

        public void MovePlayer(Vec posVec)
        {

        }

        public TypeCellGame[,] GetMap()
        {
            return new TypeCellGame[0,0];
        }



    }
}
