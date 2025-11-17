using NModbus;
using NModbus.Data;
using NModbus.Serial;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BurnIn_Temperature_simu
{
    public partial class BurnIn_Temperature_Simu : Form
    {
        private const byte SlaveId = 1;
        private SerialPort serialPort = new SerialPort();
        private IModbusSlaveNetwork slaveNetwork;
        private IModbusSlave slave;
        private CancellationTokenSource cancellationTokenSource;
        private Task listeningTask;
        private Random random = new Random();
        private System.Windows.Forms.Timer monitorTimer;
        private volatile bool isStopping = false;

        private const ushort StartAddress = 596;
        private ushort[] holdingRegisters = new ushort[11];
        private ushort[] lastDataStoreValues = new ushort[11];

        private readonly object registerLock = new object();
        private const int MaxChartPoints = 100;


        private Dictionary<string, System.Drawing.Color> originalSeriesColors = new Dictionary<string, System.Drawing.Color>();
        private Dictionary<string, bool> seriesVisibility = new Dictionary<string, bool>();
        // [NEW] 定義一個顏色陣列，用於繪製各個通道的折線
        private readonly System.Drawing.Color[] channelColors = new System.Drawing.Color[]
        {
            System.Drawing.Color.LightCoral,
            System.Drawing.Color.LightCoral,
            System.Drawing.Color.SkyBlue,
            System.Drawing.Color.MediumSeaGreen,
            System.Drawing.Color.Plum,
            System.Drawing.Color.Orange,
            System.Drawing.Color.LightSteelBlue,
            System.Drawing.Color.Gold,
            System.Drawing.Color.MediumAquamarine
        };

        public BurnIn_Temperature_Simu()
        {
            InitializeComponent();
            InitializeConfigurationControls();
            InitializeRegisters();
            InitializeChart();
            UpdateUIFromRegisters();

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string displayVersion = $"V{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            this.Text = "SINPOR - BurnIn Temperature Simulator_" + displayVersion + "_251023";
            simulationTimer.Tick += SimulationTimer_Tick;

            monitorTimer = new System.Windows.Forms.Timer();
            monitorTimer.Interval = 100;
            monitorTimer.Tick += MonitorTimer_Tick;

        }

        private void InitializeChart()
        {
            // 清除舊狀態
            originalSeriesColors.Clear();
            seriesVisibility.Clear();

            chartTempHistory.Series.Clear();
            chartTempHistory.ChartAreas.Clear();
            chartTempHistory.Legends.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartTempHistory.ChartAreas.Add(chartArea);

            // --- X 軸 (時間軸) 設定 ---
            chartArea.AxisX.Title = "時間";
            chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 10F);
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);

            // --- Y 軸 (溫度軸) 設定 ---
            chartArea.AxisY.Title = "溫度 (°C)";
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 10F);
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            chartArea.AxisY.LabelStyle.Format = "0.0";
            chartArea.AxisY.Interval = Double.NaN;

            chartTempHistory.Legends.Add(new Legend("DefaultLegend"));
            chartTempHistory.Legends["DefaultLegend"].Docking = Docking.Top;

            // 建立平均溫度 Series
            Series avgTempSeries = new Series("平均溫度")
            {
                ChartArea = "MainArea",
                ChartType = SeriesChartType.Spline,
                XValueType = ChartValueType.DateTime,
                BorderWidth = 3,
                Color = System.Drawing.Color.DodgerBlue
            };
            chartTempHistory.Series.Add(avgTempSeries);
            originalSeriesColors[avgTempSeries.Name] = avgTempSeries.Color;
            seriesVisibility[avgTempSeries.Name] = true;

            // 建立通道 Series (迴圈從 1 到 8，對應通道名稱)
            for (int i = 1; i <= 8; i++)
            {
                Series channelSeries = new Series($"CH{i}")
                {
                    ChartArea = "MainArea",
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime,
                    BorderWidth = 1,
                    // [修正] 使用 i-1 來對應 channelColors 陣列的 0-based 索引
                    Color = channelColors[i - 1]
                };
                chartTempHistory.Series.Add(channelSeries);
                originalSeriesColors[channelSeries.Name] = channelSeries.Color;
                seriesVisibility[channelSeries.Name] = true;
            }

            // 註冊事件
            chartTempHistory.MouseMove -= ChartTempHistory_MouseMove;
            chartTempHistory.MouseMove += ChartTempHistory_MouseMove;
            chartTempHistory.MouseClick -= ChartTempHistory_MouseClick;
            chartTempHistory.MouseClick += ChartTempHistory_MouseClick;
            chartTempHistory.CustomizeLegend -= Chart_CustomizeLegend;
            chartTempHistory.CustomizeLegend += Chart_CustomizeLegend;
        }
        // 修正 CS1061: Chart 沒有 ToolTip 屬性，改用 ToolTip 類別顯示提示
        private ToolTip chartToolTip = new ToolTip();
        private DataPoint lastHighlightedPoint = null;
        private const double ProximityThreshold = 30.0;

        // 處理滑鼠在圖表上移動的事件，以改變滑鼠樣式、顯示數值提示並突顯資料點
        private void ChartTempHistory_MouseMove(object sender, MouseEventArgs e)
        {
            // --- 步驟 1: 優先處理圖例點擊 ---
            var hitTestResult = chartTempHistory.HitTest(e.X, e.Y);
            if (hitTestResult.ChartElementType == ChartElementType.LegendItem)
            {
                // 如果上一個點還突顯著，就清除它
                if (lastHighlightedPoint != null)
                {
                    lastHighlightedPoint.MarkerStyle = MarkerStyle.None;
                    lastHighlightedPoint = null;
                    chartToolTip.Hide(chartTempHistory);
                }
                chartTempHistory.Cursor = Cursors.Hand;
                return;
            }

            // --- 步驟 2: 全域搜尋最近的資料點 ---
            DataPoint closestPoint = null;
            Series closestSeries = null;
            double minDistance = double.MaxValue;
            var chartArea = chartTempHistory.ChartAreas["MainArea"];

            foreach (var series in chartTempHistory.Series)
            {
                if (seriesVisibility.TryGetValue(series.Name, out bool isVisible) && isVisible)
                {
                    foreach (var point in series.Points)
                    {
                        double pixelX = chartArea.AxisX.ValueToPixelPosition(point.XValue);
                        double pixelY = chartArea.AxisY.ValueToPixelPosition(point.YValues[0]);
                        double distance = Math.Sqrt(Math.Pow(e.X - pixelX, 2) + Math.Pow(e.Y - pixelY, 2));

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestPoint = point;
                            closestSeries = series;
                        }
                    }
                }
            }

            // --- 步驟 3: 根據找到的點更新狀態，避免閃爍 ---

            // 情況一: 滑鼠移到了空白區域 (沒有任何點在範圍內)
            if (closestPoint == null || minDistance >= ProximityThreshold)
            {
                // 如果之前有突顯的點，則清除它並隱藏 ToolTip
                if (lastHighlightedPoint != null)
                {
                    lastHighlightedPoint.MarkerStyle = MarkerStyle.None;
                    lastHighlightedPoint = null;
                    chartToolTip.Hide(chartTempHistory);
                }
                chartTempHistory.Cursor = Cursors.Default;
                return;
            }

            // 情況二: 滑鼠還在上次的資料點附近，不做任何事以防止閃爍
            if (closestPoint == lastHighlightedPoint)
            {
                return;
            }

            // 情況三: 滑鼠移到了一個新的資料點上
            // 清除上一個點的突顯
            if (lastHighlightedPoint != null)
            {
                lastHighlightedPoint.MarkerStyle = MarkerStyle.None;
            }

            // 突顯這個新的點
            chartTempHistory.Cursor = Cursors.Cross;
            closestPoint.MarkerStyle = MarkerStyle.Circle;
            closestPoint.MarkerSize = 8;
            closestPoint.MarkerColor = System.Drawing.Color.Black;

            // 更新並顯示 ToolTip
            DateTime time = DateTime.FromOADate(closestPoint.XValue);
            double tempValue = closestPoint.YValues[0];
            string seriesName = closestSeries.Name;
            string tipText = $"{seriesName}\n時間: {time:HH:mm:ss}\n數值: {tempValue:F1} °C";
            chartToolTip.Show(tipText, chartTempHistory, e.Location.X + 10, e.Location.Y + 10, 2000);

            // 記錄下這個新的點為「上一個點」
            lastHighlightedPoint = closestPoint;
        }
        // 負責更新狀態
        private void ChartTempHistory_MouseClick(object sender, MouseEventArgs e)
        {
            var result = chartTempHistory.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                LegendItem legendItem = result.Object as LegendItem;
                if (legendItem != null)
                {
                    var seriesName = legendItem.SeriesName;
                    var selectedSeries = chartTempHistory.Series.FindByName(seriesName);
                    if (selectedSeries != null)
                    {
                        // 1. 切換儲存的狀態
                        seriesVisibility[seriesName] = !seriesVisibility[seriesName];

                        // 2. 根據新狀態，立即更新線條的顏色 (讓它消失或出現)
                        if (seriesVisibility[seriesName])
                        {
                            // 變為可見
                            selectedSeries.Color = originalSeriesColors[seriesName];
                        }
                        else
                        {
                            // 變為隱藏
                            selectedSeries.Color = System.Drawing.Color.Transparent;
                        }
                    }
                }
            }
        }
        // 每次圖表重繪時強制設定圖例的外觀
        private void Chart_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            // 遍歷所有即將被繪製的圖例項目
            foreach (var legendItem in e.LegendItems)
            {
                // 檢查我們的可見性狀態字典
                bool isVisible = seriesVisibility[legendItem.SeriesName];

                if (isVisible)
                {
                    // 如果狀態是 "可見"，確保它顯示原始顏色
                    legendItem.Color = originalSeriesColors[legendItem.SeriesName];
                    if (legendItem.Cells.Count > 1)
                    {
                        legendItem.Cells[1].ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    // 如果狀態是 "隱藏"，強制將圖例項目變為灰色
                    legendItem.Color = System.Drawing.Color.LightGray; // 符號變灰色
                    if (legendItem.Cells.Count > 1)
                    {
                        legendItem.Cells[1].ForeColor = System.Drawing.Color.LightGray; // 文字變灰色
                    }
                }
            }
        }
        private void UpdateChart()
        {
            DateTime currentTime = DateTime.Now;

            // 更新平均溫度 Series
            double currentAvgTemp = holdingRegisters[8] / 10.0;
            var avgSeries = chartTempHistory.Series["平均溫度"];
            avgSeries.Points.AddXY(currentTime, currentAvgTemp);

            if (avgSeries.Points.Count > MaxChartPoints)
            {
                avgSeries.Points.RemoveAt(0);
            }

            // 更新 8 個通道的 Series
            for (int i = 1; i <= 8; i++)
            {
                // [修正] 通道 i (1-8) 對應到 holdingRegisters 的索引 i-1 (0-7)
                double currentChannelTemp = holdingRegisters[i - 1] / 10.0;
                var channelSeries = chartTempHistory.Series[$"CH{i}"];
                channelSeries.Points.AddXY(currentTime, currentChannelTemp);

                if (channelSeries.Points.Count > MaxChartPoints)
                {
                    channelSeries.Points.RemoveAt(0);
                }
            }

            // X 軸滾動
            chartTempHistory.ChartAreas["MainArea"].AxisX.Minimum = avgSeries.Points[0].XValue;
            chartTempHistory.ChartAreas["MainArea"].AxisX.Maximum = currentTime.ToOADate();

            // 自動計算 Y 軸最佳顯示範圍
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            bool hasVisiblePoints = false;

            foreach (var series in chartTempHistory.Series)
            {
                if (seriesVisibility[series.Name])
                {
                    foreach (var point in series.Points)
                    {
                        if (point.YValues[0] < yMin) yMin = point.YValues[0];
                        if (point.YValues[0] > yMax) yMax = point.YValues[0];
                        hasVisiblePoints = true;
                    }
                }
            }

            if (hasVisiblePoints)
            {
                double padding = 2.0;
                chartTempHistory.ChartAreas["MainArea"].AxisY.Minimum = Math.Floor(yMin - padding);
                chartTempHistory.ChartAreas["MainArea"].AxisY.Maximum = Math.Ceiling(yMax + padding);
            }
        }


        private void InitializeRegisters()
        {
            holdingRegisters[0] = 400;  // CH1 (Address 40597)
            holdingRegisters[1] = 410;  // CH2 (Address 40598)
            holdingRegisters[2] = 420;  // CH3 (Address 40599)
            holdingRegisters[3] = 430;  // CH4 (Address 40600)
            holdingRegisters[4] = 440;  // CH5 (Address 40601)
            holdingRegisters[5] = 450;  // CH6 (Address 40602)
            holdingRegisters[6] = 460;  // CH7 (Address 40603)
            holdingRegisters[7] = 470;  // CH8 (Address 40604)
            holdingRegisters[9] = 450;  // Temp Set (Address 40606)
            holdingRegisters[10] = 50;  // Temp Range (Address 40607)
            CalculateAverage();
            Array.Copy(holdingRegisters, lastDataStoreValues, holdingRegisters.Length);
        }

        private void UpdateUIFromRegisters()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateUIFromRegisters));
                return;
            }

            lock (registerLock)
            {
                txtCh0.Text = (holdingRegisters[0] / 10.0).ToString("F1");
                txtCh1.Text = (holdingRegisters[1] / 10.0).ToString("F1");
                txtCh2.Text = (holdingRegisters[2] / 10.0).ToString("F1");
                txtCh3.Text = (holdingRegisters[3] / 10.0).ToString("F1");
                txtCh4.Text = (holdingRegisters[4] / 10.0).ToString("F1");
                txtCh5.Text = (holdingRegisters[5] / 10.0).ToString("F1");
                txtCh6.Text = (holdingRegisters[6] / 10.0).ToString("F1");
                txtCh7.Text = (holdingRegisters[7] / 10.0).ToString("F1");
                txtAvgTemp.Text = (holdingRegisters[8] / 10.0).ToString("F1");
                txtTempSet.Text = (holdingRegisters[9] / 10.0).ToString("F1");
                txtTempRange.Text = (holdingRegisters[10] / 10.0).ToString("F1");
            }
        }

        private void InitializeConfigurationControls()
        {
            cmbComPort.Items.Clear();
            cmbComPort.Items.AddRange(SerialPort.GetPortNames());
            if (cmbComPort.Items.Count > 0) cmbComPort.SelectedIndex = 0;

            cmbBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cmbBaudRate.SelectedItem = "19200";

            cmbDataBits.Items.AddRange(new object[] { "7", "8" });
            cmbDataBits.SelectedItem = "8";

            cmbParity.DataSource = Enum.GetValues(typeof(Parity));
            cmbParity.SelectedItem = Parity.None;

            cmbStopBits.DataSource = Enum.GetValues(typeof(StopBits));
            cmbStopBits.SelectedItem = StopBits.One;

            txtTimeout.Text = "1000";
            txtUpdateInterval.Text = "1000";
        }

        private void SetConfigurationControlsEnabled(bool enabled)
        {
            cmbComPort.Enabled = enabled;
            cmbBaudRate.Enabled = enabled;
            cmbDataBits.Enabled = enabled;
            cmbParity.Enabled = enabled;
            cmbStopBits.Enabled = enabled;
            txtTimeout.Enabled = enabled;
            txtUpdateInterval.Enabled = enabled;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                logUpdateCounter = 0;
                isStopping = false;
                serialPort.PortName = cmbComPort.SelectedItem.ToString();
                serialPort.BaudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                serialPort.DataBits = int.Parse(cmbDataBits.SelectedItem.ToString());
                serialPort.Parity = (Parity)cmbParity.SelectedItem;
                serialPort.StopBits = (StopBits)cmbStopBits.SelectedItem;
                serialPort.ReadTimeout = int.Parse(txtTimeout.Text);

                if (!int.TryParse(txtUpdateInterval.Text, out int updateInterval) || updateInterval <= 0)
                {
                    MessageBox.Show("更新頻率必須是一個有效的正整數。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SetConfigurationControlsEnabled(false);

                serialPort.Open();

                var factory = new ModbusFactory();
                var dataStore = new SlaveDataStore();
                slave = factory.CreateSlave(SlaveId, dataStore);
                slave.DataStore.HoldingRegisters.WritePoints(StartAddress, holdingRegisters);

                slaveNetwork = factory.CreateRtuSlaveNetwork(serialPort);
                slaveNetwork.AddSlave(slave);

                cancellationTokenSource = new CancellationTokenSource();
                listeningTask = Task.Run(() => ListenAsync(cancellationTokenSource.Token));

                simulationTimer.Interval = updateInterval;
                simulationTimer.Start();
                monitorTimer.Start();

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                lblStatus.Text = $"狀態：正在監聽 {serialPort.PortName}...";
                Log($"Slave 已啟動，監聽 {serialPort.PortName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("啟動失敗: " + ex.Message + "\n\n" + ex.StackTrace);
                if (serialPort.IsOpen) serialPort.Close();
                SetConfigurationControlsEnabled(true);
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            isStopping = true;
            btnStop.Enabled = false;
            btnStart.Enabled = false;
            lblStatus.Text = "狀態：正在停止...";
            Log("正在停止 Slave...");

            simulationTimer.Stop();
            monitorTimer.Stop();
            cancellationTokenSource?.Cancel();

            try
            {
                if (listeningTask != null)
                {
                    await listeningTask;
                }
            }
            catch (Exception ex)
            {
                Log($"等待監聽任務結束時發生錯誤: {ex.Message}");
            }
            finally
            {
                slaveNetwork?.Dispose();
                slaveNetwork = null;

                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                slave = null;
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
                listeningTask = null;

                btnStart.Enabled = true;
                lblStatus.Text = "狀態：停止";
                Log("Slave 已成功停止");
                SetConfigurationControlsEnabled(true);
            }
        }

        private void MonitorTimer_Tick(object sender, EventArgs e)
        {
            if (isStopping) return;
            if (slave == null) return;

            try
            {
                lock (registerLock)
                {
                    var currentValues = slave.DataStore.HoldingRegisters.ReadPoints(StartAddress, (ushort)holdingRegisters.Length);
                    bool hasChanged = false;

                    for (int i = 0; i < holdingRegisters.Length; i++)
                    {
                        if (currentValues[i] != lastDataStoreValues[i])
                        {
                            hasChanged = true;
                            Log($"Master 寫入: 地址={40001 + StartAddress + i}, 舊值={lastDataStoreValues[i]}, 新值={currentValues[i]}");
                            holdingRegisters[i] = currentValues[i];
                            lastDataStoreValues[i] = currentValues[i];
                        }
                    }

                    if (hasChanged)
                    {
                        CalculateAverage();
                        slave.DataStore.HoldingRegisters.WritePoints((ushort)(StartAddress + 8), new ushort[] { holdingRegisters[8] });
                        lastDataStoreValues[8] = holdingRegisters[8];
                        UpdateUIFromRegisters();
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"監控錯誤: {ex.Message}");
            }
        }

        private async Task ListenAsync(CancellationToken cancellationToken)
        {
            try
            {
                await slaveNetwork.ListenAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Log("監聽任務已成功取消。");
            }
            catch (TimeoutException)
            {
                Log("監聽因讀取逾時而停止，這是正常現象。");
            }
            catch (Exception ex)
            {
                if (!isStopping)
                {
                    Log($"監聽錯誤: {ex.Message}");
                }
            }
        }
        private long logUpdateCounter = 0;
        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            if (isStopping) return;
            if (slave == null) return;

            logUpdateCounter++;
            ushort[] newValuesForLog;

            lock (registerLock)
            {
                int baseTemp = holdingRegisters[9];
                int tempRange = holdingRegisters[10];

                if (tempRange < 0) tempRange = 0;

                // 迴圈 0 到 7，更新 8 個通道的資料
                for (int i = 0; i <= 7; i++)
                {
                    int change = random.Next(-tempRange, tempRange + 1);
                    int newValue = baseTemp + change;

                    if (newValue < 0) newValue = 0;
                    if (newValue > 1000) newValue = 1000;
                    holdingRegisters[i] = (ushort)newValue;
                }

                CalculateAverage();

                newValuesForLog = (ushort[])holdingRegisters.Clone();

                slave.DataStore.HoldingRegisters.WritePoints(StartAddress, holdingRegisters);
                Array.Copy(holdingRegisters, lastDataStoreValues, holdingRegisters.Length);
            }

            UpdateUIFromRegisters();

            // 建立日誌訊息，正確顯示通道 1-8
            var logMessage = new StringBuilder();
            logMessage.Append("溫度值更新#" + logUpdateCounter + ": ");
            // 使用 0-7 索引，但顯示為通道 1-8
            for (int i = 0; i < 8; i++)
            {
                logMessage.Append($"通道{i + 1}={(newValuesForLog[i] / 10.0):F1}, ");
            }
            logMessage.Append($"平均溫度={(newValuesForLog[8] / 10.0):F1}");

            Log(logMessage.ToString());
            UpdateChart();
        }

        private void CalculateAverage()
        {
            long sum = 0;
            for (int i = 0; i <= 7; i++)
            {
                sum += holdingRegisters[i];
            }
            holdingRegisters[8] = (ushort)(sum / 8);
        }

        private void Log(string message)
        {
            // 確保在 UI 執行緒上執行
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action<string>(Log), message);
                return;
            }

            try
            {
                // 暫停 UI 更新，防止閃爍
                lstLog.BeginUpdate();

                // [最佳化] 改為從列表尾部新增，效能更好
                lstLog.Items.Add($"{DateTime.Now:HH:mm:ss.fff} - {message}");

                // 自動捲動到最新的項目
                if (lstLog.Items.Count > 0)
                {
                    lstLog.TopIndex = lstLog.Items.Count - 1;
                }

                // [最佳化] 當超過數量時，從列表頂部 (最舊的) 刪除
                if (lstLog.Items.Count > 100)
                {
                    lstLog.Items.RemoveAt(0);
                }
            }
            finally
            {
                // 恢復 UI 更新
                lstLog.EndUpdate();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStop.Enabled)
            {
                btnStop_Click(null, null);
            }
        }

        private void cmbComPort_DropDown(object sender, EventArgs e)
        {
            // 清空原有的項目
            cmbComPort.Items.Clear();

            // 取得目前可用的 COM 埠
            string[] ports = SerialPort.GetPortNames();

            // 排序（可選）
            Array.Sort(ports);

            // 加入到下拉選單
            cmbComPort.Items.AddRange(ports);

        }
    }
}