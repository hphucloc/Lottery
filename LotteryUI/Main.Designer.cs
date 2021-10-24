namespace LotteryUI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonGetNumber = new System.Windows.Forms.Button();
            this.dateTimePickerMinDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxMinDate = new System.Windows.Forms.CheckBox();
            this.checkBoxMaxDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerMaxDate = new System.Windows.Forms.DateTimePicker();
            this.button6Over45 = new System.Windows.Forms.Button();
            this.button4DMax = new System.Windows.Forms.Button();
            this.button6Over55 = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGetNumber
            // 
            this.buttonGetNumber.Location = new System.Drawing.Point(116, 68);
            this.buttonGetNumber.Name = "buttonGetNumber";
            this.buttonGetNumber.Size = new System.Drawing.Size(117, 23);
            this.buttonGetNumber.TabIndex = 1;
            this.buttonGetNumber.Text = "Lấy số mới nhất";
            this.buttonGetNumber.UseVisualStyleBackColor = true;
            this.buttonGetNumber.Click += new System.EventHandler(this.buttonGetNumber_Click);
            // 
            // dateTimePickerMinDate
            // 
            this.dateTimePickerMinDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMinDate.Location = new System.Drawing.Point(116, 12);
            this.dateTimePickerMinDate.Name = "dateTimePickerMinDate";
            this.dateTimePickerMinDate.Size = new System.Drawing.Size(78, 20);
            this.dateTimePickerMinDate.TabIndex = 2;
            // 
            // checkBoxMinDate
            // 
            this.checkBoxMinDate.AutoSize = true;
            this.checkBoxMinDate.Location = new System.Drawing.Point(200, 17);
            this.checkBoxMinDate.Name = "checkBoxMinDate";
            this.checkBoxMinDate.Size = new System.Drawing.Size(43, 17);
            this.checkBoxMinDate.TabIndex = 3;
            this.checkBoxMinDate.Text = "Min";
            this.checkBoxMinDate.UseVisualStyleBackColor = true;
            this.checkBoxMinDate.CheckedChanged += new System.EventHandler(this.checkBoxMinDate_CheckedChanged);
            // 
            // checkBoxMaxDate
            // 
            this.checkBoxMaxDate.AutoSize = true;
            this.checkBoxMaxDate.Checked = true;
            this.checkBoxMaxDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMaxDate.Location = new System.Drawing.Point(200, 45);
            this.checkBoxMaxDate.Name = "checkBoxMaxDate";
            this.checkBoxMaxDate.Size = new System.Drawing.Size(46, 17);
            this.checkBoxMaxDate.TabIndex = 5;
            this.checkBoxMaxDate.Text = "Max";
            this.checkBoxMaxDate.UseVisualStyleBackColor = true;
            this.checkBoxMaxDate.CheckedChanged += new System.EventHandler(this.checkBoxMaxDate_CheckedChanged);
            // 
            // dateTimePickerMaxDate
            // 
            this.dateTimePickerMaxDate.Enabled = false;
            this.dateTimePickerMaxDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMaxDate.Location = new System.Drawing.Point(116, 40);
            this.dateTimePickerMaxDate.Name = "dateTimePickerMaxDate";
            this.dateTimePickerMaxDate.Size = new System.Drawing.Size(78, 20);
            this.dateTimePickerMaxDate.TabIndex = 4;
            // 
            // button6Over45
            // 
            this.button6Over45.Location = new System.Drawing.Point(116, 97);
            this.button6Over45.Name = "button6Over45";
            this.button6Over45.Size = new System.Drawing.Size(117, 23);
            this.button6Over45.TabIndex = 6;
            this.button6Over45.Text = "6/45";
            this.button6Over45.UseVisualStyleBackColor = true;
            this.button6Over45.Click += new System.EventHandler(this.button6Over45_Click);
            // 
            // button4DMax
            // 
            this.button4DMax.Location = new System.Drawing.Point(116, 153);
            this.button4DMax.Name = "button4DMax";
            this.button4DMax.Size = new System.Drawing.Size(117, 23);
            this.button4DMax.TabIndex = 7;
            this.button4DMax.Text = "4DMax";
            this.button4DMax.UseVisualStyleBackColor = true;
            this.button4DMax.Click += new System.EventHandler(this.button4Dmax_Click);
            // 
            // button6Over55
            // 
            this.button6Over55.Location = new System.Drawing.Point(116, 124);
            this.button6Over55.Name = "button6Over55";
            this.button6Over55.Size = new System.Drawing.Size(117, 23);
            this.button6Over55.TabIndex = 8;
            this.button6Over55.Text = "6/55";
            this.button6Over55.UseVisualStyleBackColor = true;
            this.button6Over55.Click += new System.EventHandler(this.button6Over55_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(12, 194);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(98, 23);
            this.buttonAbout.TabIndex = 9;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LotteryUI.Properties.Resources.WP_20170528_12_12_39_Pro;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 178);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 222);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.button6Over55);
            this.Controls.Add(this.button4DMax);
            this.Controls.Add(this.button6Over45);
            this.Controls.Add(this.checkBoxMaxDate);
            this.Controls.Add(this.dateTimePickerMaxDate);
            this.Controls.Add(this.checkBoxMinDate);
            this.Controls.Add(this.dateTimePickerMinDate);
            this.Controls.Add(this.buttonGetNumber);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê số";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonGetNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerMinDate;
        private System.Windows.Forms.CheckBox checkBoxMinDate;
        private System.Windows.Forms.CheckBox checkBoxMaxDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerMaxDate;
        private System.Windows.Forms.Button button6Over45;
        private System.Windows.Forms.Button button4DMax;
        private System.Windows.Forms.Button button6Over55;
        private System.Windows.Forms.Button buttonAbout;
    }
}

