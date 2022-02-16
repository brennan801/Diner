using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class BusserPCQ : IDisposable
	{
		Thread _worker;
		readonly object _locker = new object();
		Queue<BusserTask> ClearTableTasks = new Queue<BusserTask>();
		bool takingTasks;

		public BusserPCQ()
		{
			takingTasks = true;
			_worker = new Thread(BusBoy);
			_worker.Start();
		}

		public void EnqueueTask(BusserTask task)
		{
			lock (_locker) ClearTableTasks.Enqueue(task);
		}

		public void Dispose()
		{
			takingTasks = false;
			_worker.Join(); // Wait for the consumer's thread to finish.
		}

		void BusBoy()
		{
			while (takingTasks || ClearTableTasks.Count > 0)
			{
				BusserTask task = null;
				lock (_locker)
					if (ClearTableTasks.Count > 0)
					{
						task = ClearTableTasks.Dequeue();
					}
				if (task != null)
				{
					Console.WriteLine("BusBoy will now ClearTable number {0}", task.Table.ID);
					Thread.Sleep(2000); // takes 2 seconds to 'clear' the table
					task.DoTask();
				}
				else
					Thread.Sleep(1000);
			}
		}
	}
}
