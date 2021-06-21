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
            Point res = monteCarloTreeSearch.RunAlgorithm(new Board(boardSize), 1000000);
            Node lastnode = monteCarloTreeSearch._gameTree.Root;
            while(!lastnode.isLeaf)
            {
                lastnode = lastnode.Children[0];
            }
            lastnode.Board.PrintBoard();
            int k = 0;

            /*Console.WriteLine("Test numer 1: " + Test1().ToString());
            Console.WriteLine("Test numer 2: " + Test2().ToString());
            Console.WriteLine("Test numer 3: " + Test3().ToString());
            Console.WriteLine("Test numer 4: " + Test4().ToString());*/

        }

        public static bool Test1()
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
            return board.CheckIfWon(1);
        }

        public static bool Test2()
        {
            int boardSize = 4;
            ShapesStructure.Size = boardSize;
            Board board = new Board(boardSize);
            board.MakeMove(1, new Point(0, 0));
            board.MakeMove(1, new Point(0, 1));
            board.MakeMove(1, new Point(0, 2));
            board.MakeMove(1, new Point(0, 3));

            return board.CheckIfWon(1);
        }
        public static bool Test3()
        {
            int boardSize = 4;
            ShapesStructure.Size = boardSize;
            Board board = new Board(boardSize);
            board.MakeMove(1, new Point(1, 1));
            board.MakeMove(1, new Point(1, 2));
            board.MakeMove(1, new Point(2, 2));
            board.MakeMove(1, new Point(2, 3));
            board.MakeMove(1, new Point(3, 3));
            board.MakeMove(1, new Point(3, 4));

            return board.CheckIfWon(1);
        }
        public static bool Test4()
        {
            int boardSize = 4;
            ShapesStructure.Size = boardSize;
            Board board = new Board(boardSize);
            board.MakeMove(1, new Point(1, 1));
            board.MakeMove(1, new Point(1, 2));
            board.MakeMove(1, new Point(2, 1));
            board.MakeMove(1, new Point(2, 3));
            board.MakeMove(1, new Point(3, 3));
            board.MakeMove(1, new Point(3, 2));

            return board.CheckIfWon(1);
        }
    }
}