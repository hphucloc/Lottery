namespace LotteryUI
{
    partial class NoBet
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePickerDateBought = new System.Windows.Forms.DateTimePicker();
            this.listBoxNoBet = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerDateBought, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxNoBet, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(316, 745);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dateTimePickerDateBought
            // 
            this.dateTimePickerDateBought.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerDateBought.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateBought.Location = new System.Drawing.Point(3, 3);
            this.dateTimePickerDateBought.Name = "dateTimePickerDateBought";
            this.dateTimePickerDateBought.Size = new System.Drawing.Size(310, 20);
            this.dateTimePickerDateBought.TabIndex = 5;
            // 
            // listBoxNoBet
            // 
            this.listBoxNoBet.BackColor = System.Drawing.Color.Silver;
            this.listBoxNoBet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxNoBet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNoBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.listBoxNoBet.ForeColor = System.Drawing.Color.Maroon;
            this.listBoxNoBet.FormattingEnabled = true;
            this.listBoxNoBet.HorizontalScrollbar = true;
            this.listBoxNoBet.ItemHeight = 24;
            this.listBoxNoBet.Location = new System.Drawing.Point(3, 33);
            this.listBoxNoBet.Name = "listBoxNoBet";
            this.listBoxNoBet.Size = new System.Drawing.Size(310, 709);
            this.listBoxNoBet.TabIndex = 4;
            this.listBoxNoBet.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxNoBet_MouseDoubleClick);
            // 
            // NoBet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 745);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoBet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Number Bet";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxNoBet;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateBought;
    }
}