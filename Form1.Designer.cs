namespace BurnIn_Temperature_simu
{
    partial class BurnIn_Temperature_Simu
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BurnIn_Temperature_Simu));
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.txtCh0 = new System.Windows.Forms.TextBox();
            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.txtCh2 = new System.Windows.Forms.TextBox();
            this.txtCh3 = new System.Windows.Forms.TextBox();
            this.txtCh4 = new System.Windows.Forms.TextBox();
            this.txtCh5 = new System.Windows.Forms.TextBox();
            this.txtCh6 = new System.Windows.Forms.TextBox();
            this.txtCh7 = new System.Windows.Forms.TextBox();
            this.txtCh1 = new System.Windows.Forms.TextBox();
            this.txtAvgTemp = new System.Windows.Forms.TextBox();
            this.txtTempSet = new System.Windows.Forms.TextBox();
            this.txtTempRange = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTimeout = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtUpdateInterval = new System.Windows.Forms.TextBox();
            this.chartTempHistory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartTempHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbComPort
            // 
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(8, 64);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(92, 20);
            this.cmbComPort.TabIndex = 0;
            this.cmbComPort.DropDown += new System.EventHandler(this.cmbComPort_DropDown);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("新細明體", 10F);
            this.btnStart.Location = new System.Drawing.Point(551, 49);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(58, 37);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "開啟";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 12);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "狀態";
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(10, 150);
            this.lstLog.Name = "lstLog";
            this.lstLog.ScrollAlwaysVisible = true;
            this.lstLog.Size = new System.Drawing.Size(785, 316);
            this.lstLog.TabIndex = 3;
            // 
            // txtCh0
            // 
            this.txtCh0.Location = new System.Drawing.Point(8, 113);
            this.txtCh0.Name = "txtCh0";
            this.txtCh0.ReadOnly = true;
            this.txtCh0.Size = new System.Drawing.Size(46, 22);
            this.txtCh0.TabIndex = 4;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("新細明體", 10F);
            this.btnStop.Location = new System.Drawing.Point(615, 49);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 37);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "關閉";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtCh2
            // 
            this.txtCh2.Location = new System.Drawing.Point(112, 113);
            this.txtCh2.Name = "txtCh2";
            this.txtCh2.ReadOnly = true;
            this.txtCh2.Size = new System.Drawing.Size(46, 22);
            this.txtCh2.TabIndex = 4;
            // 
            // txtCh3
            // 
            this.txtCh3.Location = new System.Drawing.Point(164, 113);
            this.txtCh3.Name = "txtCh3";
            this.txtCh3.ReadOnly = true;
            this.txtCh3.Size = new System.Drawing.Size(46, 22);
            this.txtCh3.TabIndex = 4;
            // 
            // txtCh4
            // 
            this.txtCh4.Location = new System.Drawing.Point(216, 113);
            this.txtCh4.Name = "txtCh4";
            this.txtCh4.ReadOnly = true;
            this.txtCh4.Size = new System.Drawing.Size(46, 22);
            this.txtCh4.TabIndex = 4;
            // 
            // txtCh5
            // 
            this.txtCh5.Location = new System.Drawing.Point(268, 113);
            this.txtCh5.Name = "txtCh5";
            this.txtCh5.ReadOnly = true;
            this.txtCh5.Size = new System.Drawing.Size(46, 22);
            this.txtCh5.TabIndex = 4;
            // 
            // txtCh6
            // 
            this.txtCh6.Location = new System.Drawing.Point(320, 113);
            this.txtCh6.Name = "txtCh6";
            this.txtCh6.ReadOnly = true;
            this.txtCh6.Size = new System.Drawing.Size(46, 22);
            this.txtCh6.TabIndex = 4;
            // 
            // txtCh7
            // 
            this.txtCh7.Location = new System.Drawing.Point(372, 113);
            this.txtCh7.Name = "txtCh7";
            this.txtCh7.ReadOnly = true;
            this.txtCh7.Size = new System.Drawing.Size(46, 22);
            this.txtCh7.TabIndex = 4;
            // 
            // txtCh1
            // 
            this.txtCh1.Location = new System.Drawing.Point(60, 113);
            this.txtCh1.Name = "txtCh1";
            this.txtCh1.ReadOnly = true;
            this.txtCh1.Size = new System.Drawing.Size(46, 22);
            this.txtCh1.TabIndex = 4;
            // 
            // txtAvgTemp
            // 
            this.txtAvgTemp.Location = new System.Drawing.Point(424, 113);
            this.txtAvgTemp.Name = "txtAvgTemp";
            this.txtAvgTemp.ReadOnly = true;
            this.txtAvgTemp.Size = new System.Drawing.Size(61, 22);
            this.txtAvgTemp.TabIndex = 4;
            // 
            // txtTempSet
            // 
            this.txtTempSet.Location = new System.Drawing.Point(491, 113);
            this.txtTempSet.Name = "txtTempSet";
            this.txtTempSet.ReadOnly = true;
            this.txtTempSet.Size = new System.Drawing.Size(61, 22);
            this.txtTempSet.TabIndex = 4;
            // 
            // txtTempRange
            // 
            this.txtTempRange.Location = new System.Drawing.Point(558, 113);
            this.txtTempRange.Name = "txtTempRange";
            this.txtTempRange.ReadOnly = true;
            this.txtTempRange.Size = new System.Drawing.Size(61, 22);
            this.txtTempRange.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "通道1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "通道2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "通道3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "通道4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "通道5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "通道6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "通道7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(372, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "通道8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(424, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "平均溫度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(491, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "基準溫度";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(558, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "正負範圍";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "ComPort:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(104, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "Baud Rate:";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(106, 64);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(82, 20);
            this.cmbBaudRate.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(192, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 12);
            this.label14.TabIndex = 11;
            this.label14.Text = "Data Bits:";
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Location = new System.Drawing.Point(194, 64);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(48, 20);
            this.cmbDataBits.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(246, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "Parity:";
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(248, 64);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(60, 20);
            this.cmbParity.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(312, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 12);
            this.label16.TabIndex = 15;
            this.label16.Text = "Stop Bits:";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(314, 64);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(60, 20);
            this.cmbStopBits.TabIndex = 14;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(376, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 12);
            this.label17.TabIndex = 17;
            this.label17.Text = "Timeout (ms):";
            // 
            // txtTimeout
            // 
            this.txtTimeout.FormattingEnabled = true;
            this.txtTimeout.Location = new System.Drawing.Point(378, 64);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(69, 20);
            this.txtTimeout.TabIndex = 16;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(453, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 12);
            this.label18.TabIndex = 19;
            this.label18.Text = "更新頻率 (ms):";
            // 
            // txtUpdateInterval
            // 
            this.txtUpdateInterval.Location = new System.Drawing.Point(453, 62);
            this.txtUpdateInterval.Name = "txtUpdateInterval";
            this.txtUpdateInterval.Size = new System.Drawing.Size(92, 22);
            this.txtUpdateInterval.TabIndex = 20;
            // 
            // chartTempHistory
            // 
            this.chartTempHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartTempHistory.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTempHistory.Legends.Add(legend1);
            this.chartTempHistory.Location = new System.Drawing.Point(10, 472);
            this.chartTempHistory.Name = "chartTempHistory";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTempHistory.Series.Add(series1);
            this.chartTempHistory.Size = new System.Drawing.Size(784, 165);
            this.chartTempHistory.TabIndex = 21;
            this.chartTempHistory.Text = "chart1";
            // 
            // BurnIn_Temperature_Simu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 645);
            this.Controls.Add(this.chartTempHistory);
            this.Controls.Add(this.txtUpdateInterval);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbStopBits);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbParity);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbDataBits);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtTempRange);
            this.Controls.Add(this.txtTempSet);
            this.Controls.Add(this.txtAvgTemp);
            this.Controls.Add(this.txtCh1);
            this.Controls.Add(this.txtCh7);
            this.Controls.Add(this.txtCh6);
            this.Controls.Add(this.txtCh5);
            this.Controls.Add(this.txtCh3);
            this.Controls.Add(this.txtCh4);
            this.Controls.Add(this.txtCh2);
            this.Controls.Add(this.txtCh0);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbComPort);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1900, 684);
            this.MinimumSize = new System.Drawing.Size(820, 684);
            this.Name = "BurnIn_Temperature_Simu";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chartTempHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.TextBox txtCh0;
        private System.Windows.Forms.Timer simulationTimer;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtCh2;
        private System.Windows.Forms.TextBox txtCh3;
        private System.Windows.Forms.TextBox txtCh4;
        private System.Windows.Forms.TextBox txtCh5;
        private System.Windows.Forms.TextBox txtCh6;
        private System.Windows.Forms.TextBox txtCh7;
        private System.Windows.Forms.TextBox txtCh1;
        private System.Windows.Forms.TextBox txtAvgTemp;
        private System.Windows.Forms.TextBox txtTempSet;
        private System.Windows.Forms.TextBox txtTempRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox txtTimeout;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUpdateInterval;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTempHistory;
    }
}

