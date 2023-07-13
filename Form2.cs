using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using designMode.observer;

namespace designMode
{
    public partial class Form2 : Form
    {
        private static Form2 instance;
      
        public static Form2 Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Form2();
                }

                return instance;
            }
        }
        private Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Taskmanager.TaskNotifyList.Notify("hello");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//顺序不能颠倒 否则会出现多出显示器的分辨率
            this.WindowState = FormWindowState.Maximized;


            this.BackColor = Color.Black;
            pictureBox1.BackColor = Color.Black;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
        }

        private  void pictureBox1_Click(object sender, EventArgs e)
        {
           
            if (!Form1.IsCanPlay) return ;
            MouseEventArgs mouseArgs = (MouseEventArgs)e;
            Taskmanager.TaskNotifyList.Notify(mouseArgs);
        }
    }
}
