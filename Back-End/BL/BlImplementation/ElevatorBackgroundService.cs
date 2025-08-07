//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Threading;
//////using System.Threading.Tasks;
//////using BL.BlImplementation;
//////using BL.Bo;
//////using Microsoft.AspNet.SignalR;
//////using Microsoft.Extensions.Configuration;
//////using Microsoft.Extensions.Hosting;
//////using Microsoft.Extensions.Logging;

//////public class ElevatorBackgroundService : BackgroundService
//////{
//////    private readonly ILogger<ElevatorBackgroundService> _logger;
//////    private readonly IConfiguration _configuration;
//////    private readonly BLElevatorService _elevatorService;
//////    private readonly BLElevatorCallService _elevatorCallService;
//////    private readonly IHubContext<ElevatorHub> _hubContext;
//////    private int _interval;

//////    public ElevatorBackgroundService(ILogger<ElevatorBackgroundService> logger, IConfiguration configuration, BLElevatorService elevatorService, BLElevatorCallService elevatorCallService, IHubContext<ElevatorHub> hubContext)
//////    {
//////        _logger = logger;
//////        _configuration = configuration;
//////        _elevatorService = elevatorService;
//////        _elevatorCallService = elevatorCallService;
//////        _hubContext = hubContext;
//////        _interval = int.Parse(_configuration["ElevatorService:IntervalInSeconds"]);
//////    }

//////    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//////    {
//////        while (!stoppingToken.IsCancellationRequested)
//////        {
//////            _logger.LogInformation("Running elevator service...");

//////            // 1. טעינת מצב מעליות
//////            var elevators = _elevatorService.ReadAll();

//////            // 2. עיבוד קריאות ממתינות
//////            var calls = _elevatorCallService.ReadAll().Where(c => (bool)!c.IsHandled).ToList();
//////            foreach (var call in calls)
//////            {
//////                var elevator = elevators.FirstOrDefault(e => e.Status == BLElevatorStatus.Idle);
//////                if (elevator != null)
//////                {
//////                    // טיפול בקריאה
//////                    ProcessCall(elevator, call);
//////                }
//////            }

//////            // 3. קידום מעלית
//////            foreach (var elevator in elevators)
//////            {
//////                MoveElevator(elevator);
//////            }

//////            // 4. שליחת עדכונים ללקוח
//////            await _hubContext.Clients.All.SendAsync("ReceiveElevatorUpdate", elevators);

//////            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
//////        }
//////    }

//////    private void ProcessCall(BLElevator elevator, BLElevatorCalls call)
//////    {
//////        // לוגיקה לטיפול בקריאה
//////        call.TargetFloors.Add(call.DestinationFloor);
//////        call.IsHandled = true;
//////        _elevatorCallService.Update(call.Id, "IsHandled", 1); // עדכון שהקריאה טופלה
//////    }

//////    private async void MoveElevator(BLElevator elevator)
//////    {
//////        // לוגיקה לקידום המעלית
//////        if (elevator.TargetFloors.Count > 0)
//////        {
//////            elevator.CurrentFloor = elevator.TargetFloors.First();
//////            elevator.TargetFloors.RemoveAt(0);
//////            elevator.Status = BLElevatorStatus.OpeningDoors;

//////            await Task.Delay(2000); // דלתות פתוחות
//////            elevator.DoorStatus = BLElevatorDoorStatus.Open;
//////            elevator.Status = BLElevatorStatus.Idle;
//////        }
//////        else
//////        {
//////            elevator.Status = BLElevatorStatus.Idle;
//////        }
//////    }
//////}
////using BL.BlApi;
////using BL.BlImplementation;
////using BL.Bo;
////using Dal.Do;
////using Microsoft.AspNetCore.SignalR;
////using Microsoft.Extensions.Hosting;
////using Microsoft.Extensions.Logging;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading;
////using System.Threading.Tasks;

