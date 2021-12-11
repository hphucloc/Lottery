using LotteryBusiness;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Xml.Linq;

namespace LotteryUI
{
    public partial class _6Over55 : Form
    {
        private List<LoterryStatistic> _6Over55No { get; set; }
        private List<LoterryStatistic> _Full6Over55No { get; set; }
        private DateTime datePublishFrom;
        private DateTime datePublishTo;
        public _6Over55(DateTime datePublishFrom, DateTime datePublishTo)
        {
            this.datePublishFrom = datePublishFrom;
            this.datePublishTo = datePublishTo;
            InitializeComponent();

            _Full6Over55No = LotteryBusiness._6Over55TimeLine.Get6Over55Number(DateTime.MinValue, DateTime.MaxValue);
            _6Over55No = LotteryBusiness._6Over55TimeLine.Get6Over55Number(datePublishFrom, datePublishTo);

            this.labelMinDate.Text = _6Over55No[0].DatePublishMin.ToShortDateString();
            this.labelMaxDate.Text = _6Over55No[0].DatePublishMax.ToShortDateString();
            this.labelMinDate2.Text = _6Over55No[0].DatePublishMin.ToShortDateString();
            this.labelMaxDate2.Text = _6Over55No[0].DatePublishMax.ToShortDateString();

            this.toolStripLabelWined.Text = String.Format("Wined: {0:0,0}", LotteryBusiness.Common.Wined());
            this.toolStripLabelBeted.Text = String.Format("Bet: {0:0,0}", LotteryBusiness.Common.Bet());

            DrawTimeLine();
            DrawTimeLine2();


        }                    

        private void DrawTimeLine()
        {
            int h = 20;

            var HeaderNo = new Label()
            {
                Text = "Số",
            };
            HeaderNo.Size = new Size(30, 20);
            HeaderNo.Location = new Point(10, h - 20);            
            HeaderNo.Font = new Font("Arial Black", 8.25F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));
            this.panel1.Controls.Add(HeaderNo);

            var HeaderAppear = new Label()
            {
                Text = "Số lần xuất hiện",
            };
            HeaderAppear.Size = new Size(150, 20);
            HeaderAppear.Location = new Point(40, h - 20);
            HeaderAppear.Font = new Font("Arial Black", 8.25F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));
            this.panel1.Controls.Add(HeaderAppear);

            byte countNo = 0;            
            int horNo = 0;
            foreach (var aNo in _6Over55No)
            {
                int w = 70;
                //Number
                var no = new CustomLabel()
                {
                    Text = aNo.LotNumber.ToString(),
                    NumberAppear = aNo.TotalNumberAppear.ToString()
                };
                no.Size = new Size(30, 20);
                no.Location = new Point(10, h);
                no.ForeColor = Color.Black;                
                no.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
                this.panel1.Controls.Add(no);

                //Total appear
                var sta1 = new Label()
                {
                    Text = aNo.TotalNumberAppear.ToString()
                };
                sta1.Size = new Size(30, 20);
                sta1.Location = new Point(40, h);
                this.panel1.Controls.Add(sta1);

                foreach (var a in aNo.AllDatePublishList.AsEnumerable().Reverse())
                {
                    CustomButton b = new CustomButton();
                    b.Size = new Size(17, 17);
                    b.Location = new Point(w, h);
                    b.Number = aNo.LotNumber.ToString();
                    b.DatePublish = a.ToShortDateString();                 
                    b.Click += NumberButtonClick;
                    b.MouseEnter += NumberButtonMouseEnter;
                    b.MouseLeave += NumberButtonMouseLeave;
                    b.Text = (++countNo).ToString();
                    b.HorizontalNumber = ++horNo;
                    foreach (var c in aNo.DatePublishList.DatePublishList1)
                    {
                        if (a.Equals(c))
                        {
                            b.BackColor = Color.DarkRed;
                            countNo = 0;
                        }
                    }                    
                    panel1.Controls.Add(b);
                    w += 15;
                }

                h += 20;
                countNo = 0;
                horNo = 0;
            }

