using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    namespace WithExplicitModelling
    {
        public class InKitchenOnlineTakeawayOrder
        {
            public InKitchenOnlineTakeawayOrder(Guid id, Address address)
            {
                this.Id = id;
                this.Address = address;
            }

            public Guid Id { get; private set; }

            public Address Address { get; private set; }

            // Only contains methods it actually impelements
            // returns new state so that clients have to be aware of it
            public InOvenOnlineTakeawayOrder Cook()
            {
                // ..
                return new InOvenOnlineTakeawayOrder(this.Id, this.Address);
            }
        }

        public class InOvenOnlineTakeawayOrder
        {
            public InOvenOnlineTakeawayOrder(Guid id, Address address)
            {
                this.Id = id;
                this.Address = address;
            }

            public Guid Id { get; private set; }

            public Address Address { get; private set; }

            public CookedOnlineTakeawayOrder TakeOutOfOven()
            {
                // ..
                return new CookedOnlineTakeawayOrder(this.Id, this.Address);
            }
        }

        public class CookedOnlineTakeawayOrder
        {
            public CookedOnlineTakeawayOrder(Guid id, Address address)
            {
                this.Id = id;
                this.Address = address;
            }

            public Guid Id { get; private set; }

            public Address Address { get; private set; }

            public OutForDeliveryOnlineTakeawayOrder Package()
            {
                // ..
                return new OutForDeliveryOnlineTakeawayOrder(this.Id, this.Address);
            }
        }

        public class OutForDeliveryOnlineTakeawayOrder
        {
            public OutForDeliveryOnlineTakeawayOrder(Guid id, Address address)
            {
                this.Id = id;
                this.Address = address;
            }

            public Guid Id { get; private set; }

            public Address Address { get; private set; }

            public void Deliver()
            {
                // ..
            }
        }
            
    }
    namespace WithNoisyStatePattern
    {
        public class OnlineTakeawayOrder
        {
            private IOnlineTakeawayOrderState state;

            public OnlineTakeawayOrder(Guid id, Address address)
            {
                this.Id = id;
                this.Address = address;
                this.state = new InKitchenQueue(this);
            }

            public Guid Id { get; private set; }

            public Address Address { get; private set; }

            public void Cook()
            {
                state = state.Cook();
            }

            public void TakeOutOfOven()
            {
                state = state.TakeOutOfOven();
            }

            public void Package()
            {
                state = state.Package();
            }

            public void Deliver()
            {
                state = state.Deliver();
            }
        }

        public interface IOnlineTakeawayOrderState
        {
            IOnlineTakeawayOrderState Cook();

            IOnlineTakeawayOrderState TakeOutOfOven();

            IOnlineTakeawayOrderState Package();

            IOnlineTakeawayOrderState Deliver();
        }

        public class InKitchenQueue : IOnlineTakeawayOrderState
        {
            private OnlineTakeawayOrder order;

            public InKitchenQueue(OnlineTakeawayOrder order)
            {
                this.order = order;
            }

            public IOnlineTakeawayOrderState Cook()
            {
                // handle this scenario accordingly
                return new InOven(order);
            }

            public IOnlineTakeawayOrderState TakeOutOfOven()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Package()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Deliver()
            {
                throw new ActionNotPermittedInThisState();
            }
        }

        public class InOven : IOnlineTakeawayOrderState
        {
            private OnlineTakeawayOrder order;

            public InOven(OnlineTakeawayOrder order)
            {
                this.order = order;
            }

            public IOnlineTakeawayOrderState Cook()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState TakeOutOfOven()
            {
                // handle this scenario accordingly
                return new Cooked(order);
            }

            public IOnlineTakeawayOrderState Package()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Deliver()
            {
                throw new ActionNotPermittedInThisState();
            }
        }

        public class Cooked : IOnlineTakeawayOrderState
        {
            private OnlineTakeawayOrder order;

            public Cooked(OnlineTakeawayOrder order)
            {
                this.order = order;
            }

            public IOnlineTakeawayOrderState Cook()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState TakeOutOfOven()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Package()
            {
                // handle this scenario accordingly
                return new OutForDelivery(order);
            }

            public IOnlineTakeawayOrderState Deliver()
            {
                throw new ActionNotPermittedInThisState();
            }
        }

        public class OutForDelivery : IOnlineTakeawayOrderState
        {
            private OnlineTakeawayOrder order;

            public OutForDelivery(OnlineTakeawayOrder order)
            {
                this.order = order;
            }

            public IOnlineTakeawayOrderState Cook()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState TakeOutOfOven()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Package()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Deliver()
            {
                // handle this scenario accordingly
                return new Delivered(order);
            }
        }

        public class Delivered : IOnlineTakeawayOrderState
        {
            private OnlineTakeawayOrder order;

            public Delivered(OnlineTakeawayOrder order)
            {
                this.order = order;
            }

            public IOnlineTakeawayOrderState Cook()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState TakeOutOfOven()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Package()
            {
                throw new ActionNotPermittedInThisState();
            }

            public IOnlineTakeawayOrderState Deliver()
            {
                throw new ActionNotPermittedInThisState();
            }
        }

        public class ActionNotPermittedInThisState : Exception { }
    }

    // See chapter 16 for proper Value Object examples
    public class Address
    {
        public string PhoneNumber { get; private set; }

        public string HouseNameOrNumber { get; private set; }

        public string Code { get; private set; }
    }
}
