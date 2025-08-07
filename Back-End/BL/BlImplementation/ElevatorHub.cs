using Microsoft.AspNet.SignalR;

public class ElevatorHub : Hub
{
    public async Task ReceiveElevatorUpdate(string elevatorId, int currentFloor, string status, string direction, string doorStatus)
    {
        await Clients.All.SendAsync("ReceiveElevatorUpdate", elevatorId, currentFloor, status, direction, doorStatus);
    }
}