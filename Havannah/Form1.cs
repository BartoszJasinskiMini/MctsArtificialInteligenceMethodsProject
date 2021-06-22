using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using static System.Math;
using static System.Drawing.Color;
using static Havannah.HexagonButton;
using Havannah.Logic;
using System.Threading;

namespace Havannah
{
    public partial class Form1 : Form
    {

        private static int boardSize = 3, buttonSize = 56, buttonsLocationXCoordinate, buttonsLocationYCoordinate;
        private GameState.GameState gameState = new GameState.GameState(boardSize);
        private Color hexagonButtonsDefaultColor = SandyBrown, firstPlayerColor = ForestGreen, secondPlayerColor = Red;
        private int whichPlayerClicked = 0;
        private int moveTime = 1000;

        MonteCarloTreeSearch monteCarloTreeSearch = new MonteCarloTreeSearch();

        private System.Timers.Timer gameTimer;
        private bool ifPlayerMadeAMove;
        private const int _playerId = 1;
        private const int _oponentId = 2;


        public Form1()
        {
            InitializeComponent();

            buttonsLocationXCoordinate = ClientSize.Width / 2 - boardSize / 2 * buttonSize;
            buttonsLocationYCoordinate = ClientSize.Height / 2 + boardSize / 2 * buttonSize - 80;

            Shown += CreateHavannahBoardDelegate;
            //Thread gameThread = new Thread(GameFunction);
            gameTimer = new System.Timers.Timer();
            //gameThread.Start();

            gameTimer.Interval = moveTime;
            gameTimer.Elapsed += PlayerTurn;
            gameTimer.Start();
        }

        public void GameFunction()
        {
            
        }

        private void PlayerTurn(object sender, System.Timers.ElapsedEventArgs e)
        {
            gameTimer.Stop();
            gameTimer.Elapsed -= PlayerTurn;
            gameTimer.Elapsed += OponentTurn;
            gameTimer.Start();
            ifPlayerMadeAMove = false;
        }

        private void OponentTurn(object sender, System.Timers.ElapsedEventArgs e)
        {
            gameTimer.Stop();
            gameTimer.Elapsed -= OponentTurn;
            gameTimer.Elapsed += PlayerTurn;
            gameTimer.Start();
            if (!ifPlayerMadeAMove)
            {
                Random rand = new Random();
                ClickOnSpecificButton(_playerId, gameState.Board.FreeCells[rand.Next(0, gameState.Board.FreeCells.Count)]);
            }
            foreach (var control in Controls)
            {
                if(control is HexagonButton)
                {
                    (control as HexagonButton).Enabled = false; 
                }
            }
            Logic.Point nextMove = monteCarloTreeSearch.RunAlgorithm(gameState.Board.Clone(), 0.9 * moveTime);
            foreach (var control in Controls)
            {
                if (control is HexagonButton)
                {
                    (control as HexagonButton).Enabled = true;
                }
            }
            ClickOnSpecificButton(_oponentId, nextMove);
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH BY ALGORITHMS ON GUI
        public void ClickOnSpecificButton(int player, Havannah.Logic.Point point)
        {
            ClickOnSpecificButton(player, point.X, point.Y);
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH BY ALGORITHMS ON GUI
        public void ClickOnSpecificButton(int player, int x, int y)
        {
            whichPlayerClicked = player;
            var hexagonButton = (HexagonButton)Controls[GenerateHexagonButtonName(x, y)];

            hexagonButton.BackColor = whichPlayerClicked == 0 || whichPlayerClicked == 1 ? firstPlayerColor : secondPlayerColor;
            infoBox.Text = hexagonButton.Name;

            var buttonCoordinates = hexagonButton.GetButtonCoordinates(boardSize);
            gameState.MakeMove((whichPlayerClicked == 0 || whichPlayerClicked == 1) ? 1 : 2, buttonCoordinates.Item1, buttonCoordinates.Item2);
            gameState.PrintBoard();

            foreach (var control in Controls)
            {
                if (control is HexagonButton)
                {
                    (control as HexagonButton).Enabled = false;
                }
            }
            ifPlayerMadeAMove = true;

            if (gameState.CheckIfWon() != 0)
            {
                infoBox.Text = "SOMEONE WON GAME!!!";
            }

            whichPlayerClicked = 0;

            if (player == _playerId)
            {
                foreach (var control in Controls)
                {
                    if (control is HexagonButton)
                    {
                        (control as HexagonButton).Enabled = false;
                    }
                }
            }
            
        }


        // THIS FUNCTION IS FOR PLAYING HAVANNAH WITHOUT GUI
        //     public void ClickOnSpecificField(int player, int x, int y)
        //       {
        //        gameState.MakeMove(player, x, y);

        //  gameState.State[i, j].IsClicked = true;
        //  gameState.State[i, j].Player = didFirstPlayerClickedHexagonButton ? First : Second;

        //  gameState.CheckIfOneOfThePlayersWonGame();
        //       }



        private void CreateHavannahBoardDelegate(object sender, EventArgs e)
        {
            CreateBoard();
        }


        private void CreateBoard()
        {
            for (int i = 0, k = 2 * boardSize - 2; i < 2 * boardSize - 1; i++, k--)
            {
                int z = i;
                if (i > boardSize - 1)
                {
                    z = k;
                }
                int l = i;
                int m = 4;
                double n = 0;
                if (i > boardSize - 1)
                {
                    l -= (i - boardSize + 1);
                    m = 2;
                    n = Sqrt(3) / 4 * buttonSize;
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

                    hexagonButton.Location = new System.Drawing.Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * (boardSize - l + j) * buttonSize / 2.25),
                    buttonsLocationYCoordinate - (int)(Sqrt(3) / 4 * buttonSize * j + n) + (int)(Sqrt(3) / m * buttonSize * (boardSize - i)));

                    Controls.Add(hexagonButton);
                }
            }
        }