////public class ElevatorHub : Hub
////{
////    public async Task ReceiveElevatorUpdate(string elevatorId, int currentFloor, string status, string direction, string doorStatus)
////    {
////        await Clients.All.SendAsync("ReceiveElevatorUpdate", elevatorId, currentFloor, status, direction, doorStatus);
////    }
////}

////public class ElevatorService : BackgroundService
////{
////    private readonly IHubContext<ElevatorHub> _hubContext;
////    private readonly ILogger<BLElevatorService> _logger;
////    private List<BLElevator> _elevators;
////    private IBLElevator _Iblelevator;


////    public ElevatorService(IHubContext<ElevatorHub> hubContext, ILogger<BLElevatorService> logger)
////    {
////        _hubContext = hubContext;
////        _logger = logger;
////        _elevators = _Iblelevator.ReadAll();
////        //    new List<BLElevator>
////        //{
////        //    new BLElevator { Id = 8, CurrentFloor = 0, Status = BLElevatorStatus.Idle, Direction = BLElevatorDirection.None, DoorStatus = BLElevatorDoorStatus.Closed }
////        //};
////    }

////    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
////    {
////        while (!stoppingToken.IsCancellationRequested)
////        {
////            foreach (var elevator in _elevators)
////            {
////                // לוגיקת קידום מעלית
////                //UpdateElevator(elevator);
////                await _hubContext.Clients.All.SendAsync("ReceiveElevatorUpdate", elevator.Id, elevator.CurrentFloor, elevator.Status, elevator.Direction, elevator.DoorStatus);
////            }
////            await Task.Delay(20000, stoppingToken); // פעימה כל 20 שניות
////        }
////    }

////    private void UpdateElevator(BLElevator elevator, BLElevatorCalls calls)
////    {
////        // לוגיקת קידום מעלית
////        if (elevator.Status == BLElevatorStatus.MovingUp)
////        {
////            elevator.CurrentFloor++;
////            if (elevator.CurrentFloor == calls.DestinationFloor) // דוגמה: אם הגיעה לקומה 5
////            {
////                elevator.Status = BLElevatorStatus.OpeningDoors;
////                elevator.DoorStatus = BLElevatorDoorStatus.Open;
////                // טיפול בדלתות
////                Thread.Sleep(2000); // דלתות פתוחות ל-2 שניות
////                elevator.DoorStatus = BLElevatorDoorStatus.Closed;
////                elevator.Status = BLElevatorStatus.Idle;
////            }
////        }
////        else if (elevator.Status == BLElevatorStatus.MovingDown)
////        {
////            elevator.CurrentFloor--;
////            if (elevator.CurrentFloor == calls.DestinationFloor) // דוגמה: אם הגיעה לקומה 0
////            {
////                elevator.Status = BLElevatorStatus.OpeningDoors;
////                elevator.DoorStatus = BLElevatorDoorStatus.Open;
////                // טיפול בדלתות
////                Thread.Sleep(2000); // דלתות פתוחות ל-2 שניות
////                elevator.DoorStatus = BLElevatorDoorStatus.Closed;
////                elevator.Status = BLElevatorStatus.Idle;
////            }
////        }
////    }
////}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//public class ElevatorService : BackgroundService
//{
//    private readonly ILogger<ElevatorService> _logger;
//    private readonly IOptions<ElevatorSettings> _settings;
//    private readonly ElevatorManager _elevatorManager; // ניהול המעליות

//    public ElevatorService(ILogger<ElevatorService> logger, IOptions<ElevatorSettings> settings, ElevatorManager elevatorManager)
//    {
//        _logger = logger;
//        _settings = settings;
//        _elevatorManager = elevatorManager;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            _logger.LogInformation("Running elevator service...");

//            // טעינת מצב מעליות
//            await _elevatorManager.LoadElevatorStatesAsync();

//            // עיבוד קריאות ממתינות
//            await ProcessElevatorCallsAsync();

//            // קידום מעלית
//            await UpdateElevatorsAsync();

