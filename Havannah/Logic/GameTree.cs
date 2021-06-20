using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Havannah.Logic.Board;

namespace Havannah.Logic
{
    class GameTree
    {
        public Node Root { get; private set; }

        public GameTree(Board board, int player)
        {
            Root = new Node(null, board, player);
        }
        public Node GetBestNode()
        {
            int maxGamesPlayed = 0;
            int index = -1;
            for (int i = 0; i < Root.Children.Count; i++)
            {
                if(Root.Children[i].AllGames > maxGamesPlayed)
                {
                    maxGamesPlayed = Root.Children[i].AllGames;
                    index = i;
                }
            }
            return Root.Children[index];
        }
    }
}
