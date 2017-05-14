using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clocktester
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        int width = 300, height = 300, secondHand = 140, minuteHand = 110, hourHand = 80;

        int centerx, centery;
        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void t_tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);

            int second = DateTime.Now.Second;
            int minute = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            int[] handcoord = new int[2];

            g.Clear(Color.White);

            g.DrawRectangle(new Pen(Color.Black, 1f), 0, 0, width, height);

            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            handcoord = minsecCoord(second, secondHand);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(centerx, centery), new Point(handcoord[0], handcoord[1]));

            handcoord = minsecCoord(minute, minuteHand);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(centerx, centery), new Point(handcoord[0], handcoord[1]));

            handcoord = hourCoord(hour, minute, hourHand);
            g.DrawLine(new Pen(Color.Black, 3f), new Point(centerx, centery), new Point(handcoord[0], handcoord[1]));

            pictureBox1.Image = bmp;

            if (minute < 10)
            {
                this.Text = "Current Time  -  " + hour + ":" + "0" + minute + ":" + second;
            }
            if (second < 10)
            {
                this.Text = "Current Time  -  " + hour + ":" + minute + ":" + "0" + second;
            }
            if (minute < 10 && second < 10)
            {
                this.Text = "Current Time  -  " + hour + ":" + "0" + minute + ":" + "0" + second;
            }
            else
            {
                this.Text = "Current Time  -  " + hour + ":" + minute + ":" + second;
            }
        }
        private int[] minsecCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;
            if (val >= 0 && val <= 180)
            {
                coord[0] = centerx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = centery - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = centerx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = centery - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] hourCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = centerx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = centery - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = centerx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = centery - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(width + 1, height + 1);

            centerx = width / 2;
            centery = height / 2;

            this.BackColor = Color.White;

            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_tick);
            t.Start();
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label1.Visible = false;
            button4.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Height -= 50;
            this.Width -= 50;
            button1.Height -= 5;
            button1.Width -= 5;
            button2.Height -= 5;
            button2.Width -= 5;
            button3.Height -= 5;
            button3.Width -= 5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Height += 50;
            this.Width += 50;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            int result = Convert.ToInt32(textBox2.Text);
            int finalresult = result * 60 * 1000;
            timer1.Interval = finalresult;
            timer1.Start();
        }
        public void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int result = Convert.ToInt32(textBox2.Text);
            int finalresult = result * 1000;
            timer1.Interval = finalresult;
            timer1.Start();
        }
        public void button3_Click(object sender, EventArgs e)
        {
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button4.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            label1.Visible = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show(textBox1.Text, "Time is up!");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            MessageBox.Show("Timer has been stopped, reminder will not be displayed", "Stopped");
            timer1.Stop();
        }
    }
}