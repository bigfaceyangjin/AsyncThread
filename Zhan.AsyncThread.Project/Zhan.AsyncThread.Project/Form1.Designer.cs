namespace Zhan.AsyncThread.Project
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.btn_tongbu = new System.Windows.Forms.Button();
			this.btn_Async = new System.Windows.Forms.Button();
			this.btnAdvance = new System.Windows.Forms.Button();
			this.btnThreads = new System.Windows.Forms.Button();
			this.btnThreadPool = new System.Windows.Forms.Button();
			this.btn_Task = new System.Windows.Forms.Button();
			this.btnParallel = new System.Windows.Forms.Button();
			this.btnThreadCore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btn_tongbu
			// 
			this.btn_tongbu.Location = new System.Drawing.Point(27, 46);
			this.btn_tongbu.Margin = new System.Windows.Forms.Padding(2);
			this.btn_tongbu.Name = "btn_tongbu";
			this.btn_tongbu.Size = new System.Drawing.Size(92, 32);
			this.btn_tongbu.TabIndex = 0;
			this.btn_tongbu.Text = "同步方法";
			this.btn_tongbu.UseVisualStyleBackColor = true;
			this.btn_tongbu.Click += new System.EventHandler(this.button1_Click);
			// 
			// btn_Async
			// 
			this.btn_Async.Location = new System.Drawing.Point(27, 112);
			this.btn_Async.Margin = new System.Windows.Forms.Padding(2);
			this.btn_Async.Name = "btn_Async";
			this.btn_Async.Size = new System.Drawing.Size(92, 38);
			this.btn_Async.TabIndex = 1;
			this.btn_Async.Text = "异步方法";
			this.btn_Async.UseVisualStyleBackColor = true;
			this.btn_Async.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnAdvance
			// 
			this.btnAdvance.Location = new System.Drawing.Point(27, 175);
			this.btnAdvance.Margin = new System.Windows.Forms.Padding(2);
			this.btnAdvance.Name = "btnAdvance";
			this.btnAdvance.Size = new System.Drawing.Size(92, 35);
			this.btnAdvance.TabIndex = 2;
			this.btnAdvance.Text = "AsyncAdvance";
			this.btnAdvance.UseVisualStyleBackColor = true;
			this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
			// 
			// btnThreads
			// 
			this.btnThreads.Location = new System.Drawing.Point(166, 46);
			this.btnThreads.Margin = new System.Windows.Forms.Padding(2);
			this.btnThreads.Name = "btnThreads";
			this.btnThreads.Size = new System.Drawing.Size(92, 32);
			this.btnThreads.TabIndex = 3;
			this.btnThreads.Text = "Threads";
			this.btnThreads.UseVisualStyleBackColor = true;
			this.btnThreads.Click += new System.EventHandler(this.btnThreads_Click);
			// 
			// btnThreadPool
			// 
			this.btnThreadPool.Location = new System.Drawing.Point(166, 112);
			this.btnThreadPool.Margin = new System.Windows.Forms.Padding(2);
			this.btnThreadPool.Name = "btnThreadPool";
			this.btnThreadPool.Size = new System.Drawing.Size(92, 32);
			this.btnThreadPool.TabIndex = 4;
			this.btnThreadPool.Text = "ThreadPool";
			this.btnThreadPool.UseVisualStyleBackColor = true;
			this.btnThreadPool.Click += new System.EventHandler(this.btnThreadPool_Click);
			// 
			// btn_Task
			// 
			this.btn_Task.Location = new System.Drawing.Point(166, 175);
			this.btn_Task.Margin = new System.Windows.Forms.Padding(2);
			this.btn_Task.Name = "btn_Task";
			this.btn_Task.Size = new System.Drawing.Size(92, 32);
			this.btn_Task.TabIndex = 5;
			this.btn_Task.Text = "Task";
			this.btn_Task.UseVisualStyleBackColor = true;
			this.btn_Task.Click += new System.EventHandler(this.btn_Task_Click);
			// 
			// btnParallel
			// 
			this.btnParallel.Location = new System.Drawing.Point(166, 240);
			this.btnParallel.Margin = new System.Windows.Forms.Padding(2);
			this.btnParallel.Name = "btnParallel";
			this.btnParallel.Size = new System.Drawing.Size(92, 32);
			this.btnParallel.TabIndex = 6;
			this.btnParallel.Text = "Parallel";
			this.btnParallel.UseVisualStyleBackColor = true;
			this.btnParallel.Click += new System.EventHandler(this.btnParallel_Click);
			// 
			// btnThreadCore
			// 
			this.btnThreadCore.Location = new System.Drawing.Point(27, 240);
			this.btnThreadCore.Name = "btnThreadCore";
			this.btnThreadCore.Size = new System.Drawing.Size(92, 32);
			this.btnThreadCore.TabIndex = 7;
			this.btnThreadCore.Text = "ThreadCore";
			this.btnThreadCore.UseVisualStyleBackColor = true;
			this.btnThreadCore.Click += new System.EventHandler(this.btnThreadCore_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(545, 314);
			this.Controls.Add(this.btnThreadCore);
			this.Controls.Add(this.btnParallel);
			this.Controls.Add(this.btn_Task);
			this.Controls.Add(this.btnThreadPool);
			this.Controls.Add(this.btnThreads);
			this.Controls.Add(this.btnAdvance);
			this.Controls.Add(this.btn_Async);
			this.Controls.Add(this.btn_tongbu);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_tongbu;
        private System.Windows.Forms.Button btn_Async;
        private System.Windows.Forms.Button btnAdvance;
		private System.Windows.Forms.Button btnThreads;
		private System.Windows.Forms.Button btnThreadPool;
		private System.Windows.Forms.Button btn_Task;
		private System.Windows.Forms.Button btnParallel;
		private System.Windows.Forms.Button btnThreadCore;
	}
}

