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
    public partial class TipBet : Form
    {
        public TipBet()
        {
            InitializeComponent();

            this.listBoxTipBet.DataSource = LotteryBusiness.Common.GetNumberTipBet();
            this.listBoxTipBet.DisplayMember = "Label";
            this.listBoxTipBet.ValueMember = "ID";
        }
    }
}
