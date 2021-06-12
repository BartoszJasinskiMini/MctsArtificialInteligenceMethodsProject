namespace Havannah.GameState
{
    class GameStateElement
    {
        public bool IsClicked { get; set; }
        public Player Player;

    }

    public enum Player
    {
        First,
        Second,
        None
    }
}
