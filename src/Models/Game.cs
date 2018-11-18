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
        private int heigth;
        private int width;
        private Vec playerPos;

        public Game(TypeCellGame[,] GameMap)
        {
            heigth = GameMap.GetLength(1);
            width = GameMap.GetLength(0);
            StaticGameCells = new TypeCellGame[width, heigth];
            DynamicGameCells = new TypeCellGame[width, heigth];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < heigth; y++)
            {
                switch (GameMap[x, y])
                {
                    case TypeCellGame.Empty:
                        DynamicGameCells[x, y] = TypeCellGame.Empty;
                        StaticGameCells[x, y] = TypeCellGame.Empty;
                        break;
                    case TypeCellGame.Box:
                        DynamicGameCells[x, y] = TypeCellGame.Box;
                        StaticGameCells[x, y] = TypeCellGame.Empty;
                        break;
                    case TypeCellGame.Player:
                        playerPos = new Vec(x, y);
                        DynamicGameCells[x, y] = TypeCellGame.Empty;
                        StaticGameCells[x, y] = TypeCellGame.Empty;
                        break;
                    case TypeCellGame.Wall:
                        DynamicGameCells[x, y] = TypeCellGame.Empty;
                        StaticGameCells[x, y] = TypeCellGame.Wall;
                        break;
                    case TypeCellGame.Target:
                        DynamicGameCells[x, y] = TypeCellGame.Empty;
                        StaticGameCells[x, y] = TypeCellGame.Target;
                        break;
                    case TypeCellGame.BoxInTarget:
                        DynamicGameCells[x, y] = TypeCellGame.BoxInTarget;
                        StaticGameCells[x, y] = TypeCellGame.Empty;
                        break;
                }
            }
        }

        public TypeCellGame FindCellGameObject(int x, int y)
        {
            if (x == playerPos.X && y == playerPos.Y)
                return TypeCellGame.Player;
            if (DynamicGameCells[x, y] != TypeCellGame.Empty)
                return DynamicGameCells[x, y];
            return StaticGameCells[x, y];
        }

        public TypeCellGame FindCellGameObject(Vec vec) => FindCellGameObject(vec.X, vec.Y);

        public Vec GetPlayerPos() => playerPos;

        public void MovePlayer(Vec posVec)
        {
            var oldPlayesPos = GetPlayerPos();
            DynamicGameCells[oldPlayesPos.X, oldPlayesPos.Y] = TypeCellGame.Empty; //Доработать на проверку с StaticGameCells
            DynamicGameCells[posVec.X, posVec.Y] = TypeCellGame.Player;
        }



        public TypeCellGame[,] GetMap()
        {
            var ResMap = new TypeCellGame[width, heigth];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < heigth; y++)
                ResMap[x, y] = FindCellGameObject(x, y);

            return ResMap;
        }
    }
}
