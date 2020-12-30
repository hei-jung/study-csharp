using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPractice
{
    public partial class Form3 : Form
    {
        private bool capture = false;
        private Rectangle rectRoi;

        public Form3()
        {
            InitializeComponent();
            label1.Left = tabControl1.Width / 2 - label1.Width / 2;
            label1.Top = tabControl1.Height / 2 - label1.Height / 2;
            label2.Left = tabControl1.Width / 2 - label2.Width / 2;
            label2.Top = tabControl1.Height / 2 - label2.Height / 2;
            label3.Left = tabControl1.Width / 2 - label3.Width / 2;
            label3.Top = tabControl1.Height / 2 - label3.Height / 2;
            tabControl1.SelectedIndex = 2;
            screenshotToolStripMenuItem.Enabled = false;
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            tabControl1.Width = splitContainer1.Panel2.Width;
            tabControl1.Height = splitContainer1.Panel2.Height + 21;
            label1.Left = tabControl1.Width / 2 - label1.Width / 2;
            label1.Top = tabControl1.Height / 2 - label1.Height / 2;
            label2.Left = tabControl1.Width / 2 - label2.Width / 2;
            label2.Top = tabControl1.Height / 2 - label2.Height / 2;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            SubClient.NatsStop();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.Show();
            statusLabel1.Text = "검색";
            screenshotToolStripMenuItem.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            statusLabel1.Text = "대시보드";
            screenshotToolStripMenuItem.Enabled = true;//대시보드에서만 캡처 가능하도록
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            statusLabel1.Text = "설정";
            screenshotToolStripMenuItem.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            statusLabel2.Text =
                String.Format("{0}:{1:0#}:{2}",
                DateTime.Now.Hour, DateTime.Now.Minute,
                DateTime.Now.Second.ToString().PadLeft(2, '0'));
            //statusLabel2.Text = DateTime.Now.ToShortTimeString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void splitter_Moved(object sender, SplitterEventArgs e)
        {
            button1.Width = splitContainer1.Panel1.Width;
            button2.Width = splitContainer1.Panel1.Width;
            button3.Width = splitContainer1.Panel1.Width;
            tabControl1.Width = splitContainer1.Panel2.Width;
            label1.Left = tabControl1.Width / 2 - label1.Width / 2;
            label2.Left = tabControl1.Width / 2 - label2.Width / 2;
        }

        private void fullWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 작업 표시줄 제외 전체 화면
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            string fileName = "C:\\Users\\user1\\Downloads\\" +
                DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            ScreenCopy.AreaCapture(width, height, fileName);
        }

        private void freeAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capture = true;
        }

        private void Dashboard_MouseDown(object sender, MouseEventArgs e)
        {
            rectRoi = new Rectangle(e.X, e.Y, 0, 0);
            this.Refresh();
        }

        private void Dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rectRoi = new Rectangle(rectRoi.Left, rectRoi.Top,
                    Math.Min(e.X - rectRoi.Left, tabPage1.ClientRectangle.Width - rectRoi.Left),
                    Math.Min(e.Y - rectRoi.Top, tabPage1.ClientRectangle.Height - rectRoi.Top));
                this.Refresh();
            }
        }

        private void Dashboard_Paint(object sender, PaintEventArgs e)
        {
            if (capture == true)
            {
                using (Pen pen = new Pen(Color.Red, 1))
                {
                    e.Graphics.DrawRectangle(pen, rectRoi);
                }
            }
        }

        private void Dashboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (capture == true)
            {
                Console.WriteLine("Mouse up");
                //int x = this.Left + splitContainer1.Panel1.Width + rectRoi.X + 17;
                //int y = this.Top + menuStrip1.Height + toolStrip1.Height + splitContainer1.Panel2.Top + rectRoi.Y + 38;
                //ScreenCopy.FreeCapture(x, y, rectRoi.Width, rectRoi.Height, "C:\\Users\\user1\\Downloads\\test.jpg");
                MessageBox.Show("캡처 기능", "캡처", MessageBoxButtons.OK);
                capture = false;
                this.Refresh();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            SubClient.NatsStop();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            SubClient.NatsStart("192.168.0.4:4222", "test.1024");
        }

        //private void Esc_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((capture == true) && (e.KeyCode == Keys.Escape))
        //    {
        //        Console.WriteLine("Escape key pressed");
        //        e.SuppressKeyPress = true;
        //    }
        //}
    }
}
