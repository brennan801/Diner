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

        public PartyState(Party party)
        {
            this.Party = party;
            this.WaitCounter = 0;
        }
        public abstract void Run1();
    }
    public class WaitingInLobby : PartyState
    {
        public WaitingInLobby(Party party) : base(party) { }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class BeingSeated : PartyState
    {
        public BeingSeated(Party party) : base(party) { }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class DecidingOrder : PartyState
    {
        private int decidingTime;
        public DecidingOrder(Party party) : base(party) 
        {
            decidingTime = 2 * party.Customers.Count();
        }
        public override void Run1()
        {
            if (decidingTime > 0)
            {
                decidingTime--;
            }
            else
            {
                Party.State = new WaitingToOrder(Party);
            }
        }
    }
    public class WaitingToOrder : PartyState
    {
        public WaitingToOrder(Party party) : base(party) { }
        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class Ordering : PartyState
    {
        private int orderTime;
        public Ordering(Party party) : base(party)
        {
            orderTime = party.Customers.Count();
        }

        public override void Run1()
        {
            if (orderTime > 0)
            {
                orderTime--;
            }
            else
            {
                var order = Party.CreateOrder(); 
                Party.State = new WaitingForFood(Party);
            }
        }
    }
    public class WaitingForFood : PartyState
    {
        public WaitingForFood(Party party) : base(party) { }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class Eating : PartyState
    {
        private int eatingTime;
        public Eating(Party party) : base(party) {
            eatingTime = 2 * Party.Order.Appetizers + 5 * Party.Order.Platers;
        }

        public override void Run1()
        {
            if(eatingTime > 0)
            {
                eatingTime--;
            }
            else
            {
                // send waiter event
                Party.State = new WaitingForCheck(Party);
            }
        }
    }
    public class WaitingForCheck : PartyState
    {
        public WaitingForCheck(Party party) : base(party) { }

        public override void Run1()
        {
            WaitCounter++;
        }
    }
    public class RecievedCheck : PartyState
    {
        public RecievedCheck(Party party) : base(party) { }

        public override void Run1()
        {
            Party.State = new Left(Party);
        }
    }
    public class Left : PartyState
    {
        public Left(Party party) : base(party) { }
        
        public override void Run1()
        {
            return;
        }
    }
}
