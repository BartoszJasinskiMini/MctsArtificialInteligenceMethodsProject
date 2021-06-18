using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havannah.Logic
{
    enum Result {  Win, Loss, Draw, Timeout }
    class MonteCarloTreeSearch
    {
        private const int _player1 = 1;
        private const int _player2 = 2;
        private const double _minTimeLeft = 1000;
        private GameTree _gameTree;
        private List<Node> _path;
        private Node Selection()
        {
            Node currentNode = _gameTree.Root;
            _path.Add(currentNode);
            while (currentNode.isLeaf)
            {
                currentNode = currentNode.GetNextNode();
                _path.Add(currentNode);
            }
            return currentNode;
        }
        private Node Expansion(Node leafNode)
        {
            Node result = leafNode;
            if (!leafNode.Board.CheckIfWin(_player1) && !leafNode.Board.CheckIfWin(_player2) && !leafNode.Board.CheckIfDraw())
            {
                Board childBoard = leafNode.Board.Clone();
                if (childBoard.MakeRandomMove(leafNode.WhichPlayerMoves == _player1 ? _player2 : _player1))
                {
                    Node childNode = new Node(leafNode, childBoard, leafNode.WhichPlayerMoves == _player1 ? _player2 : _player1);
                    if (!leafNode.Children.Contains(childNode))
                        leafNode.Children.Add(childNode);
                    result = childNode;
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
                board.MakeRandomMove(player);
                if (board.CheckIfDraw())
                {
                    return Result.Draw;
                }
                if (board.CheckIfWin(_player1))
                {
                    return Result.Win;
                }
                if (board.CheckIfWin(_player2))
                {
                    return Result.Loss;
                }
                stopwatch.Stop();
                timeLeft -= stopwatch.Elapsed.TotalMilliseconds;
            }
            return Result.Timeout;

        }
        private void Backpropagation(Result result)
        {
            foreach(var node in _path)
            {
                node.AddGame(result == Result.Win);
            }
        }
    }
}
