using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class HostPCQ : IDisposable
    {
		Thread host;
		readonly object lockForHostTasks = new object();
		Queue<HostTask> hostTasks = new();

        bool producerIsSendingTasks;
		public HostPCQ()
		{
			producerIsSendingTasks = true;
			host = new Thread(Host);
			host.Start();
		}

		public void EnqueueTask(HostTask task)
		{
			lock (lockForHostTasks) { hostTasks.Enqueue(task); }
		}

		public void Dispose()
		{
			producerIsSendingTasks = false;
			host.Join();
		}

		void Host()
		{
			while (producerIsSendingTasks || QueuesNotEmpty())
			{
				HostTask task = null;
				lock (lockForHostTasks)
				{
					if (hostTasks.Count > 0)
					{
						task = hostTasks.Peek();
						if (task.Restaurant.GetCapasity() >= task.Party.Customers.Count)
						{
							task = hostTasks.Dequeue();
						}
						else task = null;
					}
				}
				if (task != null)
				{
					task.StartTask();
					Thread.Sleep(task.Time);
					task.DoTask();
				}
				else Thread.Sleep(1000);
			}
		}

		private bool QueuesNotEmpty()
		{
			lock (lockForHostTasks)
			{
				if (hostTasks.Count() > 0)
				{
					return true;
				}
				else return false;
			}
		}
	}
}
