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

            switch (move)
            {
                case MoveDirection.Down:
                    var playerPosMoveDown = new Vec(playerPos.X, playerPos.Y + 1);
                    ChangePlayerPosition(playerPosMoveDown, move);
                    break;
                case MoveDirection.Up:
                    var playerPosMoveUp = new Vec(playerPos.X, playerPos.Y - 1);
                    ChangePlayerPosition(playerPosMoveUp, move);
                    break;
                case MoveDirection.Left:
                    var playerPosMoveLeft = new Vec(playerPos.X - 1, playerPos.Y);
                    ChangePlayerPosition(playerPosMoveLeft, move);
                    break;
                case MoveDirection.Right:
                    var playerPosMoveLeftRight = new Vec(playerPos.X + 1, playerPos.Y);
                    ChangePlayerPosition(playerPosMoveLeftRight, move);
                    break;
            }
        }

        private void ChangePlayerPosition(Vec playerPosMove, MoveDirection move)
        {
            //if (!CheckMapOut(playerPosMove))
            //{
            //    playerPos = playerPosMove;
            //}

            if (!CheckWallInNextMove(playerPosMove))
            {
                if (!CheckBoxInNextMove(playerPosMove))
                {
                    //if (ChangeBoxPosition(playerPosMove, move))
                    //{
                    playerPos = playerPosMove;
                    //}
                }
            }
        }

        private bool ChangeBoxPosition(Vec boxPosition, MoveDirection move)
        {
            Vec nextPositionBox = boxPosition;
            switch (move)
            {
                case MoveDirection.Up:
                    nextPositionBox = new Vec(boxPosition.X, boxPosition.Y - 1);
                    break;
                case MoveDirection.Down:
                    nextPositionBox = new Vec(nextPositionBox.X, nextPositionBox.Y + 1);
                    break;
                case MoveDirection.Left:
                    nextPositionBox = new Vec(nextPositionBox.X - 1, nextPositionBox.Y);
                    break;
                case MoveDirection.Right:
                    nextPositionBox = new Vec(nextPositionBox.X + 1, nextPositionBox.Y);
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

        #region Statistic

        private int movesCount = 0;

        public int GetStatistic()
        {
            int targetCost = 5;
            int targetCount = GetListTargetsFromDynamicGameCells().Count;
            return targetCount * targetCost - movesCount;
        }

        #endregion

        #region Finish

        public bool IsFinished()
        {
            foreach (var targetVec in GetListTargetsFromDynamicGameCells())
            {
                if (DynamicGameCells[targetVec.Y, targetVec.X]!= TypeCellGame.Box)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Vec> GetListTargetsFromDynamicGameCells()
        {
            List<Vec> listVecsTargets = new List<Vec>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    if (StaticGameCells[y,x] == TypeCellGame.Target)
                    {
                        listVecsTargets.Add(new Vec(x,y));
                    }
                }
            }

            return listVecsTargets;
        }

        #endregion
    }
}
