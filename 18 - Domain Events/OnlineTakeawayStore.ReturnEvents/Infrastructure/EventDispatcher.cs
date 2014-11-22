using OnlineTakeawayStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.ReturnEvents.Infrastructure
{
    public interface IEventDispatcher
    {
        void Register<T>(Action<T> callback);

        void Register<T>(IHandleEvents<T> handler);

        void Dispatch<T>(T @event);

        void UnRegister<T>(Action<T> callback);

        void ClearAll();
    }

    public interface IHandleEvents<T>
    {
        void Handle(T @event);
    }

    public class EventDispatcher : IEventDispatcher
    {
        private static List<Delegate> actions;
        private static List<object> handlers;

        public void Register<T>(Action<T> callback)
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public void Register<T>(IHandleEvents<T> handler)
        {
            if (handlers == null)
                handlers = new List<object>();

            handlers.Add(handler);
        }

        public void Dispatch<T>(T @event)
        {
            if (handlers != null)
            {
                handlers.OfType<IHandleEvents<T>>()
                        .ToList()
                        .ForEach(h => h.Handle(@event));
            }
            if (actions != null)
            {
                actions.OfType<Action<T>>()
                       .ToList()
                       .ForEach(a => a(@event));
            }
        }

        public void UnRegister<T>(Action<T> callback)
        {
            actions.Remove(callback);
        }

        public void ClearAll()
        {
            actions = new List<Delegate>();
            handlers = new List<object>();
        }
    }

}
