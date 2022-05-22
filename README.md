# AsyncThread
Async异步和Thread多线程
Async异步和Thread多线程 //进程 线程 在计算机中的概念 
	进程： 一个程序运行时 占用的全部计算资源的总和 
	线程：程序执行流的最小单位 任何操作都是由线程完成的 线程是依托于进程存在的，一个进程可以包含多个进程，进程也可以有自己的计算资源 
	
	//多线程：多个执行流同时运行 
		1.CPU太快 分时间片执行 (存在上下文切换的问题),从微观角度讲一个核只能执行一个线程 宏观来说是多线程并发 
		2.多核多线程 核是指物理的核 线程是虚拟的核
		3.多线程的价值体现在多个独立任务可以同时运行

	最古老的线程类 Thread .net1.0时代就存在 是C#语言对线程对象的封装

	同步：等待计算之后，再进入下一行 异步：不会等待，直接进入下一行，非阻塞

	同步方法卡界面： why？ 上面说了任何操作都是由线程完成的，当这个主(UI)线程在执行同步方法时，没有空闲时间来对界面的执行 
	而异步多线程中主线程已经早早完事，计算任务已经交给了其他子线程操作了，从而大大提升用户体验

多线程弊端：
	多线程实际上是资源换成本 ，存在管理成本，所以线程虽然能加快速度，但并不是越多越好
	各线程的启动和结束是无序的，看CPU心情说话，每个线程处理相同的事情也有可能花费不同的时间，从而导致结束也有可能无序
	一定不要通过wait等待的形式控制线程的启动、执行时间和结束 thread.sleep();
	但是也可以通过 回调/状态等待/信号量 来控制线程
