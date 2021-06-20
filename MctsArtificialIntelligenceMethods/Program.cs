using System;
using Havannah.Logic;

namespace MctsArtificialIntelligenceMethods
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int boardSize = 4;
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            ShapesStructure.Size = boardSize;
            Point res = monteCarloTreeSearch.RunAlgorithm(new Board(boardSize), 10000);
            int k = 0;
        }
    }
}