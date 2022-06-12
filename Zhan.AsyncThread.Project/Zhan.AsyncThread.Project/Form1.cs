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

		#region 同步方法

		/// <summary>
		/// 同步方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			Console.WriteLine("----  start 同步方法 ----");
			int z = 1;
			int m = 3;
			int x = z * m;
			for (int i = 0; i < 5; i++)
			{
				string name = string.Format("btn_1" + i);
				this.DoNothing(name);
			}
			Console.WriteLine("----   end  同步方法 ----");
		}
		#endregion

		#region Async异步
		/// <summary>
		/// 5个异步/线程 并发 不卡界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			Console.WriteLine("-----  start  -----");
			Action<string> action = this.DoNothing;
			for (int i = 0; i < 5; i++)
			{
				action.BeginInvoke("btn_2", null, null);
			}
			Console.WriteLine("-----   end  -----");
		}
		#endregion

		#region Async 进阶
		/// <summary>
		/// Async 异步begin.Invoke(); 回调函数 以及控制子线程的方法 状态/信号量/EndInvoke();
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdvance_Click(object sender, EventArgs e)
		{
			Console.WriteLine("*********************** btnAdvance_Click_Start " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " *********************************");
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
				Console.WriteLine("func.Invoke(zz):" + func.Invoke("zz"));//同步调用

				//异步调用
				IAsyncResult asyncResult = func.BeginInvoke("yj", c => {
					//一般情况下这个返回值时再回调中要用到
					//int r= func.EndInvoke(c);
					Console.WriteLine($"{c.AsyncState} {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
				}, "牛皮");

				Console.WriteLine("func.EndInvoke(asyncResult):" + func.EndInvoke(asyncResult));
				asyncResult.AsyncWaitHandle.WaitOne();

			}

			Console.WriteLine("计算结果真的完成了");
			Console.WriteLine($"*********************** btnAdvance_Click_End {Thread.CurrentThread.ManagedThreadId.ToString("x2")} *********************************");
		}
		#endregion

		#region Thread
		/// <summary>
		/// .net1.0 推出 Threads
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnThreads_Click(object sender, EventArgs e)
		{
			Console.WriteLine($"******************** btnThreads_Click_Start {Thread.CurrentThread.ManagedThreadId.ToString("00")}********************");
			{
				ThreadStart threadStart = () => this.DoNothing("btnThreads_Click");
				Thread thread = new Thread(threadStart);
				thread.Start();
				//threadStart.BeginInvoke(null, null); 
				//thread.Suspend();//线程暂停 挂起
				//thread.Resume(); //线程唤醒 恢复
				//thread.Join(500);  //当前线程等待thread500ms 延时等待
				thread.Join();   //当前线程等待thread完成
								 //while (thread.ThreadState!=ThreadState.Stopped)//循环判断线程状态
								 //{
								 //	Thread.Sleep(200);//当前线程等待200ms
								 //} 
								 //线程优先级 有多个线程执行任务时 优先执行该线程 但不代表它先完成
								 //thread.Priority = ThreadPriority.Highest;

				//程序默认是前台线程(false)  指定为后台线程
				//前台线程会阻止程序的关闭直到线程任务的完成 后台线程会随之结束
				//前台线程这一特点可用于写日志 在程序结束前执行
				//thread.IsBackground = true;
				try
				{
					//thread.Abort();//销毁线程 通过抛异常的方式销毁
				}
				catch (Exception ex)
				{
					//Console.WriteLine(ex.Message);
					//Thread.ResetAbort();//撤销当前线程的销毁
				}
			}

			Console.WriteLine($"******************** btnThreads_Click_End {Thread.CurrentThread.ManagedThreadId.ToString("00")}********************");
		}
		#endregion

		#region ThreadPool 线程池
		/// <summary>
		/// .net 2.0 推出 ThreadPool 线程池
		/// 线程池--享元模式--数据库连接池
		/// 1.Thread提供的api太多了，相当于给三岁小孩一把枪，同样危害极大
		/// 2.避免了无限使用线程，加以限制。避免了重复使用线程，减少了创建和销毁
		///	  重用性的体现在于每个线程将任务完成后没有销毁，而是回归到了线程池
		///	  从而给下次再需要用的时候还是之前的线程 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnThreadPool_Click(object sender, EventArgs e)
		{
			Console.WriteLine($"********** btnThreadPool_Click_Start {Thread.CurrentThread.ManagedThreadId.ToString("x2")} **********");
			//ManualResetEvent resetEvent = new ManualResetEvent(false);
			//{
			//	//简写法：实际上就是直接写一个委托 再在委托里调用dosomething方法传参  
			//	//线程等待 
			//	ThreadPool.QueueUserWorkItem(x =>
			//	{ 
			//		this.DoSomething("btnThreadPool_Click");
			//		resetEvent.Set(); 
			//	} 
			//	); 
			//}
			//{
			//	//辅助线程(工作线程)的最大数 和 异步I/O的线程最大数
			//	ThreadPool.GetMaxThreads(out int workers, out int ports);
			//	Console.WriteLine($"MaxWorkerThreads:{workers} MaxCompletionPortThreads:{ports}");
			//	ThreadPool.GetMinThreads(out int workers2, out int ports2);
			//	Console.WriteLine($"MaxWorkerThreads:{workers2} MaxCompletionPortThreads:{ports2}");

			//	//设置最大线程数
			//	ThreadPool.SetMaxThreads(16, 16);
			//	ThreadPool.SetMinThreads(5, 5);

			//	Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");

			//	ThreadPool.GetMaxThreads(out int workers3, out int ports3);
			//	Console.WriteLine($"MaxWorkerThreads:{workers3} MaxCompletionPortThreads:{ports3}");
			//	ThreadPool.GetMinThreads(out int workers4, out int ports4);
			//	Console.WriteLine($"MaxWorkerThreads:{workers4} MaxCompletionPortThreads:{ports4}");
			//}
			//{
			//	resetEvent.WaitOne();
			//	resetEvent.Reset();
			//	Console.WriteLine("☆☆☆计算完成☆☆☆");
			//}

			//一般来说不要阻塞线程池的线程 例如一下这种情况就会造成死锁
			//死锁： 
			//ThreadPool.SetMaxThreads(16, 16); 
			//for (int i = 0; i < 20; i++)
			//{
			//	int k = i;
			//	ThreadPool.QueueUserWorkItem(x => {

			//		Console.WriteLine(k + $"  {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
			//		if (k < 18)
			//		{
			//			resetEvent.WaitOne();
			//		}
			//		else
			//		{
			//			resetEvent.Set();
			//		}
			//	}); 
			//}

			//自制回调 ThreadPool是没有回调函数的 
			{
				//无参 无返回值的回调
				this.WaitThreadCallBack(
					() => {
						Console.WriteLine($"这里是Action {Thread.CurrentThread.ManagedThreadId.ToString("X2")}");
					}
					//回调函数
					, (i) => {
						Console.WriteLine($"这里是CallBack{Thread.CurrentThread.ManagedThreadId.ToString("X2")}");
					});
				Console.WriteLine("\n============== 分割线 ==============\n");
				//带返回值的异步调用
				Func<int> func = this.WaitThreadWithResult<int>(() => {
					Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
					return DateTime.Now.Day;
				});
				//Thread.Sleep(2000);
				Console.WriteLine(func.Invoke() + $" {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
			}

			Console.WriteLine($"********** btnThreadPool_Click_End {Thread.CurrentThread.ManagedThreadId.ToString("x2")} **********");
		}
		#endregion

		#region Task

		private void btn_Task_Click(object sender, EventArgs e)
		{
			Console.WriteLine("*********** btn_Task_Click_Start " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
			{
				//Task启动异步
				//Task.Run(() => { this.DoNothing("btn_Task_Click1"); });
				//Task.Run(() => { this.DoNothing("btn_Task_Click2"); });
				//另一种启动方式
				//TaskFactory taskFactory= Task.Factory;
				//taskFactory.StartNew(()=> { this.DoNothing("btn_Task_Click"); });
				//new Task(() => { this.DoNothing("btn_Task_Click"); }).Start();

				//Console.WriteLine($"项目经理启动项目... {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
				//Console.WriteLine($"前期准备工作完成... {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
				//Console.WriteLine($"开始编码... {Thread.CurrentThread.ManagedThreadId.ToString("x2")}"); 
				//存放Task容器
				List<Task> list = new List<Task>();
				list.Add(Task.Run(() => { this.Coding("张三", "Client"); }));
				list.Add(Task.Run(() => { this.Coding("李四", "Service"); }));
				list.Add(Task.Run(() => { this.Coding("王五", "Jump"); }));
				list.Add(Task.Run(() => { this.Coding("朱展", "Api"); }));
				list.Add(Task.Run(() => { this.Coding("杨锦", "Monitor"); }));
				//Task.WaitAny(list.ToArray());//等待某个任务完成
				//Console.WriteLine("已完成某项开发...");
				//Task.WaitAll(list.ToArray());//等待所有任务完成
				//Task.WhenAll(list.ToArray()).ContinueWith(x => { Console.WriteLine($"部署环境，联调测试 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}"); });
				//Task.WhenAny(list.ToArray()).ContinueWith(x => { Console.WriteLine($"我先做完了，就先去写文档了... {Thread.CurrentThread.ManagedThreadId.ToString("x2")}"); }) ;

				//Console.WriteLine($"通知甲方验收 上线使用 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");

				//一万个任务
				//如何做到只使用10个线程
				//List<Task> tasks = new List<Task>();
				//for (int i = 1; i < 1000; i++)
				//{
				//	tasks.Add(Task.Run(() => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString("x2")); Thread.Sleep(new Random(i).Next(10, 30)); }));
				//	if (tasks.Count > 10)
				//	{
				//		Task.WhenAny(tasks.ToArray());
				//		tasks = tasks.Where(x => x.Status != TaskStatus.RanToCompletion).ToList();
				//	}
				//}
				//Task.WhenAll(tasks.ToArray());
			}
			{
				//通过TaskFactory启动线程可以获取线程标识 知道是哪一个线程先完成了
				//List<Task> list = new List<Task>();
				//TaskFactory taskFactory = Task.Factory;
				//list.Add(taskFactory.StartNew(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString("x2")); }, "张三")); ;
				//list.Add(taskFactory.StartNew(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString("x2")); }, "李四")); ;
				//list.Add(taskFactory.StartNew(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString("x2")); }, "王五")); ;
				//taskFactory.ContinueWhenAny(list.ToArray(), x => { Console.WriteLine(x.AsyncState);});
				//taskFactory.ContinueWhenAll(list.ToArray(), x => { Console.WriteLine(x[2].AsyncState); });
			}
			{
				//Delay延迟触发动作 与Thread.Sleep()；类似 但有区别
				Task.Delay(1000).ContinueWith(t => { });//不卡界面 delay是表示不管你现在在干什么 反正我1000ms后执行这个动作 不卡界面
				Thread.Sleep(1000);//卡界面 
								   //如果既不想卡界面 又要用Thread 那就只有在线程里用Thread  与Thread是一样的效果
				Task.Run(() => {
					Thread.Sleep(1000);//不会卡界面
				});
			}
			Console.WriteLine("*********** btn_Task_Click_End " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
		}
		#endregion

		#region Parallel
		/// <summary>
		/// 并行编程 在Task的基础上做了封装 .net 4.5
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnParallel_Click(object sender, EventArgs e)
		{
			Console.WriteLine("*********** btnParallel_Click_Start " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
			{
				//parallel启动异步多线程的三种方式 
				//1.
				//Action[] actions = { () => this.Coding("张三", "前端"), () => this.Coding("李四", "后端"), () => this.Coding("王五", "服务"), };
				//Parallel.Invoke(actions);

				//2.
				//Parallel.For(0, 5, x => { this.Coding("朱展","开发"); });

				//3.
				//Parallel.ForEach<int>(new int[] { 2, 3 }, i => { Console.WriteLine(i); this.Coding("朱展", "开发"); });
			}
			{
				//Parallel是天生自带WaitAll的 主线程也会工作 节约了一个线程
				//控制并发线程数 ParallelOptions.MaxDegreeOfParallelism
				ParallelOptions parallelOptions = new ParallelOptions();
				parallelOptions.MaxDegreeOfParallelism = 2;
				//Parallel.For(0,5,parallelOptions, x => { this.Coding("朱展", "开发"); });

				//那怎么做到不卡界面呢？  包一层 没有什么是包一层不能解决的 如果有 那就再包一层
				Task.Run(() => {
					Parallel.For(0, 5, parallelOptions, x => { this.Coding("朱展", "开发"); });
				});
			}
			Console.WriteLine("*********** btnParallel_Click_End " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
		}
        #endregion

        #region 1.异常处理、线程取消、多线程的临时变量 2.线程安全和lock锁 3.await/async
        private static readonly object btnThreadCoreLock = new object();//Lock锁引用类型 CSDN标准写法
        private void btnThreadCore_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*********** btnThreadCore_Click_Start " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
            try
            {
                #region 多线程异常处理
                //TaskFactory taskFactory = new TaskFactory();
                //List<Task> taskList = new List<Task>();
                //for (int i = 0; i < 15; i++)
                //{
                //	string name = "btnThreadCore_Click_" + (i + 1);
                //	Action<object> act = j =>
                //	{
                //		try
                //		{
                //			if (j.Equals($"btnThreadCore_Click_9") || j.Equals($"btnThreadCore_Click_10"))
                //			{
                //				throw new Exception($"{j} 执行失败");
                //			}
                //			Console.WriteLine($"{j} 执行成功");
                //		}
                //		catch (Exception ex)
                //		{
                //			Console.WriteLine(ex.Message);
                //		}
                //	};
                //	taskList.Add(taskFactory.StartNew(act, name));
                //}
                #endregion

                #region 线程的取消
                //CancellationTokenSource cts = new CancellationTokenSource();
                //List<Task> taskList = new List<Task>();
                //TaskFactory taskFactory = new TaskFactory();
                //for (int i = 0; i < 10; i++)
                //{
                //	string name = string.Format("btnThreadCore_{0}",i+1);
                //	Action<object> act = x => {
                //		try
                //		{ 
                //			if (x.ToString().Equals("btnThreadCore_5") || x.ToString().Equals("btnThreadCore_6"))
                //			{
                //				throw new Exception($"{x}执行失败 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
                //			}
                //			if (cts.IsCancellationRequested)
                //			{
                //				Console.WriteLine($"{x}放弃执行 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
                //				return;
                //			}
                //			else {
                //				Console.WriteLine($"{x}执行成功 {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
                //			}
                //		}
                //		catch (Exception ex)
                //		{
                //			cts.Cancel();
                //			Console.WriteLine(ex.Message);
                //		}
                //	};
                //	taskList.Add(taskFactory.StartNew(act,name,cts.Token));
                //}
                //Task.WaitAll(taskList.ToArray());
                #endregion

                #region 临时变量 
                //for (int i = 0; i < 10; i++)
                //{
                //	//Thread.Sleep(200);
                //	int k = i;
                //	k = i;
                //	Task.Run(()=> {
                //		Console.WriteLine($"k={k} i={i}");
                //	});
                //}
                #endregion

                #region 线程安全
                List<int> numList = new List<int>();
                List<Task> taskList = new List<Task>();
                int total = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    int k = i;
                    taskList.Add(Task.Run(() => {
                        //lock后的方法块 只允许一个线程进入，相当于对这个引用变量占用这个引用链接 不要用string 因为享元
                        //引用链接内存边界存在一个值为-1，当有人占用时为0，无人占用时为-1 虽然没了并发 但影响了性能
                        lock (btnThreadCoreLock)
                        {
                            total += 1;
                            numList.Add(k);
                        }
                        //Monitor与lock功能一致 {}就是锁住的内容 只允许单线程操作
                        //Monitor.Enter(btnThreadCoreLock);
                        //{

                        //}
                        //Monitor.Exit(btnThreadCoreLock);
                    }));
                }
                Task.WaitAll(taskList.ToArray());
                Console.WriteLine($"numList总数量：{numList.Count} total={total}");
                #endregion
            }
            catch (AggregateException agp)
            {
                foreach (var item in agp.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("*********** btnThreadCore_Click_End " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " ***********");
        }
        #endregion
        /// <summary>
        /// 线程测试的方法
        /// </summary>
        /// <param name="name"></param>
        private void DoNothing(string name)
		{
			Console.WriteLine($"************* DoNothing Start {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " *************");
			long rs = 0;
			for (int i = 0; i < 1000000000; i++)
			{
				rs += i;
			}
			Console.WriteLine($"************* DoNothing End {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " *************");
		}

		private void DoSomething(object name)
		{
			Console.WriteLine($"************* DoNothing Start {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " *************");
			long rs = 0;
			for (int i = 0; i < 1000000000; i++)
			{
				rs += i;
			}
			Console.WriteLine($"************* DoNothing End {name} " + Thread.CurrentThread.ManagedThreadId.ToString("x2") + " *************");
		}

		private void Coding(string name, string project)
		{
			Console.WriteLine($"********** Coding {name} Start {project} ********** {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
			long temp = 0;
			for (int i = 0; i < 1000000000; i++)
			{
				temp += i;
			}
			Console.WriteLine($"********** Coding {name} End {project} ********** {Thread.CurrentThread.ManagedThreadId.ToString("x2")}");
		}

		/// <summary>
		/// Thread手动回调 无返回值
		/// </summary>
		/// <param name="act"></param>
		/// <param name="call"></param>
		private void WaitThreadCallBack(Action act, Action<int> call)
		{
			Thread thread = new Thread(x => {
				act.Invoke();
				call.Invoke(123);
			});
			thread.Start();
		}
		/// <summary>
		/// Thread回调 带返回值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		private Func<T> WaitThreadWithResult<T>(Func<T> func)
		{
			T t = default(T);
			Thread thread = new Thread(x => {
				t = func.Invoke();
			});
			thread.Start();
			return () => {
				//while (thread.ThreadState != ThreadState.Stopped)
				//{
				//	Thread.Sleep(200);
				//}
				thread.Join();
				return t;
			};
		}

		/// <summary>
		/// ThreadPool 无返回值回调
		/// </summary>
		/// <param name="act"></param>
		/// <param name="callback"></param>
		private void WaitThreadPoolCall(Action act, Action<int> callback)
		{
			ThreadPool.QueueUserWorkItem(x => {
				act.Invoke();
				callback.Invoke(1);
			});
		}
		/// <summary>
		/// ThreadPool带返回值异步调用 与BeginEnd类似
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		private T WaitThreadPoolResult<T>(Func<T> func)
		{
			T result = default(T);
			ManualResetEvent manualReset = new ManualResetEvent(false);
			ThreadPool.QueueUserWorkItem(x => {
				result = func.Invoke();
				manualReset.Set();
			});
			manualReset.Reset();
			//manualReset.WaitOne();
			return result;
		}

        
         
    }
}
