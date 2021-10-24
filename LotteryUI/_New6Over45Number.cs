using LotteryBusiness;
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
    public partial class _New6Over45Number : Form
    {
        private DateTime datePublishFrom;
        private DateTime datePublishTo;
        public _New6Over45Number(DateTime datePublishFrom, DateTime datePublishTo)
        {
            this.datePublishFrom = datePublishFrom;
            this.datePublishTo = datePublishTo;
            InitializeComponent();         
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            List<int> number = new List<int>() { Convert.ToInt32(textBoxNo1.Text), Convert.ToInt32(textBoxNo2.Text), Convert.ToInt32(textBoxNo3.Text),
                Convert.ToInt32(textBoxNo4.Text), Convert.ToInt32(textBoxNo5.Text), Convert.ToInt32(textBoxNo6.Text)};

            LotteryBusiness._6Over45TimeLine.NewNumber(this.dateTimePickerpublishDate.Value, number);
            textBoxNo1.Text = null;
            textBoxNo2.Text = null;
            textBoxNo3.Text = null;
            textBoxNo4.Text = null;
            textBoxNo5.Text = null;
            textBoxNo6.Text = null;
            DateTime nextday = this.dateTimePickerpublishDate.Value;
            if (nextday.DayOfWeek == DayOfWeek.Wednesday || nextday.DayOfWeek == DayOfWeek.Friday)
                nextday = nextday.AddDays(2);
            else if (nextday.DayOfWeek == DayOfWeek.Sunday)
                nextday = nextday.AddDays(3);
            this.dateTimePickerpublishDate.Value = nextday;
            MessageBox.Show("Saved");           
        }
    }
}
