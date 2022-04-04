using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
	public class WaiterModel
    {
        public WaiterModel(int id)
        {
			State = States.Free;
			ID = id;
        }
        public int ID { get; set; }
		public enum States { Free, GettingCheck, ReturningOrder, GettingOrder }
		public States State { get; set; }
		public int PartyID { get; set; }
		public int TableID { get; set; }
    }

    public class WaiterPCQ : IDisposable
	{
		int count = 0;
		List<Thread> waiters;
        public List<WaiterModel> WaiterModels { get; set; }
        public int RunSpeed { get; }

        readonly object lockForWaiterTasks = new object();
		Queue<WaiterTask> getCheckTasks = new();
		Queue<WaiterTask> getOrderTasks = new();
		Queue<WaiterTask> returnOrderTasks = new();
		bool producerIsSendingTasks;

        public WaiterPCQ()
        {
			this.WaiterModels = new List<WaiterModel>();
        }
		public WaiterPCQ(int numOfWaiters, int runSpeed)
		{
			this.WaiterModels = new List<WaiterModel>();
			waiters = new List<Thread>();
			producerIsSendingTasks = true;
			for (int i = 0; i < numOfWaiters; i++)
			{
				waiters.Add(new Thread(Waiter));
				WaiterModels.Add(new WaiterModel(i));
            }
			foreach(Thread waiter in waiters)
            {
				waiter.Start();
            }
            RunSpeed = runSpeed;
        }

		public void EnqueueTask(WaiterTask task)
		{
			if (task.GetType() == typeof(GetCheckTask))
			{
				lock (lockForWaiterTasks) 
				{
					task.Party.State = new PartyQueued(task.Party);
					getCheckTasks.Enqueue(task); 
				}
			}
			else if (task.GetType() == typeof(ReturnOrderTask))
			{
				lock (lockForWaiterTasks) 
				{
					task.Party.State = new PartyQueued(task.Party);
					task.Party.Order.State = "Queued";
					returnOrderTasks.Enqueue(task);
				}
			}
			else if (task.GetType() == typeof(GetOrderTask))
			{
				lock (lockForWaiterTasks)
				{
					task.Party.State = new PartyQueued(task.Party);
					getOrderTasks.Enqueue(task);
				}
			}
				
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
					else if(returnOrderTasks.Count > 0)
                    {
						task = returnOrderTasks.Dequeue();
                    }
					else if (getOrderTasks.Count > 0)
					{
						task = getOrderTasks.Dequeue();
					}
				}
				if (task != null)
				{
					IEnumerable<WaiterModel> waiterQuery =
						from waiter in WaiterModels
						where waiter.ID == id
						select waiter;
					WaiterModel selectedWaiter = waiterQuery.FirstOrDefault();
					if (task.GetType() == typeof(GetCheckTask)) { selectedWaiter.State = WaiterModel.States.GettingCheck; }
					else if (task.GetType() == typeof(ReturnOrderTask)) { selectedWaiter.State = WaiterModel.States.ReturningOrder; }
					else if (task.GetType() == typeof(GetOrderTask)) { selectedWaiter.State = WaiterModel.States.GettingOrder; }
					selectedWaiter.PartyID = task.Party.ID;
					selectedWaiter.TableID = task.Party.Table.ID;
					task.StartTask(id);
					Thread.Sleep(task.Time * RunSpeed);
					task.DoTask(id);
					selectedWaiter.State = WaiterModel.States.Free;
				}
				else Thread.Sleep(RunSpeed);
			}
		}

        private bool QueuesNotEmpty()
        {
            lock (lockForWaiterTasks) {
				if (getCheckTasks.Count > 0 || getOrderTasks.Count > 0 || returnOrderTasks.Count > 0)
				{
					return true;
				}
				else return false;
			}
        }
    }
}
