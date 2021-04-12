namespace arduinoBank
{
    partial class graphcs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this._btnTemp = new System.Windows.Forms.Button();
            this._btnPM10 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this._btngraph = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.graphPM1cs1 = new arduinoBank.GraphPM1cs();
            this.graphreal1 = new arduinoBank.graphreal();
            this.graphPM101 = new arduinoBank.graphPM10();
            this.graphTemp1 = new arduinoBank.graphTemp();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._btnTemp);
            this.panel1.Controls.Add(this._btnPM10);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this._btngraph);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 73);
            this.panel1.TabIndex = 0;
            // 
            // _btnTemp
            // 
            this._btnTemp.BackColor = System.Drawing.Color.Aqua;
            this._btnTemp.Dock = System.Windows.Forms.DockStyle.Left;
            this._btnTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._btnTemp.Location = new System.Drawing.Point(664, 0);
            this._btnTemp.Name = "_btnTemp";
            this._btnTemp.Size = new System.Drawing.Size(197, 73);
            this._btnTemp.TabIndex = 7;
            this._btnTemp.Text = "TEMP";
            this._btnTemp.UseVisualStyleBackColor = false;
            this._btnTemp.Click += new System.EventHandler(this._btnTemp_Click);
            // 
            // _btnPM10
            // 
            this._btnPM10.BackColor = System.Drawing.Color.Aqua;
            this._btnPM10.Dock = System.Windows.Forms.DockStyle.Left;
            this._btnPM10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._btnPM10.Location = new System.Drawing.Point(452, 0);
            this._btnPM10.Name = "_btnPM10";
            this._btnPM10.Size = new System.Drawing.Size(212, 73);
            this._btnPM10.TabIndex = 6;
            this._btnPM10.Text = "PM10";
            this._btnPM10.UseVisualStyleBackColor = false;
            this._btnPM10.Click += new System.EventHandler(this._btnPM10_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aqua;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(226, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(226, 73);
            this.button1.TabIndex = 5;
            this.button1.Text = "PM1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _btngraph
            // 
            this._btngraph.BackColor = System.Drawing.Color.Aqua;
            this._btngraph.Dock = System.Windows.Forms.DockStyle.Left;
            this._btngraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._btngraph.Location = new System.Drawing.Point(0, 0);
            this._btngraph.Name = "_btngraph";
            this._btngraph.Size = new System.Drawing.Size(226, 73);
            this._btngraph.TabIndex = 4;
            this._btngraph.Text = "PM2.5";
            this._btngraph.UseVisualStyleBackColor = false;
            this._btngraph.Click += new System.EventHandler(this._btngraph_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.graphPM1cs1);
            this.panel2.Controls.Add(this.graphreal1);
            this.panel2.Controls.Add(this.graphPM101);
            this.panel2.Controls.Add(this.graphTemp1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1320, 644);
            this.panel2.TabIndex = 1;
            // 
            // graphPM1cs1
            // 
            this.graphPM1cs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPM1cs1.dts = null;
            this.graphPM1cs1.Location = new System.Drawing.Point(0, 0);
            this.graphPM1cs1.Name = "graphPM1cs1";
            this.graphPM1cs1.Size = new System.Drawing.Size(1320, 644);
            this.graphPM1cs1.TabIndex = 1;
            // 
            // graphreal1
            // 
            this.graphreal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphreal1.dts = null;
            this.graphreal1.Location = new System.Drawing.Point(0, 0);
            this.graphreal1.Name = "graphreal1";
            this.graphreal1.Size = new System.Drawing.Size(1320, 644);
            this.graphreal1.TabIndex = 0;
            // 
            // graphPM101
            // 
            this.graphPM101.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPM101.dts = null;
            this.graphPM101.Location = new System.Drawing.Point(0, 0);
            this.graphPM101.Name = "graphPM101";
            this.graphPM101.Size = new System.Drawing.Size(1320, 644);
            this.graphPM101.TabIndex = 2;
            // 
            // graphTemp1
            // 
            this.graphTemp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTemp1.dts = null;
            this.graphTemp1.Location = new System.Drawing.Point(0, 0);
            this.graphTemp1.Name = "graphTemp1";
            this.graphTemp1.Size = new System.Drawing.Size(1320, 644);
            this.graphTemp1.TabIndex = 3;
            // 
            // graphcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 717);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "graphcs";
            this.Text = "graphcs";
            this.Load += new System.EventHandler(this.graphcs_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private graphreal graphreal1;
        private System.Windows.Forms.Button _btngraph;
        private GraphPM1cs graphPM1cs1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button _btnPM10;
        private System.Windows.Forms.Button _btnTemp;
        private graphPM10 graphPM101;
        private graphTemp graphTemp1;
    }
}