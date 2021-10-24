using System;
using System.Collections.Generic;
using LotteryBusiness;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LotteryUI
{
    public partial class _4DMax : Form
    {
        private List<LoterryStatistic> _4DMaxGiaiNhat { get; set; }
        private List<LoterryStatistic> _4DMaxGiaiNhi { get; set; }
        private List<LoterryStatistic> _4DMaxGiaiBa { get; set; }
        private List<LoterryStatistic> _4DMaxKK1 { get; set; }
        private List<LoterryStatistic> _4DMaxKK2 { get; set; }
        private DateTime DatePublishFrom { get; set; }
        private DateTime DatePublishTo { get; set; }
        public _4DMax(DateTime datePublishFrom, DateTime datePublishTo)
        {
            InitializeComponent();

            this.DatePublishFrom = datePublishFrom;
            this.DatePublishTo = datePublishTo;

            _4DMaxGiaiNhat = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberGiaiNhat(this.DatePublishFrom, this.DatePublishTo);
            this.labelMinDate.Text = _4DMaxGiaiNhat[0].DatePublishMin.ToShortDateString();
            this.labelMaxDate.Text = _4DMaxGiaiNhat[0].DatePublishMax.ToShortDateString();
            DrawTimeLine(_4DMaxGiaiNhat);           
        }

        private void DrawTimeLine(List<LoterryStatistic> Giai)
        {
            int h = 40;

            var HeaderNo = new Label()
            {
                Text = "Số",
            };
            HeaderNo.Size = new Size(60, 20);
            HeaderNo.Location = new Point(10, h - 20);
            HeaderNo.Font = new Font("Arial Black", 8.25F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));           
            switch (this.tabControl1.SelectedTab.Name)
            {
                case "tabPageGiaiNhat":
                    this.panel1.Controls.Add(HeaderNo);
                    break;
                case "tabPageGiaiNhi":
                    this.panel2.Controls.Add(HeaderNo);
                    break;
                case "tabPageGiaiBa":
                    this.panel3.Controls.Add(HeaderNo);
                    break;
                case "tabPageKhuyenKhich1":
                    this.panelKK1.Controls.Add(HeaderNo);
                    break;
                case "tabPageKhuyenKhich2":
                    this.panelKK2.Controls.Add(HeaderNo);
                    break;
            }

            var HeaderAppear = new Label()
            {
                Text = "Số lần xuất hiện",
            };
            HeaderAppear.Size = new Size(150, 20);
            HeaderAppear.Location = new Point(70, h - 20);
            HeaderAppear.Font = new Font("Arial Black", 8.25F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));
            switch (this.tabControl1.SelectedTab.Name)
            {
                case "tabPageGiaiNhat":
                    this.panel1.Controls.Add(HeaderAppear);
                    break;
                case "tabPageGiaiNhi":
                    this.panel2.Controls.Add(HeaderAppear);
                    break;
                case "tabPageGiaiBa":
                    this.panel3.Controls.Add(HeaderAppear);
                    break;
                case "tabPageKhuyenKhich1":
                    this.panelKK1.Controls.Add(HeaderAppear);
                    break;
                case "tabPageKhuyenKhich2":
                    this.panelKK2.Controls.Add(HeaderAppear);
                    break;
            }
            

            foreach (var aNo in Giai)
            {
                int w = 60;
                //Number
                var no = new CustomLabel()
                {
                    Text = aNo.LotNumber.ToString()
                };
                no.Size = new Size(60, 20);
                no.Location = new Point(10, h);
                no.ForeColor = Color.Black;
                no.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));               
                switch (this.tabControl1.SelectedTab.Name)
                {
                    case "tabPageGiaiNhat":
                        this.panel1.Controls.Add(no);
                        break;
                    case "tabPageGiaiNhi":
                        this.panel2.Controls.Add(no);
                        break;
                    case "tabPageGiaiBa":
                        this.panel3.Controls.Add(no);
                        break;
                    case "tabPageKhuyenKhich1":
                        this.panelKK1.Controls.Add(no);
                        break;
                    case "tabPageKhuyenKhich2":
                        this.panelKK2.Controls.Add(no);
                        break;
                }

                //Total appear
                var sta1 = new Label()
                {
                    Text = aNo.DatePublishList.DatePublishList1.Count().ToString()
                };
                sta1.Size = new Size(20, 20);
                sta1.Location = new Point(70, h);               
                switch (this.tabControl1.SelectedTab.Name)
                {
                    case "tabPageGiaiNhat":
                        this.panel1.Controls.Add(sta1);
                        break;
                    case "tabPageGiaiNhi":
                        this.panel2.Controls.Add(sta1);
                        break;
                    case "tabPageGiaiBa":
                        this.panel3.Controls.Add(sta1);
                        break;
                    case "tabPageKhuyenKhich1":
                        this.panelKK1.Controls.Add(sta1);
                        break;
                    case "tabPageKhuyenKhich2":
                        this.panelKK2.Controls.Add(sta1);
                        break;
                }

                foreach (var a in aNo.AllDatePublishList)
                {
                    CustomButton b = new CustomButton();
                    b.Size = new Size(15, 15);
                    b.Location = new Point(w, h);
                    b.Number = aNo.LotNumber.ToString();
                    b.DatePublish = a.ToShortDateString();
                    b.Click += NumberButtonClick;
                    b.MouseEnter += NumberButtonMouseEnter;
                    b.MouseLeave += NumberButtonMouseLeave;
                    foreach (var c in aNo.DatePublishList.DatePublishList1)
                    {
                        if (a.Equals(c))
                        {
                            b.BackColor = Color.DarkRed;
                        }
                    }                  
                    switch (this.tabControl1.SelectedTab.Name)
                    {
                        case "tabPageGiaiNhat":
                            panel1.Controls.Add(b);
                            break;
                        case "tabPageGiaiNhi":
                            panel2.Controls.Add(b);
                            break;
                        case "tabPageGiaiBa":
                            panel3.Controls.Add(b);
                            break;
                        case "tabPageKhuyenKhich1":
                            panelKK1.Controls.Add(b);
                            break;
                        case "tabPageKhuyenKhich2":
                            panelKK2.Controls.Add(b);
                            break;
                    }
                    w += 15;
                }

                h += 20;
            }            
        }

        #region
        private void NumberButtonMouseLeave(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is CustomLabel)
                    if (((CustomLabel)c).Text.Equals(((CustomButton)sender).Number))
                    {
                        c.ForeColor = Color.Black;
                        c.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
                        switch (this.tabControl1.SelectedTab.Name)
                        {
                            case "tabPageGiaiNhat":
                                this.labelMessage1.Text = null;
                                break;
                            case "tabPageGiaiNhi":
                                this.labelG2Message.Text = null;
                                break;
                            case "tabPageGiaiBa":
                                this.labelG3Message.Text = null;
                                break;
                            case "tabPageKhuyenKhich1":
                                this.labelKK1Message.Text = null;
                                break;
                            case "tabPageKhuyenKhich2":
                                this.labelKK2Message.Text = null;
                                break;
                        }
                    }
            }
        }

        private void NumberButtonMouseEnter(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is CustomLabel)
                    if (((CustomLabel)c).Text.Equals(((CustomButton)sender).Number))
                    {
                        c.ForeColor = Color.Red;
                        c.Font = new Font("Arial", 10.0F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(163)));
                       
                        switch (this.tabControl1.SelectedTab.Name)
                        {
                            case "tabPageGiaiNhat":
                                this.labelMessage1.Text = "Số: " + ((CustomLabel)c).Text + ", ngày: " + ((CustomButton)sender).DatePublish;
                                break;
                            case "tabPageGiaiNhi":
                                this.labelG2Message.Text = "Số: " + ((CustomLabel)c).Text + ", ngày: " + ((CustomButton)sender).DatePublish;
                                break;
                            case "tabPageGiaiBa":
                                this.labelG3Message.Text = "Số: " + ((CustomLabel)c).Text + ", ngày: " + ((CustomButton)sender).DatePublish;
                                break;
                            case "tabPageKhuyenKhich1":
                                this.labelKK1Message.Text = "Số: " + ((CustomLabel)c).Text + ", ngày: " + ((CustomButton)sender).DatePublish;
                                break;
                            case "tabPageKhuyenKhich2":
                                this.labelKK2Message.Text = "Số: " + ((CustomLabel)c).Text + ", ngày: " + ((CustomButton)sender).DatePublish;
                                break;
                        }
                    }
            }
        }

        private void NumberButtonClick(object sender, EventArgs e)
        {
            
            switch (this.tabControl1.SelectedTab.Name)
            {
                case "tabPageGiaiNhat":
                    this.labelMessage1.Text = "Ngày sổ: " + ((CustomButton)sender).DatePublish;
                    break;
                case "tabPageGiaiNhi":
                    this.labelG2Message.Text = "Ngày sổ: " + ((CustomButton)sender).DatePublish;
                    break;
                case "tabPageGiaiBa":
                    this.labelG3Message.Text = "Ngày sổ: " + ((CustomButton)sender).DatePublish;
                    break;
                case "tabPageKhuyenKhich1":
                    this.labelKK1Message.Text = "Ngày sổ: " + ((CustomButton)sender).DatePublish;
                    break;
                case "tabPageKhuyenKhich2":
                    this.labelKK2Message.Text = "Ngày sổ: " + ((CustomButton)sender).DatePublish;
                    break;
            }
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((TabControl)sender).SelectedTab.Name)
            {
                case "tabPageGiaiNhat":
                    this.labelMessage1.Text = "Đang tải dữ liệu,vui lòng đợi trong giây lát..........";
                    _4DMaxGiaiNhat = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberGiaiNhat(this.DatePublishFrom, this.DatePublishTo);
                    this.labelMinDate.Text = _4DMaxGiaiNhat[0].DatePublishMin.ToShortDateString();
                    this.labelMaxDate.Text = _4DMaxGiaiNhat[0].DatePublishMax.ToShortDateString();

                    DrawTimeLine(_4DMaxGiaiNhat);
                    break;
                case "tabPageGiaiNhi":
                    this.labelG2Message.Text = "Đang tải dữ liệu,vui lòng đợi trong giây lát..........";
                    _4DMaxGiaiNhi = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberGiaiNhi(this.DatePublishFrom, this.DatePublishTo);
                    this.labelG2MinDate.Text = _4DMaxGiaiNhi[0].DatePublishMin.ToShortDateString();
                    this.labelG2MaxDate.Text = _4DMaxGiaiNhi[0].DatePublishMax.ToShortDateString();

                    DrawTimeLine(_4DMaxGiaiNhi);
                    break;
                case "tabPageGiaiBa":
                    this.labelG3Message.Text = "Đang tải dữ liệu,vui lòng đợi trong giây lát..........";
                    _4DMaxGiaiBa = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberGiaiBa(this.DatePublishFrom, this.DatePublishTo);
                    this.labelG3MinDate.Text = _4DMaxGiaiBa[0].DatePublishMin.ToShortDateString();
                    this.labelG3MaxDate.Text = _4DMaxGiaiBa[0].DatePublishMax.ToShortDateString();

                    DrawTimeLine(_4DMaxGiaiBa);
                    break;
                case "tabPageKhuyenKhich1":
                    this.labelKK1Message.Text = "Đang tải dữ liệu,vui lòng đợi trong giây lát..........";
                    _4DMaxKK1 = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberKhuyenKhich1(this.DatePublishFrom, this.DatePublishTo);
                    this.labelKK1MinDate.Text = _4DMaxKK1[0].DatePublishMin.ToShortDateString();
                    this.labelKK1MaxDate.Text = _4DMaxKK1[0].DatePublishMax.ToShortDateString();

                    DrawTimeLine(_4DMaxKK1);
                    break;
                case "tabPageKhuyenKhich2":
                    this.labelKK2Message.Text = "Đang tải dữ liệu,vui lòng đợi trong giây lát..........";
                    _4DMaxKK2 = LotteryBusiness._4DMaxTimeLine.Get6Over45NumberKhuyenKhich2(this.DatePublishFrom, this.DatePublishTo);
                    this.labelKK2MinDate.Text = _4DMaxKK2[0].DatePublishMin.ToShortDateString();
                    this.labelKK2MaxDate.Text = _4DMaxKK2[0].DatePublishMax.ToShortDateString();

                    DrawTimeLine(_4DMaxKK2);
                    break;
            }
        }
    }
}
