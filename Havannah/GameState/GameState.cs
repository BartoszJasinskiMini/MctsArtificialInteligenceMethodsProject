using System;

using static Havannah.GameState.Player;


namespace Havannah.GameState
{
    class GameState
    {

        public GameStateElement[,] State { get; set; }


        public GameState(int boardSize)
        {
            State = new GameStateElement[2 * boardSize - 1, 2 * boardSize - 1];
        }

        public void ResetGameState()
        {
            Array.Clear(State, 0, State.Length);
        }

        public void PrintArray()
        {
            for (int i = 0; i < State.GetLength(0); i++)
            {
                for (int j = 0; j < State.GetLength(0); j++)
                {
                    Console.Write(i + " : " + j + " " + State[i, j] + "   ");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }


        public Player CheckIfOneOfThePlayersWonGame()
        {
            return None;
        }
    }
}
