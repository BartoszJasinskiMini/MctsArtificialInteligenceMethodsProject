using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MctsArtificialIntelligenceMethods
{
    class GameTree
    {
        public Node Root { get; private set; }

        public GameTree(Board board)
        {
            Root = new Node(null, board);
        }
    }
}
