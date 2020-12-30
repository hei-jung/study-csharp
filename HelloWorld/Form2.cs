using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPractice
{
    public partial class Form2 : Form
    {
        bool section = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (section == false)
            {
                splitContainer1.SplitterDistance = 50;
                tabControl1.Width = splitContainer1.Panel2.Width;
                section = true;
                button2.Width = 50;
                button3.Width = 50;
                button1.Text = "메뉴 펼치기";
            }
            else
            {
                splitContainer1.SplitterDistance = 100;
                tabControl1.Width = splitContainer1.Panel2.Width;
                section = false;
                button2.Width = 100;
                button3.Width = 100;
                button1.Text = "메뉴 접기";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;//TabControl의 tabPage 번호
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
    }
}
