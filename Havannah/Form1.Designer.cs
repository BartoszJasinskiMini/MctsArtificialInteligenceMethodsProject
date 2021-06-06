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
            this.hexagonButton1 = new Havannah.HexagonButton();
            this.SuspendLayout();
            // 
            // hexagonButton1
            // 
            this.hexagonButton1.Location = new System.Drawing.Point(294, 396);
            this.hexagonButton1.Name = "hexagonButton1";
            this.hexagonButton1.Size = new System.Drawing.Size(494, 202);
            this.hexagonButton1.TabIndex = 0;
            this.hexagonButton1.Text = "hexagonButton1";
            this.hexagonButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 838);
            this.Controls.Add(this.hexagonButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private HexagonButton hexagonButton1;
    }
}

