using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class CookPCQ : IDisposable
    {
		int count = 0;
		List<Thread> cooks;
		readonly object lockForWaiterTasks = new object();
		Queue<CookTask> cookTasks = new();

		bool producerIsSendingTasks;
		public CookPCQ(int numOfCooks)
		{
			producerIsSendingTasks = true;
			for (int i = 0; i < numOfCooks; i++)
			{
				cooks.Add(new Thread(Cook));
			}
			foreach (Thread cook in cooks)
			{
				cook.Start();
			}
		}

		public void EnqueueTask(CookTask task)
		{
			lock (lockForWaiterTasks) { cookTasks.Enqueue(task); }
		}

		public void Dispose()
		{
			producerIsSendingTasks = false;
			foreach (Thread waiter in cooks)
			{
				waiter.Join();
			}
		}

		void Cook()
		{
			int id = count;
			count++;

			while (producerIsSendingTasks || QueuesNotEmpty())
			{
				CookTask task = null;
				lock (lockForWaiterTasks)
				{
					if (cookTasks.Count > 0)
					{
						task = cookTasks.Dequeue();
					}
				}
				if (task != null)
				{
					task.StartTask(id);
					Thread.Sleep(task.Time);
					task.DoTask(id);
				}
				else Thread.Sleep(1000);
			}
		}

		private bool QueuesNotEmpty()
		{
			lock (lockForWaiterTasks)
			{
				if (cookTasks.Count() > 0)
				{
					return true;
				}
				else return false;
			}
		}
	}
}
