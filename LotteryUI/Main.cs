using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LotteryUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            var xml = XDocument.Load(@"lotteryConfig.xml");            
            var data = xml.Descendants("mindateNumber");            
            foreach (XElement b in data)
            {             
                dateTimePickerMinDate.Value = dateTimePickerMinDate.Value.AddDays(Convert.ToInt32(b.Value.Trim()) * -1);
            }
           
        }

        private void Sleep(int v)
        {
            throw new NotImplementedException();
        }

        private void buttonGetNumber_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("LotteryConsoleApplication.exe");
        }

        private void checkBoxMinDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMinDate.Checked)
                this.dateTimePickerMinDate.Enabled = false;
            else
                this.dateTimePickerMinDate.Enabled = true;
        }

        private void checkBoxMaxDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMaxDate.Checked)
                dateTimePickerMaxDate.Enabled = false;
            else
                dateTimePickerMaxDate.Enabled = true;
        }

        private void button6Over45_Click(object sender, EventArgs e)
        {
            DateTime datePublishFrom = !checkBoxMinDate.Checked ?
                 dateTimePickerMinDate.Value
                : DateTime.MinValue;
            DateTime datePublishTo = checkBoxMaxDate.Checked ? DateTime.MaxValue : dateTimePickerMaxDate.Value;        
            new _6Over45(datePublishFrom, datePublishTo).Show();
        }

        private void button4Dmax_Click(object sender, EventArgs e)
        {
            DateTime datePublishFrom = checkBoxMinDate.Checked ? DateTime.MinValue : dateTimePickerMinDate.Value;
            DateTime datePublishTo = checkBoxMaxDate.Checked ? DateTime.MaxValue : dateTimePickerMaxDate.Value;
            new _4DMax(datePublishFrom, datePublishTo).Show();
        }

        private void button6Over55_Click(object sender, EventArgs e)
        {
            DateTime datePublishFrom = !checkBoxMinDate.Checked ?
                dateTimePickerMinDate.Value
               : DateTime.MinValue;
            DateTime datePublishTo = checkBoxMaxDate.Checked ? DateTime.MaxValue : dateTimePickerMaxDate.Value;          
            new _6Over55(datePublishFrom, datePublishTo).Show();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
