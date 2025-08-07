//using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNetCore.SignalR;

//public class BLElevatorBackgroundService : Hub
//{
//    public async Task SendElevatorUpdate(string elevatorId, int currentFloor, string status, string direction, string doorStatus)
//    {
//        // שידור העדכון לכל הלקוחות המחוברים
//        await Clients.All.SendAsync("ReceiveElevatorUpdate", elevatorId, currentFloor, status, direction, doorStatus);
//    }
//}
