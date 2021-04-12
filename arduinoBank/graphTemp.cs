using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace arduinoBank
{
    public partial class graphTemp : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dts { get; set; }
        public graphTemp()
        {
            InitializeComponent();
        }
        public graphTemp(DataTable dt)
        {
            InitializeComponent();
            dts = dt;
        }
        public void showgraph()
        {
            Series series = new Series("ค่าอุณหภูมิ", ViewType.Bar);
            chartControl1.Series.Add(series);
            // Generate a data table and bind the series to it.
            series.DataSource = dts;

            // Specify data members to bind the series.
            series.ArgumentScaleType = ScaleType.Numerical;
            series.ArgumentDataMember = "RUNNING";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "TEMP" });
            SideBySideBarSeriesView view2 = series.View as SideBySideBarSeriesView;
            view2.Color = Color.Red;


        }



        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if ((int)e.SeriesPoint.NumericalArgument > 0)
            {
                e.SeriesDrawOptions.Color = Color.Red;
                
            }
        }
    }
}
