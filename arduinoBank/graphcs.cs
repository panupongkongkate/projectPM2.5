using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arduinoBank
{
    public partial class graphcs : Form
    {
        public DataTable dts = new DataTable();
        graphreal g1 = new graphreal();
        GraphPM1cs gPM1 = new GraphPM1cs();
        graphPM10 gPM10 = new graphPM10();
        graphTemp gtemp = new graphTemp();
        public graphcs(DataTable dt)
        {
            InitializeComponent();
            dts = dt;
            g1.dts = dt;
            gPM10.dts = dt;
            gPM1.dts = dt;
            gtemp.dts = dt;
        }

        private void graphcs_Load(object sender, EventArgs e)
        {
            g1.Dock = DockStyle.Fill;
            g1.showgraph();
            gPM1.Dock = DockStyle.Fill;
            gPM1.showgraph();
            gPM10.Dock = DockStyle.Fill;
            gPM10.showgraph();
            gtemp.Dock = DockStyle.Fill;
            gtemp.showgraph();

            panel2.Controls.Clear();
        }

        private void _btngraph_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(g1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(gPM1);
        }

        private void _btnPM10_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(gPM10);
        }

        private void _btnTemp_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(gtemp);
        }
    }
}
