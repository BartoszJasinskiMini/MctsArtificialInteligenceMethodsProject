using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Havannah.Logic.Board;

namespace Havannah.Logic
{
    public class ShapesStructure
    {
        public static int Size { set; private get; }
        private List<int> _corners;
        private List<int> _edges;
        private List<Point> _points;
        public int CornersCount { get { return _corners.Count; } }
        public int EdgesCount { get { return _edges.Count; } }

        public ShapesStructure()
        {
            _points = new List<Point>();
            _edges = new List<int>();
            _corners = new List<int>();
        }
        public ShapesStructure(List<int> corners, List<int> edges, List<Point> points)
        {
            _corners = new List<int>();
            corners.ForEach(corner => _corners.Add(corner));
            _edges = new List<int>();
            edges.ForEach(edge => _edges.Add(edge));
            _points = new List<Point>();
            points.ForEach(point => _points.Add(new Point(point.X, point.Y)));
        }
        public bool isNeightbour(int x, int y)
        {
            foreach(var point in _points)
            {
                if (Point.isNeighbour(point, new Point(x, y)))
                    return true;
            }
            return false;
        }
        public void AddPoint(Point point)
        {
            _points.Add(point);
            if (IfOnCorner(point, out int cornerNumber))
                _corners.Add(cornerNumber);
            if (IfOnEdge(point, out int edgeNumber) && !_edges.Contains(edgeNumber))
                _edges.Add(edgeNumber);
        }
        private bool IfOnEdge(Point point, out int edgeNumber)
        {
            edgeNumber = -1;
            if (point.X == 0 && point.Y > 0 && point.Y < Size - 1)
            {
                edgeNumber = 0;
                return true;
            }
            if (point.X > 0 && point.X < Size - 1 && point.Y == 0)
            {
                edgeNumber = 1;
                return true;
            }
            if (point.Y > Size - 1 && point.Y < 2 * Size - 2 && point.X == 2 * Size - 2)
            {
                edgeNumber = 2;
                return true;
            }
            if (point.X > Size - 1 && point.X < 2 * Size - 2 && point.Y == 2 * Size - 2)
            {
                edgeNumber = 3;
                return true;
            }
            for (int i = 0; i < Size - 2; i++)
            {
                if(point.X == 1 + i && point.Y == Size + i)
                {
                    edgeNumber = 4;
                    return true;
                }
                if (point.Y == 1 + i && point.X == Size + i)
                {
                    edgeNumber = 5;
                    return true;
                }
            }
            return false;
        }
        private bool IfOnCorner(Point point, out int cornerNumber)
        {
            cornerNumber = -1;
            if(point.X == 0 && point.Y == 0)
            {
                cornerNumber = 0;
                return true;
            }    
            if(point.X == Size - 1 && point.Y == 0)
            {
                cornerNumber = 1;
                return true;
            }
            if (point.Y == Size - 1 && point.X == 0)
            {
                cornerNumber = 2;
                return true;
            }
            if (point.Y == Size - 1 && point.X == 2 * Size - 2)
            {
                cornerNumber = 3;
                return true;
            }
            if (point.X == Size - 1 && point.Y == 2 * Size - 2)
            {
                cornerNumber = 4;
                return true;
            }
            if (point.X == 2 * Size - 2 && point.Y == 2 * Size - 2)
            {
                cornerNumber = 5;
                return true;
            }
            return false;
        }
        public ShapesStructure Clone()
        {
            return new ShapesStructure(_corners, _edges, _points);
        }
        public static ShapesStructure Merge(ShapesStructure s1, ShapesStructure s2)
        {
            foreach(var edge in s1._edges)
            {
                if (!s2._edges.Contains(edge))
                    s2._edges.Add(edge);
            }
            foreach(var corner in s1._corners)
            {
                if (!s2._corners.Contains(corner))
                    s2._corners.Add(corner);
            }
            foreach(var point in s1._points)
            {
                if (!s2._points.Contains(point))
                    s2._points.Add(point);
            }
            return s2;
        }
    }
}
