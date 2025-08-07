using Microsoft.AspNetCore.SignalR;


public class ElevatorHub : Hub
{
    public async Task ReceiveElevatorUpdate(BLElevator elevator)
    {
        await Clients.All.SendAsync("ReceiveElevatorUpdate", elevator);
    }
}