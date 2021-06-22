using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havannah.Logic
{
    public class RaveNode
    {

        public Point Point { get; private set; }
        private int _wins;
        private int _games;
        public double Score { get { return _games > 0 ? _wins / (double)_games : 0.0; } }
        public void AddGame(bool ifWon)
        {
            _games++;
            _wins += ifWon ? 1 : 0;
        }

        public RaveNode(Point point)
        {
            _wins = 0;
            _games = 0;
            Point = point;
        }

    }
}
