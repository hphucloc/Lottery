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
    public partial class _NumberNotAppear : Form
    {
        List<LoterryStatistic> _6Over45No = null;
        public _NumberNotAppear(List<LoterryStatistic> _6Over45No)
        {
            InitializeComponent();

            this._6Over45No = _6Over45No;
        }
    }
}
