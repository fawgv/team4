using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class Game
    {
        private TypeCellGame[,] StaticGameCells;
        private TypeCellGame[,] DynamicGameCells;

        public Game(TypeCellGame[,] dynamicGameCells)
        {
            DynamicGameCells = dynamicGameCells;
        }



        public TypeCellGame FindCellGameObject(Vec vec)
        {
            return DynamicGameCells[vec.X, vec.Y];
        }

        public Vec GetPlayerPos()
        {
            for (int xI = 0; xI < DynamicGameCells.GetLength(0); xI++)
            {
                for (int yI = 0; yI < DynamicGameCells.GetLength(1); yI++)
                {
                    if (DynamicGameCells[xI, yI] == TypeCellGame.Player)
                    {
                        return new Vec(xI, yI);
                    }
                }
            }
            return new Vec(0, 0);
        }

        public void MovePlayer(Vec posVec)
        {
            var oldPlayesPos = GetPlayerPos();
            DynamicGameCells[oldPlayesPos.X, oldPlayesPos.Y] = TypeCellGame.Empty; //Доработать на проверку с StaticGameCells
            DynamicGameCells[posVec.X, posVec.Y] = TypeCellGame.Player;
        }



        public TypeCellGame[,] GetMap()
        {
            return DynamicGameCells;  //Доработать на проверку с StaticGameCells
        }



    }
}
