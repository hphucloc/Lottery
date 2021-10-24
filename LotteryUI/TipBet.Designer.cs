namespace LotteryUI
{
    partial class TipBet
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
            this.listBoxTipBet = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxTipBet
            // 
            this.listBoxTipBet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxTipBet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTipBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.listBoxTipBet.ForeColor = System.Drawing.Color.OrangeRed;
            this.listBoxTipBet.FormattingEnabled = true;
            this.listBoxTipBet.HorizontalScrollbar = true;
            this.listBoxTipBet.ItemHeight = 20;
            this.listBoxTipBet.Location = new System.Drawing.Point(0, 0);
            this.listBoxTipBet.Name = "listBoxTipBet";
            this.listBoxTipBet.Size = new System.Drawing.Size(943, 501);
            this.listBoxTipBet.TabIndex = 3;
            // 
            // TipBet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 501);
            this.Controls.Add(this.listBoxTipBet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TipBet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TipBet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTipBet;
    }
}