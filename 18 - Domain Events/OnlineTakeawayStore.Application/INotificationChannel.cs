using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Application
{
    // Simulates an abstraction over real-time communications akin to SignalR
    public interface INotificationChannel
    {
        void Publish(string message);

        string Handle(string message);
    }

    public interface IRestaurantConnector
    {
        INotificationChannel ConnectTo(int restaurantId);
    }

}
