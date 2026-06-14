using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            MessageBox.Show("Form đã được load");
            ucQLSV ucQLSV = new ucQLSV();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(ucQLSV);
            quảnLýLớpHọcToolStripMenuItem.ForeColor = Color.Black;
            quảnLýSinhViênToolStripMenuItem.ForeColor = Color.Red;
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucQLLH ucQLLH = new ucQLLH();
            ucQLLH.classSelected += ucQLLH_to_ucQLSV;
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(ucQLLH);
            quảnLýLớpHọcToolStripMenuItem.ForeColor = Color.Red;
            quảnLýSinhViênToolStripMenuItem.ForeColor = Color.Black;

        }

        private void ucQLLH_to_ucQLSV(string malop)
        {
            ucQLSV ucQLSV = new ucQLSV(malop);
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(ucQLSV);
            quảnLýLớpHọcToolStripMenuItem.ForeColor = Color.Black;
            quảnLýSinhViênToolStripMenuItem.ForeColor = Color.Red;
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucQLSV ucQLSV = new ucQLSV();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(ucQLSV);
            quảnLýLớpHọcToolStripMenuItem.ForeColor = Color.Black;
            quảnLýSinhViênToolStripMenuItem.ForeColor = Color.Red;
        }
    }
}