//            // שליחת עדכונים ללקוח
//            await SendUpdatesToClientsAsync();

//            // המתנה לפני הפעימה הבאה
//            await Task.Delay(_settings.Value.Interval, stoppingToken);
//        }
//    }

//    private async Task ProcessElevatorCallsAsync()
//    {
//        var calls = await _elevatorManager.GetPendingCallsAsync();
//        foreach (var call in calls)
//        {
//            var elevator = _elevatorManager.GetAvailableElevator();
//            if (elevator != null)
//            {
//                elevator.ProcessCall(call);
//            }
//        }
//    }

//    private async Task UpdateElevatorsAsync()
//    {
//        foreach (var elevator in _elevatorManager.GetAllElevators())
//        {
//            await elevator.UpdatePositionAsync();
//        }
//    }

//    private async Task SendUpdatesToClientsAsync()
//    {
//        // שלח עדכונים ללקוחות באמצעות SignalR
//        await ElevatorHub.ReceiveElevatorUpdateAsync(_elevatorManager.GetElevatorStates());
//    }
//}

//public class ElevatorSettings
//{
//    public int Interval { get; set; } // זמן בפעימות
//}




using BL.BlImplementation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ElevatorService : BackgroundService
{
    private readonly ILogger<BLElevatorService> _logger;
    private readonly IHubContext<ElevatorHub> _hubContext;
    private readonly ElevatorSettings _settings;
    private readonly IRepository _repository; // Assume you have an interface for data access
    private readonly List<BLElevator> _elevators; // Assume this holds the current state of elevators

    public ElevatorService(
        ILogger<BLElevatorService> logger,
        IHubContext<ElevatorHub> hubContext,
        IOptions<ElevatorSettings> settings,
        IRepository repository)
    {
        _logger = logger;
        _hubContext = hubContext;
        _settings = settings.Value;
        _repository = repository;
        _elevators = new List<BLElevator>(); // Initialize your elevators
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_settings.IntervalMilliseconds, stoppingToken); // Delay based on configuration

            LoadElevatorStates();
            ProcessPendingCalls();
            UpdateElevators();
            await SendUpdatesToClients();
        }
    }

    private void LoadElevatorStates()
    {
        // Load or refresh the state of elevators from the database or memory
        _elevators = _repository.GetElevators(); // Replace with actual data fetching logic
    }

    private void ProcessPendingCalls()
    {
        var pendingCalls = _repository.GetPendingCalls();

        foreach (var call in pendingCalls)
        {
            foreach (var elevator in _elevators)
            {
                if (elevator.Status.IsIdle())
                {
                    elevator.CallElevator(call.Floor);
                }
                else if (elevator.IsMoving())
                {
                    if (elevator.CanAddDestination(call.Floor))
                    {
                        elevator.AddDestination(call.Floor);
                    }
                    else
                    {
                        _repository.AddToPendingQueue(call);
                    }
                }
            }
        }
    }

    private void UpdateElevators()
    {
        foreach (var elevator in _elevators)
        {
            if (elevator.IsMovingUp())
            {
                elevator.CurrentFloor++;
            }
            else if (elevator.IsMovingDown())
            {
                elevator.CurrentFloor--;
            }

            if (elevator.TargetFloors.Any(target => target.Floor == elevator.CurrentFloor))
            {
                elevator.Status = ElevatorStatus.OpeningDoors;
                // Start door timer logic here
                // After timer expires:
                elevator.DoorStatus = DoorStatus.Open;
                // Then transition to closing doors, etc.
            }

            // Update direction and status if needed
            if (!elevator.TargetFloors.Any())
            {
                elevator.Direction = ElevatorDirection.None;
                elevator.Status = ElevatorStatus.Idle;
            }
        }
    }

    private async Task SendUpdatesToClients()
    {
        foreach (var elevator in _elevators)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveElevatorUpdate", elevator);
        }
    }
}

public class ElevatorSettings
{
    public int IntervalMilliseconds { get; set; }
}

