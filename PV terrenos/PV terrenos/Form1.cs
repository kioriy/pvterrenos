using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PV_terrenos
{
    public partial class WfComprador : Form
    {
        public WfComprador()
        {
            InitializeComponent();
        }

        private void predioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPredio predio = new frmPredio();
            predio.Show();
        }

       
    }
}
