using Dal.DalApi;
using Dal.DalImplementation;
using Dal.Do;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

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

            collection.AddScoped<IDbConnection>(provider =>
    new SqlConnection("Data Source=DESKTOP-4BA570G\\SQLEXPRESS;Initial Catalog=debts_system;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

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
