using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ZedGraph;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;


namespace arduinoBank
{
    public partial class Form1 : Form
    {
        public static SerialPort port;
        public string dat = string.Empty;
        public string pathfile;
        public string pathfilePM1;
        public string pathfilePM10;
        public string pathfileTEMP;
        public string Autosendpic = "PM25";
        public bool checkgrid = false;
        int iTemp = 1;
        DataTable dtExport = new DataTable();
        int TickStart;
        int  intMode = 1;
        DateTime dt = new DateTime();
        private string token = "tc6lxW40MterWEP2ZsDrKPWByz6wSwCDsjPnG5ogFEj";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _cboadRate.SelectedIndex = 0;
                string[] ports = SerialPort.GetPortNames();

                if(string.IsNullOrEmpty(ports.ToString()))
                {
                    return;
                }
                if(ports.Count() == 0)
                {
                    return;
                }
                foreach(string portreal in ports )
                {
                    _cboPort.Items.Add(portreal);
                }
              
                _cboPort.SelectedIndex = 0;

                TickStart = Environment.TickCount;
                intMode = 1;
                dtExport.Columns.Add("RUNNING",typeof(Int32));
                dtExport.Columns.Add("DAY");
                dtExport.Columns.Add("TIME");
                dtExport.Columns.Add("PM2.5", typeof(Int32));
                dtExport.Columns.Add("PM1", typeof(Int32));
                dtExport.Columns.Add("PM10", typeof(Int32));
                dtExport.Columns.Add("TEMP", typeof(Int32));

                

                Image image = Image.FromFile("C:\\Users\\57120042\\Desktop\\microgear-csharp-master\\Bank\\LOGO.jpg");
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        private void setGrapPM1()
        {
            GraphPane mypane = _ZGCPM1.GraphPane;
            mypane.Title.Text = "PM1";
            mypane.XAxis.Title.Text = "เวลา";
            mypane.YAxis.Title.Text = "ปริมาณฝุ่น";
            RollingPointPairList list = new RollingPointPairList(500);
            LineItem curve = mypane.AddCurve("PM1", list, Color.DarkBlue, SymbolType.Default);
            //mypane.XAxis.Scale.Min = 0;

            //mypane.XAxis.Scale.Max = 60;

            mypane.YAxis.Scale.Min = 0;

            mypane.YAxis.Scale.Max = 350;


            _ZGCPM1.AxisChange();
        }

        private void setGrapPM10()
        {
            GraphPane mypane = _ZGCPM10.GraphPane;
            mypane.Title.Text = "PM10";
            mypane.XAxis.Title.Text = "เวลา";
            mypane.YAxis.Title.Text = "ปริมาณฝุ่น";
            RollingPointPairList list = new RollingPointPairList(500);
            LineItem curve = mypane.AddCurve("PM10", list, Color.Green, SymbolType.Default);
            //mypane.XAxis.Scale.Min = 0;

            //mypane.XAxis.Scale.Max = 60;

            mypane.YAxis.Scale.Min = 0;

            mypane.YAxis.Scale.Max = 350;


            _ZGCPM10.AxisChange();
        }

        private void setGrapTEMP()
        {
            GraphPane mypane = _zgcTemp.GraphPane;
            mypane.Title.Text = "TEMP";
            mypane.XAxis.Title.Text = "เวลา";
            mypane.YAxis.Title.Text = "อุณหภูมิ";
            RollingPointPairList list = new RollingPointPairList(500);
            LineItem curve = mypane.AddCurve("TEMP", list, Color.Violet , SymbolType.Default);
            //mypane.XAxis.Scale.Min = 0;

            //mypane.XAxis.Scale.Max = 60;

            mypane.YAxis.Scale.Min = 0;


            _zgcTemp.AxisChange();
        }

        private void setGrapPM25()
        {
            GraphPane mypane = _zedPM25.GraphPane;
            mypane.Title.Text = "PM2.5";
            mypane.XAxis.Title.Text = "เวลา";
            mypane.YAxis.Title.Text = "ปริมาณฝุ่น";
            RollingPointPairList list = new RollingPointPairList(500);
            LineItem curve = mypane.AddCurve("PM2.5", list, Color.Red, SymbolType.Default);
            //mypane.XAxis.Scale.Min = 0;

            //mypane.XAxis.Scale.Max = 60;

            mypane.YAxis.Scale.Min = 0;

            mypane.YAxis.Scale.Max = 350;


            _zedPM25.AxisChange();
        }

