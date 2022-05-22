using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Zhan.AsyncThread.Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("----  start 同步方法 ----");
            int z = 1;
            int m = 3;
            int x = z * m;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("btn_1"+i);
                this.DoNothing(name);
            }
            Console.WriteLine("----   end  同步方法 ----");
        }

        public void DoNothing(string name)
        {
            long rs = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                rs += i;
            }
            Console.WriteLine(name+":"+Thread.CurrentThread.ManagedThreadId);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("-----  start  -----");
            Action<string> action = this.DoNothing;
            for (int i = 0; i < 5; i++)
            {
                action.BeginInvoke("btn_2",null,null);
            }
            Console.WriteLine("-----   end  -----");
        }
    }
}
