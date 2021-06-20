using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Havannah.HexagonButton;


namespace Havannah.Logic
{
    public class Board
    {
        public class Point
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private int[,] _grid;
        private int _size;
        private List<Point> _freeCells;

        public Board(int size)
        {
            _size = size;
            _grid = new int[2 * size - 1, 2 * size - 1];

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if(CheckIfClickIsCorrect(i, j))
                    {
                        _grid[i, j] = 0;
                    }
                    else
                    {
                        _grid[i, j] = -1;
                    }
                }
            }

            _freeCells = new List<Point>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    _freeCells.Add(new Point(i, j));
                    _freeCells.Add(new Point((2 * size - 1) - i, (2 * size - 1) - j));
                }
            }
        }

        public int CheckIfWon()
        {
            if(CheckIfWon(1))
            {
                return 1;
            }

            if (CheckIfWon(2))
            {
                return 2;
            }

            return 0;
        }

        public bool CheckIfWon(int player)
        {
            return CheckIfRing(player) || CheckIfFork(player) || CheckIfBridge(player);
        }


        public bool CheckIfDraw() { return false; }

        public Board Clone()
        {
            return new Board(_size, _grid, _freeCells);
        }

        public bool MakeRandomMove(int player)
        {
            if (_freeCells.Count <= 0) return false;
            Random random = new Random();
            int randomPosition = random.Next(0, _freeCells.Count);
            Point randomPoint = _freeCells[randomPosition];
            MakeMove(randomPoint.X, randomPoint.Y, player);
            _freeCells.RemoveAt(randomPosition);

            return true;
        }

        public bool MakeMove(int player, Point point)
        {
            return MakeMove(player, point.X, point.Y);
        }

        public bool MakeMove(int player, int x, int y)
        {
            EvaluateClickCoordinates(x, y);

            if (_grid[x, y] == 0)
            {
                _grid[x, y] = player;
                return true;
            }

            return false;  
        }

        public void ResetBoard() { Array.Clear(_grid, 0, _grid.Length); }

        public void PrintBoard()
        {
            Console.WriteLine("*********************************************************");

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    Console.Write("[" + GenerateHexagonButtonName(i, j) + "] = " + _grid[i, j] + "   ");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("*********************************************************");
            Console.Write(Environment.NewLine + Environment.NewLine);

        }

        public bool CheckIfClickIsCorrect(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 2 * _size - 1)
            {
                return false;
            }

            if (y >= x + _size)
            {
                return false;
            }


            if (y < x + _size && y > x - _size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfRing(int player)
        {
            return false;
        }

        private bool CheckIfBridge(int player)
        {
            return false;
        }

        private bool CheckIfFork(int player)
        {
            return false;
        }

        private bool EvaluateClickCoordinates(int x, int y)
        {
            if (CheckIfClickIsCorrect(x, y))
            {
                return true;
            }
            else
            {
                throw new Exception("CLICK: " + GenerateHexagonButtonName(x, y) + " OUTSIDE BOARD BOUNDARIES");
            }
        }

        private Board(int size, int[,] grid, List<Point> freeCells)
        {
            _size = size;
            _grid = new int[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    _grid[i, j] = grid[i, j];
                }
            }
            _freeCells = new List<Point>();
            freeCells.ForEach(cell => _freeCells.Add(new Point(cell.X, cell.Y)));
        }
    }
}
