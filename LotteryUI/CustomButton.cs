using System.Windows.Forms;

namespace LotteryUI
{
    class CustomButton : Button
    {
        public CustomButton()
        {
            this.BackColor = System.Drawing.Color.SeaShell;     
            this.Font = new System.Drawing.Font("Arial", (float)4.0);
        }
        public string DatePublish { get; set; }
        public string Number { get; set; }
        public int VerticalNumber { get; set; }
        public int HorizontalNumber { get; set; }
    }
}