        private void _cboPort_MouseClick(object sender, MouseEventArgs e)
        {
 
            try
            {
                _cboPort.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                if (string.IsNullOrEmpty(ports.ToString()))
                {
                    return;
                }
                if (ports.Count() == 0)
                {
                    return;
                }
                _cboPort.Items.AddRange(ports);
                _cboPort.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        private void _btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_cboPort.ToString()))
                {
                    return;
                }
   
                _btnConnect.Enabled = false;
                _btnclose.Enabled = true;
                _btnSave.Enabled = false;
                button1.Enabled = false;
                _cboPort.Enabled = false;
                _cboadRate.Enabled = false;
                if (string.IsNullOrEmpty( _cboPort.Text ))
                {
                    return;
                }

                port = new SerialPort(_cboPort.Text , int.Parse(_cboadRate.Text));
                port.Open();

                try
                {
                    MessageBox.Show("เชื่อมต่อแล้ว");
                    if(!checkgrid)
                    {
                        checkgrid = true;
                        setGrapPM25();
                        setGrapPM1();
                        setGrapPM10();
                        setGrapTEMP();
                    }

                    if (port.IsOpen)
                    {
                        port.DataReceived += new SerialDataReceivedEventHandler(portDatareceive);
                    }
                }
                catch 
                {

                    throw;
                }
     
            }
            catch (Exception Ex )
            {
                _btnConnect.Enabled = true;
                MessageBox.Show(Ex.Message);
            }

        }

        private void portDatareceive(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                dat = port.ReadLine();
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { dat });
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        delegate void SetTextCallback(string text);

        private void SetText(string text)

        {
       
            dt = DateTime.Now;
            string[] textSum;
            textSum = text.Split(",".ToCharArray());

            Draw(textSum[0]);
            DrawPM1(textSum[1]);
            DrawPM10(textSum[2]);
            DrawTEMP(textSum[3]);
            setgridpm25(iTemp);
            setgridpm1(iTemp);
            setgridpm10(iTemp);
            setgridtemp(iTemp);
            dtExport.Rows.Add(iTemp, dt.ToLongDateString(), dt.ToLongTimeString(), textSum[0], textSum[1], textSum[2], textSum[3]);
            iTemp++;
            //เปลื่ยนแปลงค่าทีหลังได้ 
            if (int.Parse( textSum[0]) >= 295)
            {
                var zedGraph = new ZedGraphControl();
                zedGraph = _zedPM25;
                using (var g = zedGraph.CreateGraphics())
                {
                    zedGraph.MasterPane.ReSize(g, new RectangleF(0, 0, 700, 700));
                }
                zedGraph.MasterPane.GetImage().Save(Autosendpic);
                line_notify("ข้อความอัตโนมัติ แจ้งเตือน PM2.5 ", Autosendpic, dt);
                _zedPM25.Size = new Size(291, 352);
            }
            textBox1.Text = string.Empty;
            textBox1.AppendText("ค่าฝุ่นPM2.5  =" + textSum[0]);
            textBox1.AppendText("\r\n");
            textBox1.AppendText("ค่าฝุ่นPM1  =" + textSum[1]);
            textBox1.AppendText("\r\n");
            textBox1.AppendText("ค่าฝุ่นPM10  =" + textSum[2]);
            textBox1.AppendText("\r\n");
            textBox1.AppendText("ค่าอุณหูมิ  =" + textSum[3]);
            textBox1.AppendText("\r\n");
        }

        private void setgridtemp(int Temp)
        {
            GraphPane mypane = _zgcTemp.GraphPane;
            mypane.XAxis.Scale.Min = 0;
            mypane.XAxis.Scale.Max = Temp;
          
        }

        private void setgridpm10(int Temp)
        {
            GraphPane mypane = _ZGCPM10.GraphPane;
            mypane.XAxis.Scale.Min = 0;
            mypane.XAxis.Scale.Max = Temp;
         
        }

        private void setgridpm1(int Temp)
        {
            GraphPane mypane = _ZGCPM1.GraphPane;
            mypane.XAxis.Scale.Min = 0;
            mypane.XAxis.Scale.Max = Temp;
     
        }

        private void setgridpm25(int Temp)
        {
            GraphPane mypane = _zedPM25.GraphPane;
            mypane.XAxis.Scale.Min = 0;
            mypane.XAxis.Scale.Max = Temp;

    
        }

        private void _btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_cboPort.ToString()))
                {
                    return;
                }

                if (port.IsOpen)
                {
                    _btnConnect.Enabled = true;
                    button1.Enabled = true;
                    _btnSave.Enabled = true;
                    _btnclose.Enabled = false;
                    _cboPort.Enabled = true;
                    _cboadRate.Enabled = true;
                    dat = string.Empty;
                    port.Close();
                    MessageBox.Show("ยกเลิกการเชื่อมต่อ");
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        private void Draw(string setpoint)
        {
            double intsetpoint;
            double.TryParse(setpoint, out intsetpoint);
 
            if (_zedPM25.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve = _zedPM25.GraphPane.CurveList[0] as LineItem;

            if (curve == null)
            {
                return;
            }

            IPointListEdit list = curve.Points as IPointListEdit;

            if (list == null)
            {
                return;
            }

            double time = (Environment.TickCount - TickStart) / 1000.0;
            list.Add(time, intsetpoint);
            Scale xscale = _zedPM25.GraphPane.XAxis.Scale;
            if (time > xscale.Max - xscale.MajorStep)
            {
                if (intMode == 1)
                {
                    xscale.Max = time + xscale.MajorStep;
                  xscale.Min = xscale.Max - 10;
                }
                else
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = 0;
                }
            }
            _zedPM25.AxisChange();
            _zedPM25.Invalidate();
        }
        private void DrawPM1(string setpoint)
        {
            double intsetpoint;
            //double intcurrent;
            //double intcurrent1;
            double.TryParse(setpoint, out intsetpoint);
            //double.TryParse(current, out intcurrent);
            //double.TryParse(current1, out intcurrent1);
            if (_ZGCPM1.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve = _ZGCPM1.GraphPane.CurveList[0] as LineItem;
            //LineItem curve1 = zg1.GraphPane.CurveList[1] as LineItem;
            //LineItem curve2 = zg1.GraphPane.CurveList[2] as LineItem;
            if (curve == null)
            {
                return;
            }
            //if (curve1 == null)
            //{
            //    return;
            //}
            //if (curve2 == null)
            //{
            //    return;
            //}
            IPointListEdit list = curve.Points as IPointListEdit;
            //IPointListEdit list1 = curve1.Points as IPointListEdit;
            //IPointListEdit list2 = curve2.Points as IPointListEdit;
            if (list == null)
            {
                return;
            }
            //if (list1 == null)
            //{
            //    return;
            //}
            //if (list2 == null)
            //{
            //    return;
            //}
            double time = (Environment.TickCount - TickStart) / 1000.0;
            list.Add(time, intsetpoint);
            //list1.Add(time, intcurrent);
            //list2.Add(time, intcurrent1);
            Scale xscale = _ZGCPM1.GraphPane.XAxis.Scale;
            if (time > xscale.Max - xscale.MajorStep)
            {
                if (intMode == 1)
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = xscale.Max - 10;
                }
                else
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = 0;
                }
            }
           _ZGCPM1.AxisChange();
            _ZGCPM1.Invalidate();
        }

        private void DrawPM10(string setpoint)
        {
            double intsetpoint;
            //double intcurrent;
            //double intcurrent1;
            double.TryParse(setpoint, out intsetpoint);
            //double.TryParse(current, out intcurrent);
            //double.TryParse(current1, out intcurrent1);
            if (_ZGCPM10.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve = _ZGCPM10.GraphPane.CurveList[0] as LineItem;
            //LineItem curve1 = zg1.GraphPane.CurveList[1] as LineItem;
            //LineItem curve2 = zg1.GraphPane.CurveList[2] as LineItem;
            if (curve == null)
            {
                return;
            }
            //if (curve1 == null)
            //{
            //    return;
            //}
            //if (curve2 == null)
            //{
            //    return;
            //}
            IPointListEdit list = curve.Points as IPointListEdit;
            //IPointListEdit list1 = curve1.Points as IPointListEdit;
            //IPointListEdit list2 = curve2.Points as IPointListEdit;
            if (list == null)
            {
                return;
            }
            //if (list1 == null)
            //{
            //    return;
            //}
            //if (list2 == null)
            //{
            //    return;
            //}
            double time = (Environment.TickCount - TickStart) / 1000.0;
            list.Add(time, intsetpoint);
            //list1.Add(time, intcurrent);
            //list2.Add(time, intcurrent1);
            Scale xscale = _ZGCPM10.GraphPane.XAxis.Scale;
            if (time > xscale.Max - xscale.MajorStep)
            {
                if (intMode == 1)
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = xscale.Max - 10;
                }
                else
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = 0;
                }
            }
            _ZGCPM10.AxisChange();
            _ZGCPM10.Invalidate();
        }
        private void DrawTEMP(string setpoint)
        {
            double intsetpoint;
            //double intcurrent;
            //double intcurrent1;
            double.TryParse(setpoint, out intsetpoint);
            //double.TryParse(current, out intcurrent);
            //double.TryParse(current1, out intcurrent1);
            if (_zgcTemp.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve = _zgcTemp.GraphPane.CurveList[0] as LineItem;
            //LineItem curve1 = zg1.GraphPane.CurveList[1] as LineItem;
            //LineItem curve2 = zg1.GraphPane.CurveList[2] as LineItem;
            if (curve == null)
            {
                return;
            }
            //if (curve1 == null)
            //{
            //    return;
            //}
            //if (curve2 == null)
            //{
            //    return;
            //}
            IPointListEdit list = curve.Points as IPointListEdit;
            //IPointListEdit list1 = curve1.Points as IPointListEdit;
            //IPointListEdit list2 = curve2.Points as IPointListEdit;
            if (list == null)
            {
                return;
            }
            //if (list1 == null)
            //{
            //    return;
            //}
            //if (list2 == null)
            //{
            //    return;
            //}
            double time = (Environment.TickCount - TickStart) / 1000.0;
            list.Add(time, intsetpoint);
            //list1.Add(time, intcurrent);
            //list2.Add(time, intcurrent1);
            Scale xscale = _zgcTemp.GraphPane.XAxis.Scale;
            if (time > xscale.Max - xscale.MajorStep)
            {
                if (intMode == 1)
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = xscale.Max - 10;
                }
                else
                {
                    xscale.Max = time + xscale.MajorStep;
                    xscale.Min = 0;
                }
            }
            _zgcTemp.AxisChange();
            _zgcTemp.Invalidate();
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                using (SaveFileDialog sa = new SaveFileDialog())
                {
                    sa.Filter = "Images|*.png;";
                    //sa.Title = "Save PM1";
                    //if (sa.ShowDialog() == DialogResult.OK)
                    //{
                    //    Bitmap bm = new Bitmap(100, 100);
                    //    using (Graphics g = Graphics.FromImage(bm))
                    //    {
                    //        _ZGCPM1.GraphPane.AxisChange(g);
                    //    }
                    //    _ZGCPM1.GraphPane.GetImage().Save(sa.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //    pathfilePM1 = sa.FileName;
                    //}
                    //sa.Title = "Save PM10";
                    //if (sa.ShowDialog() == DialogResult.OK)
                    //{
                    //    Bitmap bm = new Bitmap(100, 100);
                    //    using (Graphics g = Graphics.FromImage(bm))
                    //    {
                    //        _ZGCPM10.GraphPane.AxisChange(g);
                    //    }
                    //    _ZGCPM10.GraphPane.GetImage().Save(sa.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //    pathfilePM10 = sa.FileName;
                    //}
                    //sa.Title = "Save TEMP";
                    //if (sa.ShowDialog() == DialogResult.OK)
                    //{
                    //    Bitmap bm = new Bitmap(100, 100);
                    //    using (Graphics g = Graphics.FromImage(bm))
                    //    {
                    //        _zgcTemp.GraphPane.AxisChange(g);
                    //    }
                    //    _zgcTemp.GraphPane.GetImage().Save(sa.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //    pathfileTEMP = sa.FileName;
                    //}
                    sa.Title = "Save PM2.5";
                    if (sa.ShowDialog() == DialogResult.OK)
                    {

                        var zedGraph = new ZedGraphControl();
                        zedGraph = _zedPM25;
                        using (var g = zedGraph.CreateGraphics())
                        {
                            zedGraph.MasterPane.ReSize(g, new RectangleF(0, 0, 700, 700));
                        }

                        zedGraph.MasterPane.GetImage().Save(sa.FileName);
                        pathfile = sa.FileName;
                       
                    }
                    else
                    {
                        return;
                    }
                    sa.Title = "Save PM1";
                    if (sa.ShowDialog() == DialogResult.OK)
                    {

                        var zedGraph = new ZedGraphControl();
                        zedGraph = _ZGCPM1;
                        using (var g = zedGraph.CreateGraphics())
                        {
                            zedGraph.MasterPane.ReSize(g, new RectangleF(0, 0, 700, 700));
                        }

                        zedGraph.MasterPane.GetImage().Save(sa.FileName);
                        pathfilePM1 = sa.FileName;
                    
                    }
                    else
                    {
                        return;
                    }
                    sa.Title = "Save PM10";
                    if (sa.ShowDialog() == DialogResult.OK)
                    {

                        var zedGraph = new ZedGraphControl();
                        zedGraph = _ZGCPM10;
                        using (var g = zedGraph.CreateGraphics())
                        {
                            zedGraph.MasterPane.ReSize(g, new RectangleF(0, 0, 700, 700));
                        }

                        zedGraph.MasterPane.GetImage().Save(sa.FileName);
                        pathfilePM10 = sa.FileName;
                   
                    }
                    else
                    {
                        return;
                    }
                    sa.Title = "Save TEMP";
                    if (sa.ShowDialog() == DialogResult.OK)
                    {

                        var zedGraph = new ZedGraphControl();
                        zedGraph = _zgcTemp;
                        using (var g = zedGraph.CreateGraphics())
                        {
                            zedGraph.MasterPane.ReSize(g, new RectangleF(0, 0, 700, 700));
                        }

                        zedGraph.MasterPane.GetImage().Save(sa.FileName);
                        pathfileTEMP = sa.FileName;
                  
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(pathfile))
                {
                    return;
                }
                if (string.IsNullOrEmpty(pathfilePM1))
                {
                    return;
                }
                if (string.IsNullOrEmpty(pathfilePM10))
                {
                    return;
                }
                if (string.IsNullOrEmpty(pathfileTEMP))
                {
                    return;
                }
                dt = DateTime.Now;
                line_notify("คนกดส่ง PM2.5 ", pathfile, dt);
                System.Threading.Thread.Sleep(1000);
                line_notify("คนกดส่ง PM1 ", pathfilePM1, dt);
                System.Threading.Thread.Sleep(1000);
                line_notify("คนกดส่ง PM10 ", pathfilePM10, dt);
                System.Threading.Thread.Sleep(1000);
                line_notify("คนกดส่ง TEMP ", pathfileTEMP, dt);
                System.Threading.Thread.Sleep(500);



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
            finally
            {
                _zedPM25.Size = new Size(291, 352);
                _ZGCPM1.Size = new Size(327, 352);
                _ZGCPM10.Size = new Size(352, 352);
                _zgcTemp.Size = new Size(383, 352);
                splashScreenManager1.CloseWaitForm();
            }
        }
        //static Image CaprtureScreen()
        //{
        //    var sz = Screen.PrimaryScreen.Bounds;
        //    var bmp = new Bitmap(sz.Width, sz.Height);
        //    using (var g = Graphics.FromImage(bmp))
        //    {
        //        g.CopyFromScreen(0, 0, 0, 0, sz.Size);
        //    }
        //    return bmp;
        //}
        //static void Dump(Line.Notify.Result rs)
        //{
        //    if (rs.Error != null)
        //        Console.WriteLine(rs.Error);
        //    Console.WriteLine($"       Message: {rs.Message}");
        //    Console.WriteLine($"        Status: {rs.Status}");
        //    Console.WriteLine($"         Limit: {rs.Limit}");
        //    Console.WriteLine($"     Remaining: {rs.Remaining}");
        //    Console.WriteLine($"    ImageLimit: {rs.ImageLimit}");
        //    Console.WriteLine($"ImageRemaining: {rs.ImageRemaining}");
        //    Console.WriteLine($"     ResetTime: {rs.ResetTime:O}");
        //}

        public async Task line_notify(string msg,string path,DateTime dts)
        {
 
            try
            {
               
                string msgtrue = msg +"วันที่"+dts.ToShortDateString()+"เวลา"+ dts.ToShortTimeString();
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://notify-api.line.me/api/notify"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer "+ token);

                        var multipartContent = new MultipartFormDataContent();
                        multipartContent.Add(new StringContent(msgtrue), "message");
                        multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(path)), "imageFile", Path.GetFileName(path));
                        request.Content = multipartContent;
                        var response = await httpClient.SendAsync(request);
                    }
                }
            }
            catch 
            {

                throw;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportExcellcs ex = new ExportExcellcs(dtExport);
            ex.ShowDialog();
        }
    }
    //public static class Line
    //{
    //    public static class Notify
    //    {
    //        private const string LineNotifyUrl = "https://notify-api.line.me/api/notify";

    //        public class Result
    //        {
    //            public Args Args { get; set; }
    //            public Exception Error { get; set; }
    //            public string Status { get; set; }
    //            public string Message { get; set; }
    //            public int Limit { get; set; }
    //            public int Remaining { get; set; }
    //            public int ImageLimit { get; set; }
    //            public int ImageRemaining { get; set; }
    //            public DateTimeOffset ResetTime { get; set; }
    //        }

    //        public class Args
    //        {
    //            public string Token { get; set; }
    //            public string Message { get; set; }
    //            public string ImageThumbnail { get; set; }
    //            public string ImageFullsize { get; set; }
    //            public Image Image { get; set; }
    //            public string StickerPackageId { get; set; }
    //            public string StickerId { get; set; }
    //            public bool? NotificationDisabled { get; set; }
    //        }

    //        public static async Task<Result> SendImage(string token, string message, Image image)
    //        {
    //            return await Send(new Args
    //            {
    //                Token = token,
    //                Message = message,
    //                Image = image
    //            });
    //        }
    //        public static async Task<Result> SendWebImage(string token, string message, string thumbnailUrl, string fullsizeUrl)
    //        {
    //            return await Send(new Args
    //            {
    //                Token = token,
    //                Message = message,
    //                ImageThumbnail = thumbnailUrl,
    //                ImageFullsize = fullsizeUrl
    //            });
    //        }
    //        public static async Task<Result> SendSticker(string token, string message, int package, int id)
    //        {
    //            return await Send(new Args
    //            {
    //                Token = token,
    //                Message = message,
    //                StickerPackageId = package.ToString(),
    //                StickerId = id.ToString()
    //            });
    //        }
    //        public static async Task<Result> SendMessage(string token, string message)
    //        {
    //            return await Send(new Args
    //            {
    //                Token = token,
    //                Message = message
    //            });
    //        }
    //        public static async Task<Result> Send(Args a)
    //        {
    //            using (var form = new MultipartFormDataContent())
    //            {
    //                void Addstr(string value, string name)
    //                {
    //                    if (string.IsNullOrEmpty(value)) return;
    //                    form.Add(new StringContent(value), name);
    //                }
    //                Addstr(a.Message, "message");
    //                Addstr(a.ImageThumbnail, "imageThumbnail");
    //                Addstr(a.ImageFullsize, "imageFullsize");
    //                Addstr(a.StickerPackageId, "stickerPackageId");
    //                Addstr(a.StickerId, "stickerId");
    //                if (a.NotificationDisabled.HasValue)
    //                    Addstr(a.NotificationDisabled.Value ? "true" : "false", "notificationDisabled");
    //                if (a.Image != null)
    //                {
    //                    using (var mem = new MemoryStream())
    //                    {
    //                        a.Image.Save(mem, ImageFormat.Png);
    //                        form.Add(new ByteArrayContent(mem.ToArray()), "imageFile", "image.png");
    //                    }
    //                }
                   
    //                var result = await Send(a.Token, form);
                   
    //                result.Args = a;
    //                return result;
    //            }
    //        }
    //        public static async Task<Result> Send(string token, HttpContent content)
    //        {
    //            var o = new Result();
    //            using (var http = new HttpClient())
    //            {
    //                try
    //                {
    //                    http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    //                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
    //                    var rs = await http.PostAsync(LineNotifyUrl, content);
    //                    rs.EnsureSuccessStatusCode();
    //                    var ctn = await rs.Content.ReadAsStringAsync();
    //                  //  var jss = new JavaScriptSerializer();
    //                  //  var d = jss.Deserialize<Dictionary<string, object>>(ctn);
    //                    //if (d.TryGetValue("message", out var message)) o.Message = message.ToString();
    //                    //if (d.TryGetValue("status", out var status)) o.Status = status.ToString();
    //                    int R(string name) => rs.Headers.TryGetValues(name, out var v) ? int.Parse(string.Join("", v)) : 0;
    //                    o.Limit = R("X-RateLimit-Limit");
    //                    o.Remaining = R("X-RateLimit-Remaining");
    //                    o.ImageLimit = R("X-RateLimit-ImageLimit");
    //                    o.ImageRemaining = R("X-RateLimit-ImageRemaining");
    //                    o.ResetTime = DateTimeOffset.FromUnixTimeSeconds(R("X-RateLimit-Reset")).ToLocalTime();
    //                }
    //                catch (Exception e)
    //                {
    //                    o.Error = e;
    //                }
    //                return o;
    //            }
    //        }
    //    }
    //}
}
