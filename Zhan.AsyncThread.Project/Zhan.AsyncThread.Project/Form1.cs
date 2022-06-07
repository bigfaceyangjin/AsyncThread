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
            Console.WriteLine($"*********************** DoNothing Start {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") +" *********************************");
            long rs = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                rs += i;
            } 
            Console.WriteLine($"*********************** DoNothing End {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") +" *********************************");
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

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*********************** btnAdvance_Click_Start "+Thread.CurrentThread.ManagedThreadId.ToString("x2") +" *********************************");
            Action<string> act1 = this.DoNothing;
            IAsyncResult result = null;
			//AsyncCallback callback = ar => {
			//    Console.WriteLine(object.ReferenceEquals(result,ar)+result.AsyncState.ToString());
			//    Console.WriteLine($"计算结束 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}"); 
			//};
			result = act1.BeginInvoke("btnAdvance_Click", x => { Console.WriteLine("donothing的回调"); }, null);
            //怎样保证最后结果代码在子线程计算之后
            //方法一：利用iscompled属性判断异步是否完成 从而延迟主线程执行代码的时间
            { 
                //int i = 0;
                //while(!result.IsCompleted)
                //{    
                //    if(i<10)
                //    {
                //        Console.WriteLine($"文件已下载{i++ * 10}%");
                //    }
                //    else
                //    {
                //        Console.WriteLine("文件已下载99.99%...");
                //    } 
                //    Thread.Sleep(100);
                //}
                //    Console.WriteLine("文件已下载完成。");
            }
            //方法二 根据信号量 即时等待 限时等待
            Thread.Sleep(200);
            Console.WriteLine("Do Something else......");
            Console.WriteLine("Do Something else......");
            Console.WriteLine("Do Something else......");
            result.AsyncWaitHandle.WaitOne();   //组织主线程，等待子线程任务完成
            //result.AsyncWaitHandle.WaitOne(-1); //同上 
            //result.AsyncWaitHandle.WaitOne(1000);//不管任务有没有完成 等待1000ms后主线程开始工作 限时等待


            //方法三 endinvoke(); 不仅可以做到等待 还可以获取返回值 注意每一个异步操作只能使用一次endinvoke() 要么在回调里使用，要么在外面使用 否则异常
            {
                Func<string, int> func = n => { Console.WriteLine(n); Thread.Sleep(1000); return DateTime.Now.Day; };
                Console.WriteLine("func.Invoke(zz):"+func.Invoke("zz"));//同步调用

                //异步调用
                IAsyncResult asyncResult = func.BeginInvoke("yj", c => {
                    //一般情况下这个返回值时再回调中要用到
                    //int r= func.EndInvoke(c);
                    Console.WriteLine($"{c.AsyncState} {Thread.CurrentThread.ManagedThreadId.ToString("x2")}"); 
				}, "牛皮");

                Console.WriteLine("func.EndInvoke(asyncResult):"+ func.EndInvoke(asyncResult));
				asyncResult.AsyncWaitHandle.WaitOne();

			}
			
            Console.WriteLine("计算结果真的完成了");
            Console.WriteLine($"*********************** btnAdvance_Click_End {Thread.CurrentThread.ManagedThreadId.ToString("x2")} *********************************");
        }
    }
}
