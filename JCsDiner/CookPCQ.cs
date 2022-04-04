using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
	public class CookModel
    {
        public int ID { get; set; }
		public enum States { Free, Cooking}
		public States State { get; set; }
		public int OrderID { get; set; }
    }
    public class CookPCQ : IDisposable
    {
		int count = 0;
        public List<Thread> CookThreads { get; private set; }
		public List<CookModel> Cooks { get; private set; }
		readonly object lockForWaiterTasks = new object();
		Queue<CookTask> cookTasks = new();
		public int RunSpeed { get; set; }

		bool producerIsSendingTasks;

        public CookPCQ()
        {
			CookThreads = new List<Thread>();
			Cooks = new List<CookModel>();
        }
		public CookPCQ(int numOfCooks, int runSpeed)
		{
			RunSpeed = runSpeed;
			CookThreads = new List<Thread>();
			Cooks = new List<CookModel>();
			producerIsSendingTasks = true;
			for (int i = 0; i < numOfCooks; i++)
			{
				CookThreads.Add(new Thread(Cook));
				Cooks.Add(new CookModel { ID = i, State = CookModel.States.Free});
			}
			foreach (Thread cook in CookThreads)
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
			foreach (Thread cook in CookThreads)
			{
				cook.Join();
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
					IEnumerable<CookModel> cookQuery =
						from cook in Cooks
						where cook.ID == id
						select cook;
					CookModel selectedCook = cookQuery.FirstOrDefault();
					selectedCook.State = CookModel.States.Cooking;
					selectedCook.OrderID = task.Order.Table.Party.ID;
					Thread.Sleep(task.Time * RunSpeed);
					task.DoTask(id);
					selectedCook.State = CookModel.States.Free;
				}
				else Thread.Sleep(RunSpeed);
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
