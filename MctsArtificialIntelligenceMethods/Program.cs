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
            double timePerMove = 10000;
            ShapesStructure.Size = boardSize;
            Point res = null;

/*            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            res = hybridAlgorithm.RunAlgorithm(new Board(boardSize), timePerMove);*/

/*            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            res = monteCarloTreeSearch.RunAlgorithm(new Board(boardSize), timePerMove);

            SmartPlayouts smartPlayouts = new SmartPlayouts();
            res = smartPlayouts.RunAlgorithm(new Board(boardSize), timePerMove);

            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            res = raveAlgorithm.RunAlgorithm(new Board(boardSize), timePerMove);*/

            int gamesCount = 10;
            int boardSizes = 3;
            int times = 3;

            /*            for (int i = 0; i < gamesCount; i++)
                        {
                            for (int j = 1; j < boardSizes; j++)
                            {
                                ShapesStructure.Size = 4 + j * 2;
                                RaveVsSmartPlayouts(4 + j * 2, 1000.0, true, "out.txt");
                            }
                            for (int k = 0; k < times; k+=2)
                            {
                                ShapesStructure.Size = 4;
                                RaveVsSmartPlayouts(4, 500.0 + k * 500.0, true, "out.txt");
                            }
                        }
                        for (int i = 0; i < gamesCount; i++)
                        {
                            for (int j = 1; j < boardSizes; j++)
                            {
                                ShapesStructure.Size = 4 + j * 2;
                                RaveVsSmartPlayouts(4 + j * 2, 1000.0, false, "out.txt");
                            }
                            for (int k = 0; k < times; k+=2)
                            {
                                ShapesStructure.Size = 4;
                                RaveVsSmartPlayouts(4, 500.0 + k * 500.0, false, "out.txt");
                            }
                        }*/

            /*            for (int i = 0; i < gamesCount; i++)
                        {
                            for (int j = 1; j < boardSizes; j++)
                            {
                                ShapesStructure.Size = 4 + j * 2;
                                NormalVsSmartPlayouts(4 + j * 2, 1000.0, true, "out.txt");
                            }
                            for (int k = 0; k < times; k += 2)
                            {
                                ShapesStructure.Size = 4;
                                NormalVsSmartPlayouts(4, 500.0 + k * 500.0, true, "out.txt");
                            }
                        }
                        for (int i = 0; i < gamesCount; i++)
                        {
                            for (int j = 1; j < boardSizes; j++)
                            {
                                ShapesStructure.Size = 4 + j * 2;
                                NormalVsSmartPlayouts(4 + j * 2, 1000.0, false, "out.txt");
                            }
                            for (int k = 0; k < times; k += 2)
                            {
                                ShapesStructure.Size = 4;
                                NormalVsSmartPlayouts(4, 500.0 + k * 500.0, false, "out.txt");
                            }
                        }*/

            for (int i = 0; i < gamesCount; i++)
            {
                for (int j = 1; j < boardSizes; j++)
                {
                    ShapesStructure.Size = 4 + j * 2;
                    RaveVsHybridPlayouts(4 + j * 2, 1000.0, true, "out.txt");
                }
                for (int k = 0; k < times; k++)
                {
                    ShapesStructure.Size = 4;
                    RaveVsHybridPlayouts(4, 500.0 + k * 500.0, true, "out.txt");
                }
            }
            for (int i = 0; i < gamesCount; i++)
            {
                for (int j = 1; j < boardSizes; j++)
                {
                    ShapesStructure.Size = 4 + j * 2;
                    RaveVsHybridPlayouts(4 + j * 2, 1000.0, false, "out.txt");
                }
                for (int k = 0; k < times; k++)
                {
                    ShapesStructure.Size = 4;
                    RaveVsHybridPlayouts(4, 500.0 + k * 500.0, false, "out.txt");
                }
            }

            for (int i = 0; i < gamesCount; i++)
            {
                for (int j = 1; j < boardSizes; j++)
                {
                    ShapesStructure.Size = 4 + j * 2;
                    NormalVsHybridAlgorithm(4 + j * 2, 1000.0, true, "out2.txt");
                }
                for (int k = 0; k < times; k++)
                {
                    ShapesStructure.Size = 4;
                    NormalVsHybridAlgorithm(4, 500.0 + k * 500.0, true, "out2.txt");
                }
            }
            for (int i = 0; i < gamesCount; i++)
            {
                for (int j = 1; j < boardSizes; j++)
                {
                    ShapesStructure.Size = 4 + j * 2;
                    NormalVsHybridAlgorithm(4 + j * 2, 1000.0, false, "out2.txt");
                }
                for (int k = 0; k < times; k++)
                {
                    ShapesStructure.Size = 4;
                    NormalVsHybridAlgorithm(4, 500.0 + k * 500.0, false, "out2.txt");
                }
            }
        }

        public static void RaveVsHybridPlayouts(int boardSize, double timePerMove, bool ifRaveFirst, string outPutFileName)
        {
            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            Board board = new Board(boardSize);
            string line = "Board size: " + boardSize.ToString() + " Time per move: " + timePerMove.ToString() + " Rave first: " + ifRaveFirst.ToString();
            while (board.FreeCells.Count > 0)
            {
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
                    line += " -> Rave Algorithm wins";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Hybrid Algorithm wins";
                    break;
                }
                if (board.CheckIfDraw())
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

        public static void RaveVsSmartPlayouts(int boardSize, double timePerMove, bool ifRaveFirst, string outPutFileName)
        {
            RaveAlgorithm raveAlgorithm = new RaveAlgorithm();
            SmartPlayouts smartPlayouts = new SmartPlayouts();
            Board board = new Board(boardSize);
            string line = "Board size: " + boardSize.ToString() + " Time per move: " + timePerMove.ToString() + " Rave first: " + ifRaveFirst.ToString();
            while (board.FreeCells.Count > 0)
            {
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
                    line += " -> Rave Algorithm wins";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Smart Playouts wins";
                    break;
                }
                if (board.CheckIfDraw())
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

        public static void NormalVsHybridAlgorithm(int boardSize, double timePerMove, bool ifNormalFirst, string outPutFileName)
        {
            MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();
            HybridAlgorithm hybridAlgorithm = new HybridAlgorithm();
            Board board = new Board(boardSize);
            string line = "Board size: " + boardSize.ToString() + " Time per move: " + timePerMove.ToString() + " Normal first: " + ifNormalFirst.ToString();
            while (board.FreeCells.Count > 0)
            {
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
                    line += " -> Normal Algorithm wins";
                    break;
                }
                if (board.CheckIfWon(2))
                {
                    line += " -> Hybrid Algorithm wins";
                    break;
                }
                if (board.CheckIfDraw())
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
    }
}