using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BL.BlImplementation;
using BL.Bo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ElevatorBackgroundService : BackgroundService
{
    private readonly ILogger<ElevatorBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private readonly BLElevatorService _elevatorService;
    private readonly BLElevatorCallService _elevatorCallService;
    private readonly IHubContext<ElevatorHub> _hubContext;
    private int _interval;

    public ElevatorBackgroundService(ILogger<ElevatorBackgroundService> logger, IConfiguration configuration, BLElevatorService elevatorService, BLElevatorCallService elevatorCallService, IHubContext<ElevatorHub> hubContext)
    {
        _logger = logger;
        _configuration = configuration;
        _elevatorService = elevatorService;
        _elevatorCallService = elevatorCallService;
        _hubContext = hubContext;
        _interval = int.Parse(_configuration["ElevatorService:IntervalInSeconds"]);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running elevator service...");

            // 1. טעינת מצב מעליות
            var elevators = _elevatorService.ReadAll();

            // 2. עיבוד קריאות ממתינות
            var calls = _elevatorCallService.ReadAll()
    .Where(c => c.IsHandled == false)
    .ToList();
            foreach (var call in calls)
            {
                var elevator = elevators.FirstOrDefault(e => e.Status == BLElevatorStatus.Idle);
                if (elevator != null)
                {
                    // טיפול בקריאה
                    ProcessCall(elevator, call);
                }
            }

            // 3. קידום מעלית
            foreach (var elevator in elevators)
            {
                await MoveElevator(elevator);
            }

            // 4. שליחת עדכונים ללקוח
            await _hubContext.Clients.All.SendAsync("ReceiveElevatorUpdate", elevators);

            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
        }
    }

    private void ProcessCall(BLElevator elevator, BLElevatorCalls call)
    {
        // לוגיקה לטיפול בקריאה
        elevator.AddTargetFloor(call.DestinationFloor.Value);
        call.IsHandled = true;
        _elevatorCallService.Update(call.Id, "IsHandled", true); // עדכון שהקריאה טופלה
    }

    private async Task MoveElevator(BLElevator elevator)
    {
        // לוגיקה לקידום המעלית
        if (elevator.TargetFloors.Count > 0)
        {
            elevator.CurrentFloor = elevator.TargetFloors.First();
            elevator.TargetFloors.RemoveAt(0);
            elevator.Status = BLElevatorStatus.OpeningDoors;

            await Task.Delay(2000); // דלתות פתוחות
            elevator.DoorStatus = BLElevatorDoorStatus.Open;
            elevator.Status = BLElevatorStatus.Idle;
        }
        else
        {
            elevator.Status = BLElevatorStatus.Idle;
        }
    }
}

