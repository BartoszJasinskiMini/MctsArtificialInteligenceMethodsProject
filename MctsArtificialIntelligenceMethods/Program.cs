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
            int boardSize = 5;
            double timePerMove = 1000;
            ShapesStructure.Size = boardSize;

            /*            for (int i = 0; i < 20; i++)
                        {
                            RaveVsHybridPlayouts(boardSize, timePerMove, true, "logs.txt");
                            RaveVsHybridPlayouts(boardSize, timePerMove, false, "logs.txt");
                        }*/
            for (int i = 0; i < 20; i++)
            {
                RaveVsSmartPlayouts(boardSize, timePerMove, true, "logs.txt");
                RaveVsSmartPlayouts(boardSize, timePerMove, false, "logs.txt");
            }
            for (int i = 0; i < 20; i++)
            {
                NormalVsHybridAlgorithm(boardSize, timePerMove, true, "logs.txt");
                NormalVsHybridAlgorithm(boardSize, timePerMove, false, "logs.txt");
            }
            for (int i = 0; i < 20; i++)
            {
                NormalVsRave(boardSize, timePerMove, true, "logs.txt");
                NormalVsRave(boardSize, timePerMove, false, "logs.txt");
            }
            for (int i = 0; i < 20; i++)
            {
                NormalVsSmartPlayouts(boardSize, timePerMove, true, "logs.txt");
                NormalVsSmartPlayouts(boardSize, timePerMove, false, "logs.txt");
            }
            for (int i = 0; i < 20; i++)
            {
                HybridVsSmartPlayouts(boardSize, timePerMove, true, "logs.txt");
                HybridVsSmartPlayouts(boardSize, timePerMove, false, "logs.txt");
            }


        }

        public static void RaveVsHybridPlayouts(int boardSize, double timePerMove, bool ifRaveFirst, string outPutFileName)
        {
            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            Board board = new Board(boardSize);
            string line = "RaveVsHybrid" + " Rave first: " + ifRaveFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
                Point move = null;
                if (ifRaveFirst)
                {
                    move = raveAlgorithm.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = hybridAlgorithm.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifRaveFirst ? 1 : 2, move);
                ifRaveFirst = !ifRaveFirst;
                if (board.CheckIfWon(1))
                {
                    line += " -> Rave Algorithm wins ";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Hybrid Algorithm wins ";
                    break;
                }
                if (board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }

        }
        public static void RaveVsSmartPlayouts(int boardSize, double timePerMove, bool ifRaveFirst, string outPutFileName)
        {
            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Board board = new Board(boardSize);
            string line = "RaveVsSmart" + " Rave first: " + ifRaveFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
                Point move = null;
                if (ifRaveFirst)
                {
                    move = raveAlgorithm.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = smartPlayouts.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifRaveFirst ? 1 : 2, move);
                ifRaveFirst = !ifRaveFirst;
                if (board.CheckIfWon(1))
                {
                    line += " -> Rave Algorithm wins ";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Smart Playouts wins ";
                    break;
                }
                if (board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }

        }
        public static void NormalVsHybridAlgorithm(int boardSize, double timePerMove, bool ifNormalFirst, string outPutFileName)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            Board board = new Board(boardSize);
            string line = "NormalVsHybrid" + " Normal first: " + ifNormalFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
                Point move = null;
                if (ifNormalFirst)
                {
                    move = monteCarloTreeSearch.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = hybridAlgorithm.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifNormalFirst ? 1 : 2, move);
                ifNormalFirst = !ifNormalFirst;
                if (board.CheckIfWon(1))
                {
                    line += " -> Normal Algorithm wins ";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Hybrid Algorithm wins ";
                    break;
                }
                if (board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }
        }

        public static void NormalVsSmartPlayouts(int boardSize, double timePerMove, bool ifNormalFirst, string outPutFileName)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Board board = new Board(boardSize);
            string line = "NormalVsSmart" + " Normal first: " + ifNormalFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
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
                    line += " -> Normal Algorithm wins ";
                    break;
                }
                if(board.CheckIfWon(2))
                {
                    line += " -> Smart Playouts wins ";
                    break;
                }
                if(board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }
        }

        public static void NormalVsRave(int boardSize, double timePerMove, bool ifNormalFirst, string outPutFileName)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            Board board = new Board(boardSize);
            string line = "NormalVsRave" + " Normal first: " + ifNormalFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
                Point move = null;
                if (ifNormalFirst)
                {
                    move = monteCarloTreeSearch.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = raveAlgorithm.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifNormalFirst ? 1 : 2, move);
                ifNormalFirst = !ifNormalFirst;
                if (board.CheckIfWon(1))
                {
                    line += " -> Normal Algorithm wins ";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Rave Algorithm wins ";
                    break;
                }
                if (board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }
        }
        public static void HybridVsSmartPlayouts(int boardSize, double timePerMove, bool ifRaveFirst, string outPutFileName)
        {
            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Board board = new Board(boardSize);
            string line = "HybridVsSmart" + " Rave first: " + ifRaveFirst.ToString();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int movesCount = 0;
            while (board.FreeCells.Count > 0)
            {
                movesCount++;
                Point move = null;
                if (ifRaveFirst)
                {
                    move = hybridAlgorithm.RunAlgorithm(board, timePerMove);
                }
                else
                {
                    move = smartPlayouts.RunAlgorithm(board, timePerMove);
                }
                board.MakeMove(ifRaveFirst ? 1 : 2, move);
                ifRaveFirst = !ifRaveFirst;
                if (board.CheckIfWon(1))
                {
                    line += " -> Hybrid Algorithm wins ";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Smart Playouts wins ";
                    break;
                }
                if (board.CheckIfDraw())
                {
                    line += " -> Draw ";
                    break;
                }
            }
            stopwatch.Stop();
            line += stopwatch.Elapsed.TotalSeconds.ToString() + " ";
            line += movesCount.ToString();
            Console.WriteLine(line);
            using (StreamWriter sw = File.AppendText(outPutFileName))
            {
                sw.WriteLine(line);
            }

        }
    }
}