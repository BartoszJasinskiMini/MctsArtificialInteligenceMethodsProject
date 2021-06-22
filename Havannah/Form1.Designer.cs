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
            this.restartGame = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // restartGame
            // 
            this.restartGame.Location = new System.Drawing.Point(17, 16);
            this.restartGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.restartGame.Name = "restartGame";
            this.restartGame.Size = new System.Drawing.Size(100, 42);
            this.restartGame.TabIndex = 0;
            this.restartGame.Text = "Restart Game";
            this.restartGame.UseVisualStyleBackColor = true;
            this.restartGame.Click += new System.EventHandler(this.restartGame_Click);
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(17, 65);
            this.infoBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(236, 50);
            this.infoBox.TabIndex = 1;
            this.infoBox.Text = "";
            this.infoBox.TextChanged += new System.EventHandler(this.infoBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1845, 1061);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.restartGame);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Havannah";
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button restartGame;
        private System.Windows.Forms.RichTextBox infoBox;
    }
}

