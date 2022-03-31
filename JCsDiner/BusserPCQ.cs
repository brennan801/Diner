using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
	public class BusserModel
	{
		public enum States { Free, Cleaning }
		public States State { get; set; }
		public int TableID { get; set; }
        public BusserModel()
        {
			State = States.Free;
        }
	}
    public class BusserPCQ : IDisposable
	{
		Thread _worker;
        public BusserModel Busser { get; set; }
        readonly object _locker = new object();
		Queue<BusserTask> ClearTableTasks = new Queue<BusserTask>();
		bool takingTasks;

		public BusserPCQ()
		{
			takingTasks = true;
			_worker = new Thread(BusBoy);
			_worker.Start();
			Busser = new();
		}

		public void EnqueueTask(BusserTask task)
		{
			lock (_locker)
			{
				task.Table.State = "queued";
				ClearTableTasks.Enqueue(task);
			}
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
					Busser.State = BusserModel.States.Cleaning;
					Busser.TableID = task.Table.ID;
					task.StartTask();
					Thread.Sleep(5000); // takes 3 seconds to 'clear' the table
					task.DoTask();
					Busser.State = BusserModel.States.Free;
				}
				else
					Thread.Sleep(1000);
			}
		}
	}
}
