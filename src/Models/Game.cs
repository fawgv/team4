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
            for (int y = 0; y < width; y++)
                for (int x = 0; x < heigth; x++)
                {
                    switch (GameMap[y, x])
                    {
                        case TypeCellGame.Empty:
                            DynamicGameCells[y, x] = TypeCellGame.Empty;
                            StaticGameCells[y, x] = TypeCellGame.Empty;
                            break;
                        case TypeCellGame.Box:
                            DynamicGameCells[y, x] = TypeCellGame.Box;
                            StaticGameCells[y, x] = TypeCellGame.Empty;
                            break;
                        case TypeCellGame.Player:
                            playerPos = new Vec(y, x);
                            DynamicGameCells[y, x] = TypeCellGame.Empty;
                            StaticGameCells[y, x] = TypeCellGame.Empty;
                            break;
                        case TypeCellGame.Wall:
                            DynamicGameCells[y, x] = TypeCellGame.Empty;
                            StaticGameCells[y, x] = TypeCellGame.Wall;
                            break;
                        case TypeCellGame.Target:
                            DynamicGameCells[y, x] = TypeCellGame.Empty;
                            StaticGameCells[y, x] = TypeCellGame.Target;
                            break;
                        case TypeCellGame.BoxInTarget:
                            DynamicGameCells[y, x] = TypeCellGame.BoxInTarget;
                            StaticGameCells[y, x] = TypeCellGame.Empty;
                            break;
                    }
                }
        }

        public TypeCellGame FindCellGameObject(int x, int y)
        {
            if (y == playerPos.X && x == playerPos.Y)
                return TypeCellGame.Player;
            if (DynamicGameCells[y, x] != TypeCellGame.Empty)
                return DynamicGameCells[y, x];
            return StaticGameCells[y, x];
        }

        public TypeCellGame FindCellGameObject(Vec vec) => FindCellGameObject(vec.X, vec.Y);

        public Vec GetPlayerPos() => playerPos;

        public void MovePlayer(MoveDirection move)
        {
            switch (move)
            {
                case MoveDirection.Down:
                    var playerPosMoveDown = new Vec(playerPos.X, playerPos.Y + 1);
                    ChangePlayerPosition(playerPosMoveDown);
                    break;
                case MoveDirection.Up:
                    var playerPosMoveUp = new Vec(playerPos.X, playerPos.Y - 1);
                    ChangePlayerPosition(playerPosMoveUp);
                    break;
                case MoveDirection.Left:
                    var playerPosMoveLeft = new Vec(playerPos.X - 1, playerPos.Y);
                    ChangePlayerPosition(playerPosMoveLeft);
                    break;
                case MoveDirection.Right:
                    var playerPosMoveLeftRight = new Vec(playerPos.X + 1, playerPos.Y);
                    ChangePlayerPosition(playerPosMoveLeftRight);
                    break;
            }
        }

        private void ChangePlayerPosition(Vec playerPosMove)
        {
            if (CheckMapOut(playerPosMove))
            {
                playerPos = playerPosMove;
            }

            if (false)
            {
                
            }
        }

        public bool CheckMapOut(Vec playerMovePosition)
        {
            var result = playerMovePosition.X < 0
                         || playerMovePosition.Y < 0
                         || playerMovePosition.X >= width
                         || playerMovePosition.Y >= heigth;
            return result;
        }

        public bool CheckWallInNextMove(Vec nextMovePosition)
        {
            var result = StaticGameCells[nextMovePosition.X, nextMovePosition.Y] != TypeCellGame.Wall;
            return result;
        }

        public bool CheckBoxInNextMove(Vec nextMovePosition)
        {
            var result = StaticGameCells[nextMovePosition.X, nextMovePosition.Y] != TypeCellGame.Box;
            return result;
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
