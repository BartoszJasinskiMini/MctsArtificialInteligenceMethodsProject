using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havannah.Logic
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
        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }
        public Point Clone()
        {
            return new Point(X, Y);
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        static public bool isNeighbour(Point p1, Point p2)
        {
            return (p1.X == p2.X && Math.Abs(p1.Y - p2.Y) == 1) 
                || (p1.Y == p2.Y && Math.Abs(p1.X - p2.X) == 1) 
                || (p1.X - p2.X == 1 && p1.Y - p2.Y == 1) 
                || (p1.X - p2.X == -1 && p1.Y - p2.Y == -1);
        }
    }
}
