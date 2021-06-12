using System;
using System.Drawing;
using System.Windows.Forms;

using static System.Math;
using static System.Drawing.Color;
using static Havannah.HexagonButton;
using static Havannah.GameState.Player;


namespace Havannah
{
    public partial class Form1 : Form
    {

        private static int boardSize = 4, buttonSize = 56, buttonsLocationXCoordinate, buttonsLocationYCoordinate;
        private GameState.GameState gameState = new GameState.GameState(boardSize);
        private Color hexagonButtonsDefaultColor = SandyBrown, firstPlayerColor = ForestGreen, secondPlayerColor = Red;
        private bool didFirstPlayerClickedHexagonButton = true;

        public Form1()
        {
            InitializeComponent();

            buttonsLocationXCoordinate = ClientSize.Width / 2 - boardSize / 2 * buttonSize;
            buttonsLocationYCoordinate = ClientSize.Height / 2 + boardSize / 2 * buttonSize - 80;

            Shown += CreateHavannahBoardDelegate;
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH BY ALGORITHMS ON GUI
        public void ClickOnSpecificButton(int i, int j, GameState.Player player)
        {
            EvaluateClickCoordinates(i, j);

            didFirstPlayerClickedHexagonButton = player == First;
            var hexagonButton = (HexagonButton)Controls[GenerateHexagonButtonName(i, j)];
            hexagonButton.PerformClick();

            ClickOnSpecificField(i, j);
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH WITHOUT GUI
        public void ClickOnSpecificField(int i, int j)
        {
            EvaluateClickCoordinates(i, j);

            gameState.State[i, j].IsClicked = true;
            gameState.State[i, j].Player = didFirstPlayerClickedHexagonButton ? First : Second;

            gameState.CheckIfOneOfThePlayersWonGame();
        }

        private void CreateHavannahBoardDelegate(object sender, EventArgs e)
        {

            for (int i = 0, k = 2 * boardSize - 2; i < 2 * boardSize - 1; i++, k--)
            {
                int z = i;
                if (i > boardSize - 1)
                {
                    z = k;
                }
                for (int j = 0; j < boardSize + z; j++)
                {
                    var hexagonButton = new HexagonButton
                    {
                        ForeColor = White,
                        BackColor = hexagonButtonsDefaultColor,
                        FlatStyle = FlatStyle.Flat
                    };
                    hexagonButton.FlatAppearance.BorderSize = 0;


                    hexagonButton.Text = GenerateHexagonButtonName(i, j);
                    hexagonButton.Name = GenerateHexagonButtonName(i, j);
                    hexagonButton.Size = new Size(buttonSize, buttonSize);
                    hexagonButton.Click += new EventHandler(HexagonButtons_Click);

                    hexagonButton.Location = new Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * i * buttonSize / 2.25),
                        buttonsLocationYCoordinate - (int)(Sqrt(3) / 2 * buttonSize * j) + (int)(Sqrt(3) / 4 * buttonSize * z));

                    Controls.Add(hexagonButton);
                }
            }

        }

        private bool EvaluateClickCoordinates(int i, int j)
        {
            if (i < 0 || j < 0 || i >= 2 * boardSize - 1)
            {
                throw new Exception("CLICK: " + GenerateHexagonButtonName(i, j) + " OUTSIDE BOARD BOUNDARIES");
            }

            if (j >= i + boardSize)
            {
                throw new Exception("CLICK: " + GenerateHexagonButtonName(i, j) + " OUTSIDE BOARD BOUNDARIES");
            }

            return true;
        }


        private void restartGame_Click(object sender, EventArgs e)
        {
            gameState.ResetGameState();
            ResetGuiState();
        }

        private void ResetGuiState()
        {
            foreach(var control in Controls)
            {
                if (control is HexagonButton)
                {
                    (control as HexagonButton).BackColor = hexagonButtonsDefaultColor;
                }
            }

            infoBox.ResetText();

        }

        private void infoBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void HexagonButtons_Click(object sender, EventArgs e)
        {
            var hexagonButton = (HexagonButton)sender;
            if(didFirstPlayerClickedHexagonButton)
            hexagonButton.BackColor = didFirstPlayerClickedHexagonButton ? firstPlayerColor : secondPlayerColor;

            infoBox.Text = hexagonButton.Name;

            didFirstPlayerClickedHexagonButton = true;
        }






    }
}
