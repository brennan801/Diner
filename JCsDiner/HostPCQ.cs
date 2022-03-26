using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
	public class HostModel
	{
		public enum States { SeatingParty, Free }
		public States State { get; set; }
		public int PartyID {get; set;}
        public HostModel()
        {
			State = States.Free;
        }
    }
    public class HostPCQ : IDisposable
    {
        public HostModel HostModel { get; set; }
        Thread host;
		readonly object lockForHostTasks = new object();
		Queue<HostTask> hostTasks = new();

        bool producerIsSendingTasks;
		public HostPCQ()
		{
			HostModel = new HostModel();
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
						if (task.Restaurant.GetCapasity() >= task.Party.Customers)
						{
							task = hostTasks.Dequeue();
						}
						else task = null;
					}
				}
				if (task != null)
				{
					try
					{
						task.StartTask();
						HostModel.State = HostModel.States.SeatingParty;
						HostModel.PartyID = task.Party.ID;
					}
					catch(IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("putting task back in the queue");
						EnqueueTask(task);
						continue;
                    }

					Thread.Sleep(task.Time);
					task.DoTask();
					HostModel.State = HostModel.States.Free;
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
