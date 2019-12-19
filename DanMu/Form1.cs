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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle rec = Screen.GetWorkingArea(this);
            this.Height = rec.Height / 3 * 2; // 3分之2
            this.Width = rec.Width;
            this.Location = new Point(0, 0);
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.Opacity = 1;

            AddTanMu("你开启了弹幕");

            //测试运行
            Thread th1 = new Thread(new ThreadStart(test));
            th1.Start();
        }

        private void test()
        {
            while (true)
            {
                AddTanMu(Guid.NewGuid().ToString());
                Thread.Sleep(new Random().Next(2000, 3500));
            }
        }

        public void AddTanMu(String word)
        {
            Label label = new Label();
            label.Text = word;
            label.AutoSize = true;
            label.ForeColor = Color.Red;
            label.BackColor = Color.Transparent;
            label.Font = new Font("宋体", 20);
            label.Location = new Point(this.Width, new Random().Next(this.Height - label.Height));

            this.Invoke(new Action(() =>
            {
                this.Controls.Add(label);
            }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //方法1：
            //foreach (Label label in this.Controls)
            //{
            //    Task.Run(() =>
            //    {
            //        //单独执行的委托，防止卡顿
            //        label.Invoke(new Action(() =>
            //        {
            //            //步数、速度
            //            for (int v = 0; v < 3; v++)
            //            {
            //                label.Left -= 2;
            //            }
            //        }));

            //        //超出移除
            //        if (label.Left + label.Width < 0)
            //        {
            //            this.Invoke(new Action(() =>
            //            {
            //                this.Controls.Remove(label);
            //                label.Dispose();

            //            }));
            //        }
            //    });
            //}

            //方法2：
            foreach (Label label in this.Controls)
            {
                label.Invoke(new Action(() =>
                {
                    //步数、速度
                    //【3（次数）*2（步数）=6（速度）】
                    for (int v = 0; v < 3; v++)
                    {
                        label.Left -= 2;
                    }
                }));

                //超出移除
                if (label.Left + label.Width < 0)
                {
                    label.Visible = false;
                    this.Controls.Remove(label);
                }
            }
        }

    }
}
