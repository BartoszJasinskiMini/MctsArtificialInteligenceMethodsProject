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
        private int[,] _grid;
        private int _size;
        public List<Point> FreeCells { get; private set; }
        private List<ShapesStructure> _playerStructures;
        private List<ShapesStructure> _oponentStructues;

        public Board(int size)
        {
            _size = size;
            _grid = new int[2 * size - 1, 2 * size - 1];
            FreeCells = new List<Point>();
            _playerStructures = new List<ShapesStructure>();
            _oponentStructues = new List<ShapesStructure>();
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if(CheckIfClickIsCorrect(i, j))
                    {
                        _grid[i, j] = 0;
                        FreeCells.Add(new Point(i, j));
                    }
                    else
                    {
                        _grid[i, j] = -1;
                    }
                }
            }
        }
        public int CheckIfWon()
        {
            if (CheckIfWon(1))
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
        public bool CheckIfDraw() { return FreeCells.Count == 0; }
        public Board Clone()
        {
            return new Board(_size, _grid, FreeCells, _playerStructures, _oponentStructues);
        }
        public bool MakeRandomMove(int player, out Point move)
        {
            move = null;
            if (FreeCells.Count <= 0) return false;
            Random random = new Random();
            int randomPosition = random.Next(0, FreeCells.Count);
            Point randomPoint = FreeCells[randomPosition];
            move = randomPoint.Clone();
            MakeMove(player, randomPoint.X, randomPoint.Y);
            return true;
        }
        public bool MakeMove(int player, Point point)
        {
            return MakeMove(player, point.X, point.Y);
        }
        public bool MakeSmartMove(int player, out Point move)
        {
            move = null;
            if (FreeCells.Count <= 0) return false;
            for (int i = 0; i < FreeCells.Count; i++)
            {
                if(HasNeighbours(FreeCells[i]))
                {
                    move = FreeCells[i].Clone();
                    MakeMove(player, move.X, move.Y);
                    return true;
                }
            }
            return false;
        }
        public bool MakeMove(int player, int x, int y)
        {
            EvaluateClickCoordinates(x, y);

            if (_grid[x, y] == 0)
            {
                _grid[x, y] = player;
                FreeCells.Remove(new Point(x, y));
                if (player == 1)
                {
                    ExpandStructures(x, y, _playerStructures);
                }
                if (player == 2)
                {
                    ExpandStructures(x, y, _oponentStructues);
                }
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
                    //Console.Write("[" + GenerateHexagonButtonName(i, j) + "] = " + _grid[i, j] + "   ");
                    Console.Write("[" + _grid[i, j] + "] = "  + "   ");
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
            List<ShapesStructure> structures = player == 1 ? _playerStructures : _oponentStructues;
            foreach (var st in _playerStructures)
            {
                bool change = true;
                ShapesStructure shapes = st.Clone();
                while (change)
                {
                    change = false;
                    for (int i = shapes.Points.Count - 1; i >= 0; i--)
                    {
                        int neighbourCount = 0;
                        List<int> neighbourIndexes = new List<int>();
                        for (int j = shapes.Points.Count - 1; j >= 0; j--)
                        {
                            if (i != j && Point.isNeighbour(shapes.Points[i], shapes.Points[j]))
                            {
                                neighbourCount++;
                                neighbourIndexes.Add(j);
                            }
                        }
                        if (neighbourCount > 2 || (neighbourCount == 2 && !Point.isNeighbour(shapes.Points[neighbourIndexes[0]], shapes.Points[neighbourIndexes[1]])))
                            continue;
                        else
                        {
                            shapes.Points.RemoveAt(i);
                            change = true;
                        }
                    }
                }
                if (shapes.Points.Count >= 6)
                    return true;
            }
            return false;
        }
        private bool CheckIfBridge(int player)
        {
            if(player == 1)
            {
                foreach(var st in _playerStructures)
                {
                    if (st.CornersCount == 2)
                        return true;
                }
            }
            if (player == 2)
            {
                foreach (var st in _oponentStructues)
                {
                    if (st.CornersCount == 2)
                        return true;
                }
            }
            return false;
        }
        private bool CheckIfFork(int player)
        {
            if(player == 1)
            {
                foreach(var st in _playerStructures)
                {
                    if (st.EdgesCount == 3)
                        return true;
                }
            }
            if(player == 2)
            {
                foreach (var st in _oponentStructues)
                {
                    if (st.EdgesCount == 3)
                        return true;
                }
            }
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
        private void ExpandStructures(int x, int y, List<ShapesStructure> structures)
        {
            List<ShapesStructure> neighbouringStructures = new List<ShapesStructure>();
            for (int i = structures.Count - 1; i >= 0; i--)
            {
                if(structures[i].isNeightbour(x, y))
                {
                    neighbouringStructures.Add(structures[i]);
                    structures.RemoveAt(i);
                }
            }
            if(neighbouringStructures.Count == 0)
            {
                ShapesStructure structure = new ShapesStructure();
                structure.AddPoint(new Point(x, y));
                structures.Add(structure);
                return;
            }
            while(neighbouringStructures.Count > 1)
            {
                int lastIndex = neighbouringStructures.Count - 1;
                ShapesStructure s1 = neighbouringStructures[lastIndex];
                neighbouringStructures.RemoveAt(lastIndex);
                lastIndex--;
                ShapesStructure s2 = neighbouringStructures[lastIndex];
                neighbouringStructures.RemoveAt(lastIndex);
                neighbouringStructures.Add(ShapesStructure.Merge(s1, s2));
            }
            neighbouringStructures[neighbouringStructures.Count - 1].AddPoint(new Point(x, y));
            structures.Add(neighbouringStructures[neighbouringStructures.Count - 1]);
        }
        private Board(int size, int[,] grid, List<Point> freeCells, List<ShapesStructure> playerStructures, List<ShapesStructure> opponentStructures)
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
            FreeCells = new List<Point>();
            freeCells.ForEach(cell => FreeCells.Add(new Point(cell.X, cell.Y)));
            _playerStructures = new List<ShapesStructure>();
            playerStructures.ForEach(structure => _playerStructures.Add(structure.Clone()));
            _oponentStructues = new List<ShapesStructure>();
            opponentStructures.ForEach(structure => _oponentStructues.Add(structure.Clone()));
        }
        private bool HasNeighbours(Point point)
        {
            if (point.X - 1 >= 0 && _grid[point.X - 1, point.Y] > 0)
                return true;
            if (point.Y - 1 >= 0 && _grid[point.X, point.Y - 1] > 0)
                return true;
            if (point.Y - 1 >= 0 && point.X - 1 >= 0 && _grid[point.X - 1, point.Y - 1] > 0)
                return true;
            if (point.X + 1 <= 2 * _size - 2 && _grid[point.X + 1, point.Y] > 0)
                return true;
            if (point.Y + 1 <= 2 * _size - 2 && _grid[point.X, point.Y + 1] > 0)
                return true;
            if (point.Y + 1 <= 2 * _size - 2 && point.X + 1 <= 2 * _size - 2 && _grid[point.X + 1, point.Y + 1] > 0)
                return true;
            return false;
        }
    }
}
