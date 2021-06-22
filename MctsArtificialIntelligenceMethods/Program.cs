using System;
using System.Diagnostics;
using System.IO;
using Havannah.Logic;

namespace MctsArtificialIntelligenceMethods
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int boardSize = 4;
            ShapesStructure.Size = boardSize;

            /*MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Point res = monteCarloTreeSearch.RunAlgorithm(new Board(boardSize), 1000);
            Point res = smartPlayouts.RunAlgorithm(new Board(boardSize), 10000);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds.ToString());*/

            int gamesCount = 10;
            int boardSizes = 3;
            int times = 3;

            for (int i = 0; i < gamesCount; i++)
            {
                for (int j = 0; j < boardSizes; j++)
                {
                    ShapesStructure.Size = 4 + j * 2;
                    NormalVsSmartPlayouts(4 + j * 2, 1000.0, false, "out.txt");
                }
                for (int k = 0; k < times; k++)
                {
                    ShapesStructure.Size = 4;
                    NormalVsSmartPlayouts(4, 500.0 + k * 500.0, false, "out.txt");
                }
            }

            /*            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
                        Point res = raveAlgorithm.RunAlgorithm(new Board(boardSize), 10000);
                        int k = 0;*/

            /*Console.WriteLine("Test numer 1: " + Test1().ToString());
            Console.WriteLine("Test numer 2: " + Test2().ToString());
            Console.WriteLine("Test numer 3: " + Test3().ToString());
            Console.WriteLine("Test numer 4: " + Test4().ToString());*/

        }

        public static void NormalVsSmartPlayouts(int boardSize, double timePerMove, bool ifNormalFirst, string outPutFileName)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Board board = new Board(boardSize);
            string line = "Board size: " + boardSize.ToString() + " Time per move: " + timePerMove.ToString() + " Normal first: " + ifNormalFirst.ToString();
            while(board.FreeCells.Count > 0)
            {
                Point move = null;
                if(ifNormalFirst)
                {
                    move = monteCarloTreeSearch.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = smartPlayouts.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifNormalFirst ? 1 : 2, move);
                ifNormalFirst = !ifNormalFirst;
                if(board.CheckIfWon(1))
                {
                    line += " -> Normal Algorithm wins";
                    break;
                }
                if(board.CheckIfWon(2))
                {
                    line += " -> Smart Playouts wins";
                    break;
                }
                if(board.CheckIfDraw())
                {
                    line += " -> Draw";
                    break;
                }
            }
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }

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