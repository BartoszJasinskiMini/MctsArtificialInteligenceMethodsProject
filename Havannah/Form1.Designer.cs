using System;

using static System.Drawing.Color;

namespace Havannah
{
    partial class Form1
    {
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
            this.ClientSize = new System.Drawing.Size(1363, 838);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private void CreateHavannahBoardDelegate(object sender, EventArgs e)
        {
            var boardSize = 8;
            var buttonSize = 32;
            CreateFirstHalfOfHavannahBoard(boardSize, buttonSize);


        }

        private void CreateFirstHalfOfHavannahBoard(int boardSize, int buttonSize)
        {
            for (int i = 0; i < boardSize * 2 - 1; i++)
            {
                for (int j = 0; j < boardSize + i; j++)
                {
                    var hexagonButton = new HexagonButton();
                    hexagonButton.BackColor = Red;
                    hexagonButton.Text = "Button" + i;
                    hexagonButton.Location = new System.Drawing.Point(400 + (int)(Math.Sqrt(3) * i * buttonSize / 2), 400 - (int)(Math.Sqrt(3)/2 * buttonSize * (j)) + (int)(Math.Sqrt(3) / 4 * buttonSize * i)    );
                    hexagonButton.Size = new System.Drawing.Size(buttonSize, buttonSize);
                    hexagonButton.AccessibleName = "HERE IS BUTTON" + i + " " + j;
                    hexagonButton.Click += new EventHandler(hexagonButtons_Click);

                    this.Controls.Add(hexagonButton);
                }


            }
        }


        private void hexagonButtons_Click(object sender, EventArgs e)
        {
            var hexagonButton = (HexagonButton)sender;
            Console.WriteLine(hexagonButton.AccessibleName);
        }


        private double Ceiling(int number)
        {
            return Math.Ceiling((double)number);
        }
    }
}

