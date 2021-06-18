using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MctsArtificialIntelligenceMethods
{
    class MonteCarloTreeSearch
    {
        private const int _player1 = 1;
        private const int _player2 = 2;
        private GameTree _gameTree;
        private List<Node> _path;
        private Node Selection()
        {
            Node currentNode = _gameTree.Root;
            _path.Add(currentNode);
            while(currentNode.isLeaf)
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
        private void Simulation(Node node, int timeLeft)
        {
            

        }
        private void Backpropagation()
        {

        }
    }
}
