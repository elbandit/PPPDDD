using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    // Factory used for example - not mandatory
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle()
        {
            var id = Guid.NewGuid();
            return new Vehicle(id);
        }
    }
    
    public class Vehicle
    {
        public Vehicle(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}
