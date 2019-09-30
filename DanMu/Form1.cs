using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanMu
{
    public partial class Form1 : Form
    {
        List<Label> TanMuList = new List<Label>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rec = Screen.GetWorkingArea(this);
            this.Height = rec.Height / 3 * 2; // 3分之2
            this.Width = rec.Width;
            this.Location = new Point(0, 0);
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.Opacity = 1;

            Thread th1 = new Thread(new ThreadStart(aaaa));
            th1.Start();

            AddTanMu("你开启了弹幕");
        }

        //测试
        private void aaaa()
        {
            while (true)
            {
                AddTanMu(Guid.NewGuid().ToString());
                Thread.Sleep(new Random().Next(1000, 3000));
            }
        }

        private void AddTanMu(String word)
        {
            Label label = new Label();
            label.Text = word;
            label.AutoSize = true;
            label.ForeColor = Color.Red;
            label.BackColor = Color.Transparent;
            label.Font = new Font("宋体", 20);
            label.Location = new Point(this.Width, new Random().Next(this.Height - label.Height));

            TanMuList.Add(label);

            this.Invoke(new Action(() =>
            {
                this.Controls.Add(label);
            }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //只能使用for循环
            for (int i = 0; i < TanMuList.Count; i++)
            {
                Label label = TanMuList[i];

                Point point = label.Location;

                //步数、速度
                for (int v = 0; v < 7; v++)
                {
                    point.X = point.X - 1;
                    label.Location = point;
                }

                //超出移除
                if (point.X + label.Width < 0)
                {
                    label.Visible = false;
                    TanMuList.Remove(label);
                    this.Controls.Remove(label);
                }
            }
        }

    }
}
