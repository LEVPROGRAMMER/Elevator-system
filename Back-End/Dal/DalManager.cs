using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Dal.DalImplementation;
using Dal.Do;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public class DalManager
    {
        public ElevatorService Elevator { get; set; }
        public UserService User { get; set; }
        public ElevatorCallService ElevatorCall { get; set; }

        public BuildingService Building { get; set; }
        public ElevatorCallAssignmentService ElevatorCallAssignment { get; set; }

        public DalManager()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddSingleton<dbcontext>();
            collection.AddSingleton<ElevatorService>();
            collection.AddSingleton<UserService>();
            collection.AddSingleton<BuildingService>();
            collection.AddSingleton<ElevatorCallAssignmentService>();
            collection.AddSingleton<ElevatorCallService>();


            var serviceprovider = collection.BuildServiceProvider();
            Elevator = serviceprovider.GetRequiredService<ElevatorService>();
            User = serviceprovider.GetRequiredService<UserService>();
            Building = serviceprovider.GetRequiredService<BuildingService>();
            ElevatorCallAssignment = serviceprovider.GetRequiredService<ElevatorCallAssignmentService>();
            ElevatorCall = serviceprovider.GetRequiredService<ElevatorCallService>();

        }
    }
}
