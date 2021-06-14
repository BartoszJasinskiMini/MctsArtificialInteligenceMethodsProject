using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MctsArtificialIntelligenceMethods
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
        public bool isChild { get; set; }
        public List<Node> Children { get; private set; }
        public Node Parent { get; private set; }

    }
}
