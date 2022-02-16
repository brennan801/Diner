using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class WaiterPCQ : IDisposable
	{
		int count = 0;
		List<Thread> waiters;
		readonly object lockForWaiterTasks = new object();
		Queue<WaiterTask> getCheckTasks = new();
		Queue<WaiterTask> getOrderTasks = new();
		bool producerIsSendingTasks;

		public WaiterPCQ(int numOfWaiters)
		{
			waiters = new List<Thread>();
			producerIsSendingTasks = true;
			for (int i = 0; i < numOfWaiters; i++)
			{
				waiters.Add(new Thread(Waiter));
            }
			foreach(Thread waiter in waiters)
            {
				waiter.Start();
            }
		}

		public void EnqueueTask(WaiterTask task)
		{
			if (task.GetType() == typeof(GetCheckTask))
			{
				lock (lockForWaiterTasks) { getCheckTasks.Enqueue(task); }
			}
			else lock (lockForWaiterTasks) { getOrderTasks.Enqueue(task); }
				
		}

		public void Dispose()
		{
			producerIsSendingTasks = false;
			foreach(Thread waiter in waiters)
            {
				waiter.Join();
            }
		}

		void Waiter()
		{
			int id = count;
			count++;

            while (producerIsSendingTasks || QueuesNotEmpty())
            {
				WaiterTask task = null;
				lock (lockForWaiterTasks)
				{
					if (getCheckTasks.Count > 0)
					{
						task = getCheckTasks.Dequeue();
					}
					else if (getOrderTasks.Count > 0)
					{
						task = getOrderTasks.Dequeue();
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
            lock (lockForWaiterTasks) {
				if (getCheckTasks.Count() > 0 || getOrderTasks.Count() > 0)
				{
					return true;
				}
				else return false;
			}
        }
    }
}
