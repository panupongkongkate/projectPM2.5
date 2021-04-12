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
    public partial class graphreal : DevExpress.XtraEditors.XtraUserControl
    {
     public DataTable dts {get;set;}
        public graphreal(DataTable dt)
        {
            InitializeComponent();
            dt = dts;
        }
        public graphreal()
        {
            InitializeComponent();
        }
        public void showgraph()
        {
            Series series = new Series("ค่าฝุ่นPM2.5", ViewType.Bar);
            chartControl1.Series.Add(series);
            // Generate a data table and bind the series to it.
            series.DataSource = dts;

            // Specify data members to bind the series.
            series.ArgumentScaleType = ScaleType.Numerical;
            series.ArgumentDataMember = "RUNNING";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "PM2.5" });
            SideBySideBarSeriesView view2 = series.View as SideBySideBarSeriesView;
            view2.Color = Color.Green;


        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if ((int)e.SeriesPoint.NumericalArgument > 0)
            {
                e.SeriesDrawOptions.Color = Color.Green;
               
            }
        }
    }
}
