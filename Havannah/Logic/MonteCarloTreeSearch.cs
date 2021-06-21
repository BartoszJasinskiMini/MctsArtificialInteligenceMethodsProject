using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Havannah.Logic.Board;

namespace Havannah.Logic
{
    enum Result {  Win, Loss, Draw, Timeout }
    public class MonteCarloTreeSearch
    {
        private const double alpha = 0.95;
        private const int howManyChildren = 5;
        private const int _player1 = 1;
        private const int _player2 = 2;
        private const double _minTimeLeft = 1000;
        public GameTree _gameTree;
        private List<Node> _path;

        public Point RunAlgorithm(Board board, double time)
        {
            Stopwatch stopwatch = new Stopwatch();
            time = alpha * time;
            _gameTree = new GameTree(board, _player1);
            _path = new List<Node>();
            while(time > 0)
            {
                stopwatch.Start();
                Backpropagation(Simulation(Expansion(Selection()), time));
                stopwatch.Start();
                time -= stopwatch.Elapsed.TotalMilliseconds;
            }
            return _gameTree.GetBestNode().Move;
        }
        private Node Selection()
        {
            Node currentNode = _gameTree.Root;
            _path.Add(currentNode);
            while (!currentNode.isLeaf)
            {
                currentNode = currentNode.GetNextNode();
                _path.Add(currentNode);
            }
            return currentNode;
        }
        private Node Expansion(Node leafNode)
        {
            Node result = leafNode;
            if (!leafNode.Board.CheckIfWon(_player1) && !leafNode.Board.CheckIfWon(_player2) && !leafNode.Board.CheckIfDraw())
            {
                Board childBoard = leafNode.Board.Clone();
                for (int i = 0; i < howManyChildren; i++)
                {
                    if (childBoard.MakeRandomMove(leafNode.WhichPlayerMoves == _player1 ? _player2 : _player1, out Point move))
                    {
                        Node childNode = new Node(leafNode, childBoard, leafNode.WhichPlayerMoves == _player1 ? _player2 : _player1);
                        childNode.SetMove(move.X, move.Y);
                        if (!leafNode.Children.Contains(childNode))
                            leafNode.Children.Add(childNode);
                        result = childNode;
                    }
                }
            }
            _path.Add(result);
            return result;
        }
        private Result Simulation(Node node, double timeLeft)
        {
            Stopwatch stopwatch = new Stopwatch();
            Board board = node.Board.Clone();
            int player = node.WhichPlayerMoves == _player1 ? _player2 : _player1;
            while (timeLeft > _minTimeLeft)
            {
                stopwatch.Start();
                board.MakeRandomMove(player, out Point move);
                if (board.CheckIfDraw())
                {
                    return Result.Draw;
                }
                if (board.CheckIfWon(_player1))
                {
                    return Result.Win;
                }
                if (board.CheckIfWon(_player2))
                {
                    return Result.Loss;
                }
                stopwatch.Stop();
                timeLeft -= stopwatch.Elapsed.TotalMilliseconds;
                player = player == _player1 ? _player2 : _player1;
            }
            return Result.Timeout;

        }
        private void Backpropagation(Result result)
        {
            Console.WriteLine(result.ToString());
            foreach(var node in _path)
            {
                node.AddGame(result == Result.Win);
            }
            _path.Clear();
        }
    }
}

