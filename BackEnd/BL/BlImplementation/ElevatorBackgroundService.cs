using BL;
using BL.BlApi;
using BL.Bo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ElevatorBackgroundService : BackgroundService
{
    private readonly ILogger<ElevatorBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IBLElevator _elevatorService;
    private readonly IBLElevatorCall _elevatorCallService;
    private readonly IHubContext<ElevatorHub> _hubContext;
    private int _interval;

    public ElevatorBackgroundService(
    ILogger<ElevatorBackgroundService> logger,
    IConfiguration configuration,
    BLManager blManager,
    IHubContext<ElevatorHub> hubContext)
    {
        _logger = logger;
        _configuration = configuration;
        _elevatorService = blManager.BLElevator;
        _elevatorCallService = blManager.BLElevatorCall;
        _hubContext = hubContext;
        _interval = int.Parse(_configuration["ElevatorService:IntervalInSeconds"]);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running elevator service...");
            var elevators = _elevatorService.ReadAll();
            var calls = _elevatorCallService.ReadAll()
    .Where(c => c.IsHandled == false)
    .ToList();
            foreach (var call in calls)
            {
                var elevator = elevators.FirstOrDefault(e => e.Status == BLElevatorStatus.Idle);
                if (elevator != null)
                {
                    ProcessCall(elevator, call);
                }
            }

            foreach (var elevator in elevators)
            {
                await MoveElevator(elevator);
            }

            await _hubContext.Clients.All.SendAsync("ReceiveElevatorUpdate", elevators);

            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
        }
    }

    private void ProcessCall(BLElevator elevator, BLElevatorCalls call)
    {
        elevator.AddTargetFloor(call.DestinationFloor.Value);
        call.IsHandled = true;
        _elevatorCallService.Update(call.Id, "IsHandled", true); 
    }

    private async Task MoveElevator(BLElevator elevator)
    {
        if (elevator.TargetFloors.Count > 0)
        {
            elevator.CurrentFloor = elevator.TargetFloors.First().Floor;
            elevator.TargetFloors.RemoveAt(0);
            elevator.Status = BLElevatorStatus.OpeningDoors;

            await Task.Delay(2000);
            elevator.DoorStatus = BLElevatorDoorStatus.Open;
            elevator.Status = BLElevatorStatus.Idle;
        }
        else
        {
            elevator.Status = BLElevatorStatus.Idle;
        }
    }
}

