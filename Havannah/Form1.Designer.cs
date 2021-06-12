using System;

using static System.Math;
using static System.Drawing.Color;
using System.Windows.Forms;

namespace Havannah
{
    partial class Form1
    {
        public void ClickOnSpecificButton(int i, int j)
        {
            var hexagonButton = this.Controls[GenerateHexagonButtonName(i, j)];
            ((HexagonButton)hexagonButton).PerformClick();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 962);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private void CreateHavannahBoardDelegate(object sender, EventArgs e)
        {
            int boardSize = 8, buttonSize = 56, buttonsLocationXCoordinate = ClientSize.Width / 2 - boardSize / 2 * buttonSize, 
                buttonsLocationYCoordinate = ClientSize.Height / 2 + boardSize / 2 * buttonSize;
            

            for (int i = 0, k = 2 * boardSize - 2; i < 2 * boardSize - 1; i++, k--)
            {
                int z = i;
                if (i > boardSize - 1)
                {
                    z = k;
                }
                for (int j = 0; j < boardSize + z; j++)
                {
                    var hexagonButton = new HexagonButton();
                    hexagonButton.ForeColor = White;
                    hexagonButton.BackColor = Black;
                    hexagonButton.FlatStyle = FlatStyle.Flat;
                    hexagonButton.FlatAppearance.BorderColor = Black;
                    hexagonButton.FlatAppearance.BorderSize = 5;
                    hexagonButton.Text = GenerateHexagonButtonName(i, j);
                    hexagonButton.Name = GenerateHexagonButtonName(i, j);
                    hexagonButton.Size = new System.Drawing.Size(buttonSize, buttonSize);
                    hexagonButton.Click += new EventHandler(hexagonButtons_Click);

                    hexagonButton.Location = new System.Drawing.Point(buttonsLocationXCoordinate + (int)(Sqrt(3) * i * buttonSize / 2),
                        buttonsLocationYCoordinate - (int)(Sqrt(3) / 2 * buttonSize * j) + (int)(Sqrt(3) / 4 * buttonSize * z));

                    this.Controls.Add(hexagonButton);
                }
            }

            ClickOnSpecificButton(1, 3);

            ClickOnSpecificButton(3, 3);
        }



        private void hexagonButtons_Click(object sender, EventArgs e)
        {
            var hexagonButton = (HexagonButton)sender;
            Console.WriteLine(hexagonButton.Name);
        }


        private string GenerateHexagonButtonName(int i, int j) => i + ", " + j;

    }
}

