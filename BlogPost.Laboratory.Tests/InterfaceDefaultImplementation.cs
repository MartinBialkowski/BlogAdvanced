using System;
using Xunit;

namespace BlogPost.Laboratory.Tests
{
    public class InterfaceDefaultImplementation
    {
        public void Test1()
        {
            var car = new Car();

            car.Ride();
            ((IVehicle)car).Ride();
            IVehicle car2 = new Car();
            car2.Ride();

            var bike = new Bike();
            ((IVehicle)bike).Ride();
            Console.WriteLine("END");

            Assert.True(false);
        }
    }

    public interface IVehicle
    {
        public void Ride()
        {
            System.Diagnostics.Debug.WriteLine("I'm riding vehicle");
        }
    }

    public class Car: IVehicle
    {
        public void Ride()
        {
            System.Diagnostics.Debug.WriteLine("Car");
        }
    }

    public class Bike: IVehicle
    {

    }
}
