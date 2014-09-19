using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain
{
    public interface IHandleEvents<T>
    {
        void Handle(T @event);
    }

    public static class DomainEvents
    {
        private static List<Delegate> actions;
        private static List<object> handlers;

        public static void Register<T>(Action<T> callback)
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void Register<T>(IHandleEvents<T> handler)
        {
            if (handlers == null)
                handlers = new List<object>();

            handlers.Add(handler);
        }

        // invokes each handler synchronously inside the same thread
        // before returning control to the caller
        public static void Raise<T>(T @event)
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

        public static void UnRegister<T>(Action<T> callback)
        {
            actions.Remove(callback);
        }

        public static void ClearAll()
        {
            actions = new List<Delegate>();
            handlers = new List<object>();
        }
    }
}
