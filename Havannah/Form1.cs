using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using static System.Math;
using static System.Drawing.Color;


namespace Havannah
{
    public partial class Form1 : Form
    {
        private static int boardSize = 8, buttonSize = 56, buttonsLocationXCoordinate, buttonsLocationYCoordinate;
        private bool[,] gameState = new bool[2 * boardSize - 1, 2 * boardSize - 1];

        public Form1()
        {
            InitializeComponent();

            buttonsLocationXCoordinate = ClientSize.Width / 2 - boardSize / 2 * buttonSize;
            buttonsLocationYCoordinate = ClientSize.Height / 2 + boardSize / 2 * buttonSize - 80;



            //CreateStateHoldingStructure();
            Shown += CreateHavannahBoardDelegate;
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH ON GUI
        public void ClickOnSpecificButton(int i, int j)
        {
            EvaluateClickCoordinates(i, j);
            var hexagonButton = Controls[GenerateHexagonButtonName(i, j)];
            ((HexagonButton)hexagonButton).PerformClick();
            ClickOnSpecificField(i, j);
        }

        // THIS FUNCTION IS FOR PLAYING HAVANNAH WITHOUT GUI
        public void ClickOnSpecificField(int i, int j)
        {
            EvaluateClickCoordinates(i, j);

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
                        BackColor = Black,
                        FlatStyle = FlatStyle.Flat
                    };
                    hexagonButton.FlatAppearance.BorderColor = Black;
                    hexagonButton.FlatAppearance.BorderSize = 5;
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
        }

        private void infoBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void HexagonButtons_Click(object sender, EventArgs e)
        {
            var hexagonButton = (HexagonButton)sender;
            infoBox.Text = hexagonButton.Name;
        }

        private string GenerateHexagonButtonName(int i, int j) => i + ", " + j;


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

        private void PrintArray()
        {
            for (int i = 0; i < gameState.GetLength(0); i++)
            {
                for (int j = 0; j < gameState.GetLength(0); j++)
                {
                    Console.Write(i + " : " + j + " " + gameState[i, j] + "   ");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }


    }
}
