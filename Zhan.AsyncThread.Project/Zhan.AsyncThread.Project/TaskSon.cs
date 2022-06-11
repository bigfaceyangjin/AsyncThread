using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhan.AsyncThread.Project
{
	public class TaskSon : Task
	{
		//由于父类没有无参构造 解决办法 1：在父类手动添加无参构造(这里Task类是元数据无法修改) 2:在执行构造方法之前 先执行父类中的构造方法
		public TaskSon():base(()=> { })
		{
			
		}
		public  string states { get; set; }
	}
}
