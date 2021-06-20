using System;
using Havannah.Logic;

namespace MctsArtificialIntelligenceMethods
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            monteCarloTreeSearch.RunAlgorithm(new Board(4), 10000);
        }
    }
}