        //ITS NOT USED BUT DONT DELETE IT
        private void func()
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

                    hexagonButton.Location = new System.Drawing.Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * i * buttonSize / 2.25),
                        buttonsLocationYCoordinate - (int)(Sqrt(3) / 2 * buttonSize * j) + (int)(Sqrt(3) / 4 * buttonSize * z));

                    Controls.Add(hexagonButton);
                }
            }
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

        //private void HexagonButtons_Click(object sender, EventArgs e)
        //{
        //    var hexagonButton = (HexagonButton)sender;
        //    hexagonButton.Visible = false;
        //    //if(didFirstPlayerClickedHexagonButton)
        //    //hexagonButton.BackColor = didFirstPlayerClickedHexagonButton ? firstPlayerColor : secondPlayerColor;
        //    hexagonButton.BackColor = whichPlayerClicked == 0 || whichPlayerClicked == 1 ? firstPlayerColor : secondPlayerColor;
        //    infoBox.Text = hexagonButton.Name;
        //    var buttonCoordinates = hexagonButton.GetHexCoordinates();
        //    gameState.MakeMove(whichPlayerClicked == 0 || whichPlayerClicked == 1 ? 1 : 2, buttonCoordinates.Item1, buttonCoordinates.Item2);
        //    gameState.PrintBoard();
        //    // didFirstPlayerClickedHexagonButton = true;
        //}

        private void HexagonButtons_Click(object sender, EventArgs e)
        {
            var hexagonButton = (HexagonButton)sender;
            hexagonButton.BackColor = whichPlayerClicked == 0 || whichPlayerClicked == 1 ? firstPlayerColor : secondPlayerColor;
            infoBox.Text = hexagonButton.Name;

            var buttonCoordinates = hexagonButton.GetButtonCoordinates(boardSize);
            gameState.MakeMove((whichPlayerClicked == 0 || whichPlayerClicked == 1) ? 1 : 2, buttonCoordinates.Item1, buttonCoordinates.Item2);
            gameState.PrintBoard();

            foreach (var control in Controls)
            {
                if (control is HexagonButton)
                {
                    (control as HexagonButton).Enabled = false;
                }
            }
            ifPlayerMadeAMove = true;

            if (gameState.CheckIfWon() != 0)
            {
                infoBox.Text = "SOMEONE WON GAME!!!";
            }

            whichPlayerClicked = 0;
        }






    }
}
