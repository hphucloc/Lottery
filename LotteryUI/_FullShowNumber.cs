using LotteryBusiness;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryUI
{
    public partial class _FullShowNumber : Form
    {
        private string Number { get; set; }
        private List<LoterryStatistic> _LotNumber { get; set; }
        public _FullShowNumber(string number, Enum_NumberType numberType, List<LoterryStatistic> LotNumber)
        {            
            InitializeComponent();

            Number = number;
            this.Text = "Full show number for: " + number;
            _LotNumber = LotNumber;

            DrawTimeLine();
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
            int countButton = 0;
            foreach (var aNo in _LotNumber)
            {
                if (aNo.LotNumber == Convert.ToInt32(Number))
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

                    foreach (var a in aNo.AllDatePublishList)
                    {
                        CustomButton b = new CustomButton();
                        b.Size = new Size(17, 17);
                        b.Location = new Point(w, h);
                        b.Number = aNo.LotNumber.ToString();
                        b.DatePublish = a.ToShortDateString();
                        // b.MouseEnter += NumberButtonMouseEnter;
                        //b.MouseLeave += NumberButtonMouseLeave;
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

                        this.panel1.Controls.Add(b);
                        w += 15;
                        countButton++;
                        if (countButton >= 85)
                        {
                            w = 70;
                            h += 40;
                            countButton = 0;
                        }
                    }

                    
                    countNo = 0;
                    horNo = 0;
                }
            }
        }
    }
}
