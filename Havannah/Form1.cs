using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using static System.Math;
using static System.Drawing.Color;
using static Havannah.HexagonButton;
using Havannah.Logic;

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

        private Timer timer1;



        public Form1()
        {
            InitializeComponent();

            buttonsLocationXCoordinate = ClientSize.Width / 2 - boardSize / 2 * buttonSize;
            buttonsLocationYCoordinate = ClientSize.Height / 2 + boardSize / 2 * buttonSize - 80;

            Shown += CreateHavannahBoardDelegate;
          //  InitTimer();
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH BY ALGORITHMS ON GUI
        public void ClickOnSpecificButton(int player, Board.Point point)
        {
            ClickOnSpecificButton(player, point.X, point.Y);
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH BY ALGORITHMS ON GUI
        public void ClickOnSpecificButton(int player, int x, int y)
        {
            whichPlayerClicked = player;
            var hexagonButton = (HexagonButton)Controls[GenerateHexagonButtonName(x, y)];
            hexagonButton.PerformClick();
            
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = moveTime + 100; // in miliseconds
            timer1.Start();
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH WITHOUT GUI
        //     public void ClickOnSpecificField(int player, int x, int y)
        //       {
        //        gameState.MakeMove(player, x, y);

        //  gameState.State[i, j].IsClicked = true;
        //  gameState.State[i, j].Player = didFirstPlayerClickedHexagonButton ? First : Second;

        //  gameState.CheckIfOneOfThePlayersWonGame();
        //       }


        private void timer1_Tick(object sender, EventArgs e)
        {
            InvokeAlgorithm();
        }

        private void InvokeAlgorithm()
        {


            Task<Board.Point> task = Task<Board.Point>.Factory.StartNew(() =>
            {
                //infoBox.Text = "WAIT FOR YOUR TURN";

                var point = monteCarloTreeSearch.RunAlgorithm(gameState.Board, moveTime);
                ClickOnSpecificButton(1, point);
                //if (whichPlayerClicked == 1 || whichPlayerClicked == 0)
                //{
                //    whichPlayerClicked = 2;
                //}
                //else if (whichPlayerClicked == 2)
                //{
                //    whichPlayerClicked = 1;
                //}

             //   infoBox.Text = "MAKE MOVE";

                return point;
            });



        }

        private void CreateHavannahBoardDelegate(object sender, EventArgs e)
        {
            func2();
        }


        private void func2()
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

                    hexagonButton.Location = new Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * (boardSize - l + j) * buttonSize / 2.25),
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

                    hexagonButton.Location = new Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * i * buttonSize / 2.25),
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
            //hexagonButton.Visible = false;
            hexagonButton.BackColor = whichPlayerClicked == 0 || whichPlayerClicked == 1 ? firstPlayerColor : secondPlayerColor;
            infoBox.Text = hexagonButton.Name;

            var buttonCoordinates = hexagonButton.GetButtonCoordinates(boardSize);
            gameState.MakeMove((whichPlayerClicked == 0 || whichPlayerClicked == 1) ? 1 : 2, buttonCoordinates.Item1, buttonCoordinates.Item2);
            gameState.PrintBoard();

            if(gameState.CheckIfWon() != 0)
            {
                infoBox.Text = "SOMEONE WON GAME!!!";
            }

            whichPlayerClicked = 0;
        }






    }
}
