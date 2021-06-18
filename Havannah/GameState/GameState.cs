using System;

using Havannah.Logic;

using static Havannah.GameState.Player;

namespace Havannah.GameState
{
    class GameState
    {

        //public GameStateElement[,] State { get; set; }
        public Board Board { get; set; }

        public GameState(int boardSize)
        {
           // State = new GameStateElement[2 * boardSize - 1, 2 * boardSize - 1];
            Board = new Board(boardSize);
        }

        public void MakeMove(int player, int x, int y)
        {
            Board.MakeMove(player, x, y);
        }

        public void ResetGameState()
        {
            Board.ResetBoard();
        }

     //   public void ResetGameState()
      //  {
       //     Array.Clear(State, 0, State.Length);
       // }

//        public void PrintBoard()
 //       {
 //           for (int i = 0; i < State.GetLength(0); i++)
  //          {
   //             for (int j = 0; j < State.GetLength(0); j++)
    //            {
     //               Console.Write(i + " : " + j + " " + State[i, j] + "   ");
      //          }
       //         Console.Write(Environment.NewLine + Environment.NewLine);
        //    }
       // }


        public void PrintBoard()
        {
            Board.PrintBoard();
        }

        public int CheckIfWon()
        {
            return Board.CheckIfWon();
        }

 //       public Player CheckIfOneOfThePlayersWonGame()
 //       {
 //           return None;
 //       }
    }
}
