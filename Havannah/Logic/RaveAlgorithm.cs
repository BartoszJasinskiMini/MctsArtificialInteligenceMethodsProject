using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havannah.Logic
{
    public class RaveAlgorithm
    {
        private const double alpha = 0.95;
        private const int _player1 = 1;
        private const int _player2 = 2;
        private const double _minTimeLeft = 100;
        public GameTree _gameTree;
        private List<Node> _path;

        private List<RaveNode> _raveNodes;

        public Point RunAlgorithm(Board board, double time)
        {
            time = alpha * time;
            _gameTree = new GameTree(board, _player1);
            _path = new List<Node>();
            _raveNodes = new List<RaveNode>();
            while (time > 0)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Backpropagation(Simulation(Expansion(Selection()), time));
                stopwatch.Stop();
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
                currentNode = currentNode.GetNextRaveNode(_raveNodes);
                if (currentNode.Parent != null)
                    _path.Add(currentNode);
            }
            return currentNode;
        }
        private Node Expansion(Node leafNode)
        {
            Node result = leafNode;
            if (!leafNode.Board.CheckIfWon(_player1) && !leafNode.Board.CheckIfWon(_player2) && !leafNode.Board.CheckIfDraw())
            {
                if (leafNode.NotMadeMoves.Count > 0)
                {
                    Board childBoard = leafNode.Board.Clone();
                    Random random = new Random();
                    Point nextMove = leafNode.NotMadeMoves[random.Next(0, leafNode.NotMadeMoves.Count)];
                    leafNode.NotMadeMoves.Remove(nextMove);
                    childBoard.MakeMove(leafNode.WhichPlayerMoves, nextMove);
                    Node childNode = new Node(leafNode, childBoard, leafNode.WhichPlayerMoves == _player1 ? _player2 : _player1);
                    childNode.SetMove(nextMove.X, nextMove.Y);
                    leafNode.Children.Add(childNode);
                    result = childNode;
                    _path.Add(result);
                }
            }
            return result;
        }
        private Result Simulation(Node node, double timeLeft)
        {
            Board board = node.Board.Clone();
            int player = node.WhichPlayerMoves;
            while (timeLeft > _minTimeLeft)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                board.MakeRandomMove(player, out Point move);

                if (board.CheckIfWon(_player1))
                {
                    return Result.Win;
                }
                else if (board.CheckIfWon(_player2))
                {
                    return Result.Loss;
                }
                else if (board.CheckIfDraw())
                {
                    return Result.Draw;
                }
                stopwatch.Stop();
                timeLeft -= stopwatch.Elapsed.TotalMilliseconds;
                player = player == _player1 ? _player2 : _player1;
            }
            return Result.Timeout;

        }
        private void Backpropagation(Result result)
        {
            foreach (var node in _path)
            {
                node.AddGame(result == Result.Win);
                if (node.Move != null)
                {
                    RaveNode raveNode = _raveNodes.FindLast(r => r.Point.Equals(node.Move));
                    if (raveNode != null)
                    {
                        raveNode.AddGame(node.WhichPlayerMoves != _player1);
                    }
                    else
                    {
                        _raveNodes.Add(new RaveNode(node.Move));
                    } 
                }
            }
            _path.Clear();
        }
    }
}
