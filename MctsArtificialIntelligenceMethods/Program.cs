using System;
using Havannah.Logic;

namespace MctsArtificialIntelligenceMethods
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*int boardSize = 4;
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            ShapesStructure.Size = boardSize;
            Point res = monteCarloTreeSearch.RunAlgorithm(new Board(boardSize), 10000);
            int k = 0;*/
            Test1();
            Test2();
        }

        public static void Test1()
        {
            
            int boardSize = 4;
            ShapesStructure.Size = boardSize;
            Board board = new Board(boardSize);
            board.MakeMove(1, new Point(0, 1));
            board.MakeMove(1, new Point(1, 1));
            board.MakeMove(1, new Point(1, 0));
            board.MakeMove(1, new Point(2, 1));
            //board.MakeMove(1, new Point(2, 2));
            board.MakeMove(1, new Point(2, 3));
            board.MakeMove(1, new Point(2, 4));
            board.MakeMove(1, new Point(2, 5));

            board.MakeMove(1, new Point(2, 2));
            bool t = board.CheckIfWon(1);
        }

        public static void Test2()
        {
            int boardSize = 4;
            ShapesStructure.Size = boardSize;
            Board board = new Board(boardSize);
            board.MakeMove(1, new Point(0, 0));
            board.MakeMove(1, new Point(0, 1));
            board.MakeMove(1, new Point(0, 2));
            board.MakeMove(1, new Point(0, 3));

            bool t = board.CheckIfWon(1);
        }
    }
}