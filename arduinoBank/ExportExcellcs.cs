using DevExpress.Spreadsheet;
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
    public partial class ExportExcellcs : Form
    {
        DataTable dts = new DataTable();

        public ExportExcellcs(DataTable dt)
        {
            InitializeComponent();
            dts = dt;
        }

        private void gridControl1_Click(object sSender, EventArgs e)
        {

        }

        private void ExportExcellcs_Load(object sender, EventArgs e)
        {

            gridControl1.DataSource = null;
            gridControl1.DataSource = dts;


        }
    
        private void _btn_Click(object sender, EventArgs e)
        {
            if(dts.Rows.Count <= 0)
            {
                return;
            }
            try
            {
                using (SaveFileDialog sa = new SaveFileDialog())
                {
                    sa.Filter = "Execl files (*.xls)|*.xls";
                    sa.FilterIndex = 0;
                    sa.Title = "Export Excel File To";
                    if(sa.ShowDialog() == DialogResult.OK)
                    {
                       
                        Workbook wb = new Workbook();
                        wb.Worksheets[0].Import(dts, true, 0, 0);
                        wb.SaveDocument(sa.FileName, DevExpress.Spreadsheet.DocumentFormat.Xls);
                    }
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
            finally
            {
              
            }
        }

        private void _btngraph_Click(object sender, EventArgs e)
        {
            try
            {

                graphcs f = new graphcs(dts);
                f.ShowDialog();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
    }
}
