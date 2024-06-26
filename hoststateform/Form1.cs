using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace hoststateform
{
    public partial class Form1 : Form, ISystemHostStateActionHandler
    {
        private int data_size = 200;
        private const int chunk_size = 50;
        private const int time_interval = 500;
        private int win_sz = 40;
        private List<double> data = new List<double>();  // 数据源
        private List<double> pressure = new List<double>();
        private List<double> speed = new List<double>();
        private double timeSource = 0.0;
        private Random rnd = new Random();
        private int currState = 0;
        private SystemHostState shs = null;
        private bool updating = true;
        private double _value, _newP, _newRPM;

        public Form1()
        {
            InitializeComponent();

            // 初始化Chart控件
            chart1.Titles.Add("Real-time Chart Demo");
            chart1.ChartAreas[0].AxisX.Title = "Time";
            chart1.ChartAreas[0].AxisY.Title = "Value";
            chart1.Series.Add("Data");
            chart1.Series["Data"].ChartType = SeriesChartType.Line;
            ChartArea CA = chart1.ChartAreas[0];
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
            shs = new SystemHostState(this);
            shs.VSInitAll();
            shs.VSDeduct(SystemHostState.SE_RESET);
        }

        public void appInit()
        {
            Console.WriteLine("AppInit()");
            // 启动定时器
            timer1.Interval = time_interval;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        public void pauseHost()
        {
            Console.WriteLine("PauseHost()");
            button5.Text = "继续";
            updating = false;
        }

        public void restoreHost()
        {
            Console.WriteLine("RestoreHost()");
            button5.Text = "暂停";
            updating = true;
        }

        public byte startHost()
        {
            Console.WriteLine("StartHost()");
            timer1.Start();
            button5.Text = "暂停";
            updating = true;
            confModBus.Enabled = false;
            confCAN.Enabled = false;
            confSerial.Enabled = false;
            confLog.Enabled = false;
            return 0; 
        }

        public void stopHost()
        {
            Console.WriteLine("StopHost()");
            timer1.Stop();
            updating = false;
            button5.Text = "开始";
            confModBus.Enabled = true;
            confCAN.Enabled = true;
            confSerial.Enabled = true;
            confLog.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double value = 0.0;
            double newP = 0.0;
            double newRPM = 0.0;

            for (int i = 0; i < chunk_size; i ++) {
                timeSource += 0.1;
                // 随机生成一个数据
                if (updating == true)
                {
                    value = Math.Sin(timeSource) * 100;
                    newP = 3000.0 + rnd.Next(-50, 50);
                    newRPM = Math.Cos(timeSource * 5) * 3000 + 4000;
                }
                else
                {
                    value = _value;
                    newP = _newP;
                    newRPM = _newRPM;
                }

                // 添加到数据源中
                data.Add(value);
                pressure.Add(newP);
                speed.Add(newRPM);

                // 如果数据源超过了一定长度，就删除最前面的数据
                while (data.Count > data_size)
                {
                    data.RemoveAt(0);
                }
                while (pressure.Count > data_size)
                {
                    pressure.RemoveAt(0);
                }
                while (speed.Count > data_size)
                {
                    speed.RemoveAt(0);
                }
            }
            _value = value;
            _newP = newP;
            _newRPM = newRPM;
            // 绑定数据并刷新Chart控件
            if (radioButton1.Checked)
            {
                chart1.Series["Data"].Points.DataBindY(data);
            } else if (radioButton2.Checked)
            {
                chart1.Series["Data"].Points.DataBindY(pressure);
            } else
            {
                chart1.Series["Data"].Points.DataBindY(speed);
            }
            chart1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("串口参数详细配置", "配置页面", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "暂停")
            {
                shs.VSDeduct(SystemHostState.clickPause);
            }
            else if (button5.Text == "继续")
            {
                shs.VSDeduct(SystemHostState.clickRestore);
            }
            else
            {
                shs.VSDeduct(SystemHostState.clickStart);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            shs.VSDeduct(SystemHostState.clickStop);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("RadioButton1 checked!");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void confModBus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ModBus参数详细配置", "配置页面", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void confCAN_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CAN参数详细配置", "配置页面", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void confLog_Click(object sender, EventArgs e)
        {
            MessageBox.Show("上位机日志参数详细配置", "配置页面", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (data_size >= 20) {
                data_size /= 2;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (data_size <= 400)
            {
                data_size *= 2;
            }
        }
    }
}
