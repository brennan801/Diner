using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public abstract class PartyState
    {
        public Party Party { get; protected set; }
        public int WaitCounter { get; set; }
        public enum States {WaitingInLobby, BeingSeated, DecidingOrder, WaitingToOrder, Ordering, WaitingForFood, Eating, WaitingForCheck, RecievedCheck, Left, QueuedByWaiter}
        public States Value { get; protected set; }

        public PartyState(Party party)
        {
            this.Party = party;
            this.WaitCounter = 0;
        }
        public abstract void Run1();
    }
    public class PartyWaitingInLobby : PartyState
    {
        public PartyWaitingInLobby(Party party) : base(party) 
        {
            Value = States.WaitingInLobby;
        }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class PartyBeingSeated : PartyState
    {
        public PartyBeingSeated(Party party) : base(party)
        { 
            Value = States.BeingSeated;
        }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class PartyDecidingOrder : PartyState
    {
        private int decidingTime;
        public PartyDecidingOrder(Party party) : base(party) 
        {
            Value = States.DecidingOrder;
            decidingTime = (int)((2/3) * party.Customers);
        }
        public override void Run1()
        {
            if (decidingTime > 0)
            {
                decidingTime--;
            }
            else
            {
                Party.State = new PartyWaitingToOrder(Party);
                Console.WriteLine($"\t Party {Party.ID} is ready to order");
            }
        }
    }
    public class PartyWaitingToOrder : PartyState
    {
        public PartyWaitingToOrder(Party party) : base(party) 
        {
            Value = States.WaitingToOrder;
        }
        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class PartyOrdering : PartyState
    {
        public PartyOrdering(Party party) : base(party)
        {
            Value = States.Ordering;
        }

        public override void Run1()
        {
            return;
        }
    }
    public class PartyWaitingForFood : PartyState
    {
        public PartyWaitingForFood(Party party) : base(party) 
        {
            Value = States.WaitingForFood;
        }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class PartyEating : PartyState
    {
        private int eatingTime;

        public PartyEating(Party party, int averageEatingTime) : base(party) {
            Value = States.Eating;
            var rand = new Random();
            var randNum = rand.Next(averageEatingTime - 3, averageEatingTime + 3);
            eatingTime = randNum;
        }

        public override void Run1()
        {
            if(eatingTime > 0)
            {
                eatingTime--;
            }
            else
            {
                Party.State = new PartyWaitingForCheck(Party);
                Console.WriteLine($"\t Party {Party.ID} is waiting for the check");
            }
        }
    }
    public class PartyWaitingForCheck : PartyState
    {
        public PartyWaitingForCheck(Party party) : base(party) 
        {
            Value = States.WaitingForCheck;
        }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class PartyRecievedCheck : PartyState
    {
        public PartyRecievedCheck(Party party) : base(party) 
        {
            Value = States.RecievedCheck;
        }

        public override void Run1()
        {
            Party.Table.State = "dirty";
            Party.State = new PartyLeft(Party);
            Console.WriteLine($"\t Party {Party.ID} is ready to leave");
        }
    }
    public class PartyLeft : PartyState
    {
        public PartyLeft(Party party) : base(party) 
        {
            Value = States.Left;
        }
        
        public override void Run1()
        {
            return;
        }
    }
    public class PartyQueued : PartyState
    {
        public PartyQueued(Party party) : base(party) 
        {
            Value = States.QueuedByWaiter;
        }


        public override void Run1()
        {
            return;
        }
    }
}
