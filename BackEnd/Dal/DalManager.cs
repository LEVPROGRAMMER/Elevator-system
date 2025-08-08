using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.DalImplementation;
using Dal.Do;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public class DalManager
    {
        public IElevator Elevator { get; set; }
        public IUser User { get; set; }
        public IElevatorCall ElevatorCall { get; set; }

        public IBuilding Building { get; set; }
        public IElevatorCallAssignment ElevatorCallAssignment { get; set; }
         public ITargetFloor TargetFloor { get; set; }
        public DalManager()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddSingleton<dbcontext>();
            collection.AddSingleton<IElevator, ElevatorService>();
            collection.AddSingleton<IUser, UserService>();
            collection.AddSingleton<IBuilding,BuildingService>();
            collection.AddSingleton<IElevatorCallAssignment,ElevatorCallAssignmentService>();
            collection.AddSingleton<IElevatorCall,ElevatorCallService>();
            collection.AddSingleton<ITargetFloor,TargetFloorService>();


            var serviceprovider = collection.BuildServiceProvider();
            Elevator = serviceprovider.GetRequiredService<IElevator>();
            User = serviceprovider.GetRequiredService<IUser>();
            Building = serviceprovider.GetRequiredService<IBuilding>();
            ElevatorCallAssignment = serviceprovider.GetRequiredService<IElevatorCallAssignment>();
            ElevatorCall = serviceprovider.GetRequiredService<IElevatorCall>();
            TargetFloor = serviceprovider.GetRequiredService<ITargetFloor>();
        }
    }
}
