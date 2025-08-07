using BL.BlApi;
using BL.BlImplementation;
using BL.Bo;
using Dal;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    public class BLManager
    {
        public IBLUser BLUser { get; }
        public IBLBuilding BLBuilding { get; }
        public IBLElevator BLElevator { get; }
        public IBLElevatorCall BLElevatorCall { get; }
        public IBLElevatorCallAssignment BLElevatorCallAssignment { get; }



        public BLManager()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddSingleton<DalManager>();
            collection.AddSingleton<IBLUser, BLUserService>();
            collection.AddSingleton<IBLBuilding, BLBuildingService>();
            collection.AddSingleton<IBLElevator, BLElevatorService>();
            collection.AddSingleton<IBLElevatorCall, BLElevatorCallService>();
            collection.AddSingleton<IBLElevatorCallAssignment, BLElevatorCallAssignmentService>();


            var ServiceProvider = collection.BuildServiceProvider();
            BLUser = ServiceProvider.GetRequiredService<IBLUser>();
            BLBuilding = ServiceProvider.GetRequiredService<IBLBuilding>();
            BLElevator = ServiceProvider.GetRequiredService<IBLElevator>();
            BLElevatorCall = ServiceProvider.GetRequiredService<IBLElevatorCall>();
            BLElevatorCallAssignment = ServiceProvider.GetRequiredService<IBLElevatorCallAssignment>();


        }
    }
}