            this.labelMessage.Text = "Thông báo";
        }

        private void DrawTimeLine2(bool isFull = false)
        {
            int x = 10;
            int y = 200;
            int btnSize = 150;

            //draw legend
            CustomButton lengendButton = new CustomButton();
            lengendButton.Size = new Size(btnSize, 25);
            lengendButton.Location = new Point(10, 200);
            lengendButton.BackColor = Color.DarkRed;
            lengendButton.Text = "1->7";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.White;
            this.panel3.Controls.Add(lengendButton);
            TextBox txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear1_7 = new Label();
            lblTotalAppear1_7.Location = new Point(10, 260);
            lblTotalAppear1_7.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear1_7);

            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize, 200);
            lengendButton.BackColor = Color.Black;
            lengendButton.Text = "8->15";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.White;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear8_15 = new Label();
            lblTotalAppear8_15.Location = new Point(10 + btnSize, 260);
            lblTotalAppear8_15.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear8_15);


            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize * 2, 200);
            lengendButton.BackColor = Color.Pink;
            lengendButton.Text = "16->23";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.Black;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize*2, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear16_23 = new Label();
            lblTotalAppear16_23.Location = new Point(10 + btnSize * 2, 260);
            lblTotalAppear16_23.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear16_23);


            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize * 3, 200);
            lengendButton.BackColor = Color.Blue;
            lengendButton.Text = "24->31";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.White;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize*3, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear24_31 = new Label();
            lblTotalAppear24_31.Location = new Point(10 + btnSize * 3, 260);
            lblTotalAppear24_31.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear24_31);

            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize * 4, 200);
            lengendButton.BackColor = Color.WhiteSmoke;
            lengendButton.Text = "32->39";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.Black;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize*4, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear32_39 = new Label();
            lblTotalAppear32_39.Location = new Point(10 + btnSize * 4, 260);
            lblTotalAppear32_39.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear32_39);

            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize * 5, 200);
            lengendButton.BackColor = Color.DarkMagenta;
            lengendButton.Text = "40->47";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.White;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize*5, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppear40_47 = new Label();
            lblTotalAppear40_47.Location = new Point(10 + btnSize * 5, 260);
            lblTotalAppear40_47.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppear40_47);

            lengendButton = new CustomButton();
            lengendButton.Size = new Size(150, 25);
            lengendButton.Location = new Point(10 + btnSize * 6, 200);
            lengendButton.BackColor = Color.DarkGoldenrod;
            lengendButton.Text = "48->55";
            lengendButton.Font = new Font("Arial", (float)12, FontStyle.Bold);
            lengendButton.ForeColor = Color.White;
            this.panel3.Controls.Add(lengendButton);
            txtNumberShouldBuy = new TextBox();
            txtNumberShouldBuy.Location = new Point(10 + btnSize*6, 230);
            txtNumberShouldBuy.Size = new Size(btnSize, 25);
            this.panel3.Controls.Add(txtNumberShouldBuy);
            Label lblTotalAppearupper47 = new Label();
            lblTotalAppearupper47.Location = new Point(10 + btnSize * 6, 260);
            lblTotalAppearupper47.Font = new Font("Arial", (float)12, FontStyle.Bold);
            this.panel3.Controls.Add(lblTotalAppearupper47);
            Label lblTotalAppearByColor = new Label();
            lblTotalAppearByColor.Location = new Point(10 + btnSize * 7, 260);
            lblTotalAppearByColor.Font = new Font("Arial", (float)12, FontStyle.Regular);
            lblTotalAppearByColor.Text = "Tổng số lần xuất hiện group by color.";
            lblTotalAppearByColor.Size = new Size(300, 25);
            this.panel3.Controls.Add(lblTotalAppearByColor);

            var xml = XDocument.Load(@"lotteryConfig.xml");
            var dataTimelineColor = xml.Descendants("mindateColor");

            var data = LotteryBusiness._6Over55TimeLine.Get6Over55Number(dataTimelineColor.FirstOrDefault() == null ?
                datePublishFrom : DateTime.Now.AddDays(Convert.ToInt32(dataTimelineColor.FirstOrDefault().Value.Trim()) * -1),
                datePublishTo);

            y = 10;
            x = 10;
            int count1_7 = 0;
            int count8_15 = 0;
            int count16_23 = 0;
            int count24_31 = 0;
            int count32_39 = 0;
            int count40_47 = 0;
            int countUpper47 = 0;

            SortedDictionary<DateTime, SortedSet<int?>> hitNumberByDate = new SortedDictionary<DateTime, SortedSet<int?>>();
            foreach (var aNo in data)
            {
                //Number
                var no = new CustomLabel()
                {
                    Text = aNo.LotNumber.ToString(),
                    NumberAppear = aNo.TotalNumberAppear.ToString()
                };
                no.Size = new Size(25, 15);
                no.Location = new Point(x-5, y);
                no.ForeColor = Color.Black;
                no.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
                this.panel3.Controls.Add(no);

                //Total appear
                var sta1 = new Label()
                {
                    Text = aNo.TotalNumberAppear.ToString()
                };
                sta1.Size = new Size(25, 15);
                sta1.Location = new Point(x-5, y + 20);
                this.panel3.Controls.Add(sta1);

                x += 24;
                if (aNo.LotNumber >= 1 && aNo.LotNumber <= 7)
                {
                    count1_7 += aNo.TotalNumberAppear;
                }
                else
                    if (aNo.LotNumber >= 8 && aNo.LotNumber <= 15)
                {
                    count8_15 += aNo.TotalNumberAppear;
                }
                else
                    if (aNo.LotNumber >= 6 && aNo.LotNumber <= 23)
                {
                    count16_23 += aNo.TotalNumberAppear;
                }
                else
                    if (aNo.LotNumber >= 24 && aNo.LotNumber <= 31)
                {
                    count24_31 += aNo.TotalNumberAppear;
                }
                else
                    if (aNo.LotNumber >= 32 && aNo.LotNumber <= 39)
                {
                    count32_39 += aNo.TotalNumberAppear;
                }
                else
                    if (aNo.LotNumber >= 40 && aNo.LotNumber <= 47)
                {
                    count40_47 += aNo.TotalNumberAppear;
                }
                else
                {
                    countUpper47 += aNo.TotalNumberAppear;
                }
                //Get all publis Date               
                foreach (var date in aNo.AllDatePublishList)
                {
                    if (!hitNumberByDate.ContainsKey(date))
                    {
                        hitNumberByDate.Add(date, new SortedSet<int?>());
                    }
                }
            }

            lblTotalAppear1_7.Text = count1_7.ToString();
            lblTotalAppear8_15.Text = count8_15.ToString();
            lblTotalAppear16_23.Text = count16_23.ToString();
            lblTotalAppear24_31.Text = count24_31.ToString();
            lblTotalAppear32_39.Text = count32_39.ToString();
            lblTotalAppear40_47.Text = count40_47.ToString();
            lblTotalAppearupper47.Text = countUpper47.ToString();

            foreach (var item in hitNumberByDate)
                foreach (var aNo in data)
                    foreach (var date in aNo.DatePublishList.DatePublishList1)
                        if (item.Key == date)
                            item.Value.Add(aNo.LotNumber);

            int newline = 0;           
            var dataxml = xml.Descendants("newline");
            foreach (XElement b in dataxml)
            {
                newline = Convert.ToInt32(b.Value.Trim());
            }

            y = 50;
            x = 10;
            int tempNewLine = 0;
            int tempy = 50;
            //Draw timeline by color
            foreach (var i in hitNumberByDate.AsEnumerable().Reverse())
            {
                //Add 6 so
                foreach (var j in i.Value)
                {
                    CustomButton b = new CustomButton();
                    b.Size = new Size(20, 17);
                    b.Location = new Point(x, y);
                    b.Text = j.ToString();
                    b.Font = new Font("Arial", (float)5, FontStyle.Bold);
                    b.ForeColor = Color.White;
                    if (j >= 1 && j <= 7)
                    {
                        b.BackColor = Color.DarkRed;
                        b.ForeColor = Color.White;
                    }
                    else if (j >= 8 && j <= 15)
                    {
                        b.BackColor = Color.Black;
                        b.ForeColor = Color.White;
                    }
                    else if (j >= 16 && j <= 23)
                    {
                        b.BackColor = Color.Pink;
                        b.ForeColor = Color.Black;
                    }
                    else if (j >= 24 && j <= 31)
                    {
                        b.BackColor = Color.Blue;
                        b.ForeColor = Color.White;
                    }
                    else if (j >= 32 && j <= 39)
                    {
                        b.BackColor = Color.WhiteSmoke;
                        b.ForeColor = Color.Black;
                    }
                    else if (j >= 40 && j <= 47)
                        b.BackColor = Color.DarkMagenta;
                    else if (j > 47)
                        b.BackColor = Color.DarkGoldenrod;
                    panel3.Controls.Add(b);

                    y += 20;
                }

                x += 16;
                y = tempy;
                tempNewLine++;
                if (tempNewLine == newline)
                {
                    // x = 10;
                    //  tempy = 200;
                    tempNewLine = 0;
                }
            }
            label6.Text = "Thông báo";
        }

        private void NumberButtonMouseLeave(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is CustomLabel)
                    if (((CustomLabel)c).Text.Equals(((CustomButton)sender).Number))
                    {
                        c.ForeColor = Color.Black;
                        c.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
                        this.labelMessage.Text = null;
                        //this.lblNoAppear.Text = null;
                        //this.lblNextNoAppear.Text = null;
                    }

                if (c is CustomButton)
                    if (((CustomButton)c).Number.Equals(((CustomButton)sender).Number))
                    {
                        if (c.BackColor == Color.Blue)
                        {
                            c.BackColor = Color.DarkRed;
                        }
                        if (c.BackColor != Color.DarkRed)
                            c.BackColor = Color.SeaShell;                        
                    }
            }

            foreach (Control c in panel1.Controls)
            {
                if (c is CustomButton)
                    if (c.BackColor == Color.LightGreen || c.BackColor == Color.LightPink)
                        c.BackColor = Color.SeaShell;
                    else if (c.BackColor == Color.Blue)
                    {
                        c.BackColor = Color.DarkRed;
                    }                  
            }
        }
        private void NumberButtonMouseEnter(object sender, EventArgs e)
        {
            List<int> horNoList = new List<int>();
            foreach (Control c in panel1.Controls)
            {
                if (c is CustomLabel)
                    if (((CustomLabel)c).Text.Equals(((CustomButton)sender).Number))
                    {
                        c.ForeColor = Color.Red;
                        c.Font = new Font("Arial", 10.0F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));
                        this.labelMessage.Text = "Số: " + ((CustomLabel)c).Text + " (" + ((CustomLabel)c).NumberAppear + ")" + ", ngày: " + ((CustomButton)sender).DatePublish;
                    }

                if (c is CustomButton)
                    if (((CustomButton)c).Number.Equals(((CustomButton)sender).Number))
                    {
                        if (c.BackColor != Color.DarkRed)
                            c.BackColor = Color.Yellow;
                        else
                        {
                            horNoList.Add(((CustomButton)c).HorizontalNumber);
                        }
                    }
            }

            //Count Number Appear Same time
            int[] tempNoAppear = new int[56];//Dai dien 55 so
            SortedSet<DateTime> dateAppear = new SortedSet<DateTime>();
            CultureInfo cul = new CultureInfo("vi-VN");
            foreach (Control c in panel1.Controls)
            {
                if (c is CustomButton)
                    if (horNoList.Contains(((CustomButton)c).HorizontalNumber))
                    {
                        if (c.BackColor != Color.DarkRed && c.BackColor != Color.Yellow)
                            c.BackColor = Color.LightGreen;
                        else if (c.BackColor == Color.DarkRed)
                        {
                            ((CustomButton)c).BackColor = Color.Blue;
                            tempNoAppear[Convert.ToByte(((CustomButton)c).Number)]++;
                            dateAppear.Add(Convert.ToDateTime(((CustomButton)c).DatePublish).Date);
                        }
                    }
            }
            //string temp = null;
            //for (byte i = 1; i < tempNoAppear.Count(); i++)
            //{
            //    temp += i + "(" + tempNoAppear[i] + "),";
            //}
            //this.lblNoAppear.Text += temp;

            ////Count Next Appear
            //int[] nextNoAppear = new int[56];//Dai dien 56 so     
            //List<int> numberNextAppear = _6Over55TimeLine.GetNumberNextAppear(dateAppear);
            //for (byte i = 0; i < numberNextAppear.Count(); i++)
            //{
            //    nextNoAppear[numberNextAppear[i]]++;
            //}
            //temp = null;
            //for (byte i = 1; i < nextNoAppear.Count(); i++)
            //{
            //    temp += i + "(" + nextNoAppear[i] + "),";
            //}
            //this.lblNextNoAppear.Text = temp;
        }

        private void NumberButtonClick(object sender, EventArgs e)
        {          
            new _FullShowNumber(((CustomButton)sender).Number, Enum_NumberType._6Over55, _Full6Over55No).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.toolStripTextBoxBetTip.Text))
                Common.AddTipBet(this.toolStripTextBoxBetTip.Text.Trim());          
        }

        private void toolStripButtonSoChuaXuatHien_Click(object sender, EventArgs e)
        {
            new _NumberNotAppear(_6Over55No).ShowDialog();
        }

        private void toolStripButtonShowTipBet_Click(object sender, EventArgs e)
        {
            new TipBet().ShowDialog();
        }

        private void toolStripButtonCombination_Click(object sender, EventArgs e)
        {           
            if (toolStripNo1.Text.Trim().Length >= 2 && toolStripNo2.Text.Trim().Length >= 2 && toolStripNo3.Text.Trim().Length >= 2 && toolStripNo4.Text.Trim().Length >= 2 &&
                toolStripNo5.Text.Trim().Length >= 2 && toolStripNo6.Text.Trim().Length >= 2 && toolStripNo7.Text.Trim().Length >= 2)
            {
                char a = '@';
                byte count = 0;
                List<string> result = new List<string>();
                string[] no = new string[8];
                no[0] = toolStripNo1.Text.Trim();
                no[1] = toolStripNo2.Text.Trim();
                no[2] = toolStripNo3.Text.Trim();
                no[3] = toolStripNo4.Text.Trim();
                no[4] = toolStripNo5.Text.Trim();
                no[5] = toolStripNo6.Text.Trim();
                no[6] = toolStripNo7.Text.Trim();
                no[7] = toolStripNo8.Text.Trim();

                string[] firstTemp = new string[6];
                string[] secondTemp = new string[6];

                for (byte i = 0; i < 6; i++)
                {
                    firstTemp[i] = no[i];
                }
                result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, firstTemp[0], firstTemp[1], firstTemp[2], firstTemp[3], firstTemp[4], firstTemp[5]));

                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length; j++)
                    {
                        secondTemp[j] = no[i];
                        result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));                        
                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                }

                //Replace 2 number next to
                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length - (no.Length - 6 - 1); j++) //Replace 2 number next to
                    {
                        if (i < no.Length - 1)
                        {
                            secondTemp[j] = no[i];
                            secondTemp[j + 1] = no[i + 1];
                            result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));

                        }
                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                }

                //Replace 2 number Distance  1
                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length; j++)
                    {
                        secondTemp[j] = no[i];
                        if (j < 4)
                        {
                            secondTemp[j + 2] = no[7];
                            result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));
                        }

                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                    break;
                }

                //Replace 2 number Distance  2
                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length; j++)
                    {
                        secondTemp[j] = no[i];
                        if (j < 3)
                        {
                            secondTemp[j + 3] = no[7];
                            result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));
                        }

                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                    break;
                }

                //Replace 2 number Distance  3
                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length; j++)
                    {
                        secondTemp[j] = no[i];
                        if (j < 2)
                        {
                            secondTemp[j + 4] = no[7];
                            result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));
                        }

                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                    break;
                }

                //Replace 2 number Distance  4
                for (byte i = 6; i < no.Length; i++)
                {
                    #region Reset to firstTemp
                    for (byte k = 0; k < firstTemp.Length; k++)
                    {
                        secondTemp[k] = firstTemp[k];
                    }
                    #endregion
                    for (byte j = 0; j < secondTemp.Length; j++)
                    {
                        secondTemp[j] = no[i];
                        if (j < 1)
                        {
                            secondTemp[j + 5] = no[7];
                            result.Add(String.Format("{0}{1}: {2}---{3}---{4}---{5}---{6}---{7}", ++count, ++a, secondTemp[0], secondTemp[1], secondTemp[2], secondTemp[3], secondTemp[4], secondTemp[5]));
                        }

                        #region Reset to firstTemp
                        for (byte k = 0; k < firstTemp.Length; k++)
                        {
                            secondTemp[k] = firstTemp[k];
                        }
                        #endregion
                    }
                    break;
                }

                new NoBet(result, (short)Enum_NumberType._6Over55, (short)Enum_NumberWinLevel.DacBiet).ShowDialog();
            }
        }

        private void toolStripButtonWinAmount_Click(object sender, EventArgs e)
        {
            var input = this.toolStripTextBoxWinedAmount.Text.Trim();
            long o;
            bool val = long.TryParse(input.Split('~')[0], out o);
            if (val)
            {
                Common.AddWinnedAmount(input);
                this.toolStripTextBoxWinedAmount.Text = "Amount~MatchNo~FulllNo~Comment";
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Please enter number in winned amount textbox");
            }
        }

        private void toolStripButtonAddBet_Click(object sender, EventArgs e)
        {
            var input = this.toolStripTextBoxBetAmount.Text.Trim();
            long o;
            bool val = long.TryParse(input.Split('~')[0], out o);
            if (val)
            {
                Common.AddBettedAmount(input);
                this.toolStripTextBoxBetAmount.Text = "Amount~Comment";
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Please enter number in Bet amount textbox");
            }
        }

        private void toolStripButtonNewNumber_Click(object sender, EventArgs e)
        {
            new _New6Over55Number(this.datePublishFrom, this.datePublishTo).ShowDialog();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {

        }
    }
}
