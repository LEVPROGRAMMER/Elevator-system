//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dal.Do;

//namespace BL.Bo
//{
//    public class BLElevator
//    {
//        public int Id { get; set; }

//        public int? BuildingId { get; set; }

//        public int? CurrentFloor { get; set; }

//        public BLElevatorStatus? Status { get; set; }

//        public BLElevatorDirection? Direction { get; set; }

//        public BLElevatorDoorStatus? DoorStatus { get; set; }
//        public List<BLTargetFloors>? TargetFloors { get; set; }

//    }
//}
using System.Linq;
using BL.Bo;

public class BLElevator
{
    public int Id { get; set; }
    public int? BuildingId { get; set; }
    public int? CurrentFloor { get; set; }
    public BLElevatorStatus? Status { get; set; }
    public BLElevatorDirection? Direction { get; set; }
    public BLElevatorDoorStatus? DoorStatus { get; set; }
    public List<BLTargetFloors> TargetFloors { get; set; } = new List<BLTargetFloors>(); // עדכון לרשימה של קומות יעד

    public void AddTargetFloor(int floor)
    {
        var targetFloor = new BLTargetFloors { Floor = floor };
        TargetFloors.Add(targetFloor);
    }


    public void Move()
    {
        if (!TargetFloors.Any()) return; // אם אין יעדים, אל תזז את המעלית

        int highestFloor = (int)TargetFloors.Max(target => target.Floor);
        int lowestFloor = (int)TargetFloors.Min(target => target.Floor);

        if (Status == BLElevatorStatus.MovingUp && CurrentFloor < highestFloor)
        {
            CurrentFloor++;
        }
        else if (Status == BLElevatorStatus.MovingDown && CurrentFloor > lowestFloor)
        {
            CurrentFloor--;
        }

        // בדוק אם הגענו ליעד
        if (TargetFloors.Any(target => target.Floor == (int)CurrentFloor))
        {
            Status = BLElevatorStatus.OpeningDoors;
            // טיפול בדלתות
        }
    }

}
