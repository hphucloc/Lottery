using LotteryBusiness;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LotteryUI
{
    public partial class NoBet : Form
    {
        private short _numberType = 0;
        private short _numberWinLevel = 0;
        
        public NoBet(List<string> noBet, short numberType, short numberWinLevel)
        {
            _numberType = numberType;
            _numberWinLevel = numberWinLevel;
           
            InitializeComponent();
         
            foreach(var i in noBet)
            {
                this.listBoxNoBet.Items.Add(i);
            }

            //DateTime nextday = DateTime.Now;
            //if (numberType == (short)Enum_NumberType._6Over55 && 
            //   (nextday.DayOfWeek == DayOfWeek.Monday || nextday.DayOfWeek == DayOfWeek.Wednesday || nextday.DayOfWeek == DayOfWeek.Friday))
            //    nextday = nextday.AddDays(1);
            //else if (nextday.DayOfWeek == DayOfWeek.Sunday)
            //    nextday = nextday.AddDays(3);
            //this.dateTimePickerDateBought.Value = nextday;
        }

        private void listBoxNoBet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_numberType == (short)Enum_NumberType._6Over45)
            {
                var date = this.dateTimePickerDateBought.Value;
                if (date.DayOfWeek == DayOfWeek.Tuesday ||
                    date.DayOfWeek == DayOfWeek.Thursday ||
                    date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Monday)
                {
                    MessageBox.Show("6/45 khong xuat hien ngay da chon.");
                    return;
                }

            }
            else
            if (_numberType == (short)Enum_NumberType._6Over55)
            {
                var date = this.dateTimePickerDateBought.Value;
                if (date.DayOfWeek == DayOfWeek.Monday ||
                    date.DayOfWeek == DayOfWeek.Wednesday ||
                    date.DayOfWeek == DayOfWeek.Friday ||
                    date.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show("6/55 khong xuat hien ngay da chon.");
                    return;
                }
            }
            

            if (listBoxNoBet.SelectedItem != null)
            {
                //MessageBox.Show(listBoxNoBet.SelectedItem.ToString());
                var numbers = listBoxNoBet.SelectedItem.ToString().Split(':')[1].Split(new string[1] { "---" }, StringSplitOptions.RemoveEmptyEntries);
                List<int> nos = new List<int>();
                foreach (string i in numbers)
                {
                    nos.Add(Convert.ToInt32(i));
                }

                Common.CreateBoughtNumber(nos, dateTimePickerDateBought.Value, _numberType, _numberWinLevel);
                MessageBox.Show("Saved");

                for (byte i = 0; i < listBoxNoBet.Items.Count; i++)
                {
                    if (listBoxNoBet.Items[i].ToString() == listBoxNoBet.SelectedItem.ToString())
                    {
                        listBoxNoBet.Items.RemoveAt(i);
                        break;
                    }
                }
            }            
        }
    }
}
