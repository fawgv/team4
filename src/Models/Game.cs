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
            heigth = GameMap.GetLength(0);
            width = GameMap.GetLength(1);
            StaticGameCells = new TypeCellGame[heigth, width];
            DynamicGameCells = new TypeCellGame[heigth, width];
            for (int y = 0; y < heigth; y++)
                for (int x = 0; x < width; x++)
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

        public TypeCellGame FindCellGameObject(int y, int x)
        {
            if (y == playerPos.Y && x == playerPos.X)
                return TypeCellGame.Player;
            if (DynamicGameCells[y, x] != TypeCellGame.Empty)
                return DynamicGameCells[y, x];
            return StaticGameCells[y, x];
        }

        public TypeCellGame FindCellGameObject(Vec vec) => FindCellGameObject(vec.Y, vec.X);

        public Vec GetPlayerPos() => playerPos;

        public void MovePlayer(MoveDirection move)
        {
            Vec nextPos;
            switch (move)
            {
                case MoveDirection.Down:
                    nextPos = new Vec(playerPos.X, playerPos.Y + 1);
                    
                    break;
                case MoveDirection.Up:
                     nextPos = new Vec(playerPos.X, playerPos.Y - 1);
                 
                    break;
                case MoveDirection.Left:
                    nextPos = new Vec(playerPos.X - 1, playerPos.Y);
                 
                    break;
                case MoveDirection.Right:
                    nextPos = new Vec(playerPos.X + 1, playerPos.Y);   
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
            ChangePlayerPosition(nextPos, move);
        }

        private void ChangePlayerPosition(Vec playerPosMove, MoveDirection move)
        {
            if (DynamicGameCells[playerPosMove.Y, playerPosMove.X] == TypeCellGame.Box)
            {
                Vec nextBox;
                Vec behindBox;
                switch (move)
                {
                    case MoveDirection.Down:
                        nextBox = new Vec(playerPosMove.X, playerPosMove.Y + 1);
                        behindBox = new Vec(playerPosMove.X, playerPosMove.Y + 2);

                        break;
                    case MoveDirection.Up:
                        nextBox = new Vec(playerPosMove.X, playerPosMove.Y - 1);
                        behindBox = new Vec(playerPosMove.X, playerPosMove.Y - 2);

                        break;
                    case MoveDirection.Left:
                        nextBox = new Vec(playerPosMove.X - 1, playerPosMove.Y);
                        behindBox = new Vec(playerPosMove.X - 2, playerPosMove.Y);
                        break;
                    case MoveDirection.Right:
                        nextBox = new Vec(playerPosMove.X + 1, playerPosMove.Y);
                        behindBox = new Vec(playerPosMove.X + 2, playerPosMove.Y);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(move), move, null);
                }

                if((behindBox.Y < heigth && behindBox.X < width) && (StaticGameCells[behindBox.Y, behindBox.X] != TypeCellGame.Wall || 
                   DynamicGameCells[behindBox.Y, behindBox.X] != TypeCellGame.Box))
                {
                    DynamicGameCells[nextBox.Y, nextBox.X] = TypeCellGame.Box;
                    DynamicGameCells[playerPosMove.Y, playerPosMove.X] = TypeCellGame.Empty;
                    playerPos = playerPosMove;
                }
                 
            }
                
         
           else if (!CheckWallInNextMove(playerPosMove))
            {
               
                    //if (ChangeBoxPosition(playerPosMove, move))
                    //{
                        playerPos = playerPosMove;
                    //}
                
            }
        }

        private bool ChangeBoxPosition(Vec boxPosition, MoveDirection move)
        {
            Vec nextPositionBox = boxPosition;
            switch (move)
            {
                case MoveDirection.Up:
                    nextPositionBox = new Vec(boxPosition.X, boxPosition.Y-1);
                    break;
                case MoveDirection.Down:
                    nextPositionBox = new Vec(nextPositionBox.X, nextPositionBox.Y+1);
                    break;
                case MoveDirection.Left:
                    nextPositionBox = new Vec(nextPositionBox.X-1, nextPositionBox.Y);
                    break;
                case MoveDirection.Right:
                    nextPositionBox = new Vec(nextPositionBox.X+1, nextPositionBox.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }

            if (!CheckWallInNextMove(nextPositionBox) && !CheckBoxInNextMove(nextPositionBox))
            {
                var oldBoxCell = DynamicGameCells[boxPosition.Y, boxPosition.X];
                DynamicGameCells[nextPositionBox.Y, nextPositionBox.X] = oldBoxCell;
                return true;
            }

            return false;


        }

         

        private bool CheckMapOut(Vec playerMovePosition)
        {
            var result = playerMovePosition.X < 0
                         || playerMovePosition.Y < 0
                         || playerMovePosition.X >= width
                         || playerMovePosition.Y >= heigth;
            return result;
        }

        private bool CheckWallInNextMove(Vec nextMovePosition)
        {
            var result = StaticGameCells[nextMovePosition.Y, nextMovePosition.X] == TypeCellGame.Wall;
            return result;
        }

        private bool CheckBoxInNextMove(Vec nextMovePosition)
        {
            var result = StaticGameCells[nextMovePosition.Y, nextMovePosition.X] == TypeCellGame.Box;
            return result;
        }


        public TypeCellGame[,] GetMap()
        {
            var ResMap = new TypeCellGame[heigth, width];
            for (int y = 0; y < heigth; y++)
                for (int x = 0; x < width; x++)
                    ResMap[y, x] = FindCellGameObject(y, x);

            return ResMap;
        }
    }
}
