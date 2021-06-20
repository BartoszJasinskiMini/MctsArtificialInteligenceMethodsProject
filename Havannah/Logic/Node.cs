using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Havannah.Logic.Board;

namespace Havannah.Logic
{
    class Node
    {
        public int Wins { get; private set; }
        public int AllGames { get; private set; }
        public double WinRatio { 
            get {
                if (AllGames > 0)
                    return (double)Wins / AllGames;
                return 0.0;
            }
        }
        public bool isLeaf { get { return Children.Count == 0; } }
        public List<Node> Children { get; private set; }
        public Node Parent { get; private set; }
        public Board Board { get; private set; }
        public Point Move { get; private set; }
        public int WhichPlayerMoves { get; private set; }

        public void AddGame(bool ifWon)
        {
            AllGames++;
            Wins += ifWon ? 1 : 0;
        }
        public Node(Node parent, Board board, int player)
        {
            Wins = 0;
            AllGames = 0;
            Parent = parent;
            Children = new List<Node>();
            Board = board;
            WhichPlayerMoves = player;
        }
        public void SetMove(int x, int y)
        {
            Move = new Point(x, y);
        }
        public Node GetNextNode()
        {
            if (Children.Count == 0) return this;
            double[] scores = new double[Children.Count];
            double maxValue = 0.0;
            int index = -1;
            for (int i = 0; i < Children.Count; i++)
            {
                double value = Children[i].WinRatio;
                scores[i] = value;
                if(value >= maxValue)
                {
                    maxValue = value;
                    index = i;
                }
            }
            return Children[index];
            
        }
    }
}
