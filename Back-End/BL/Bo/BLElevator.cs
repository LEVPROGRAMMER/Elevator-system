using BL.Bo;

public class BLElevator
{
    public int Id { get; set; }
    public int? BuildingId { get; set; }
    public int? CurrentFloor { get; set; }
    public BLElevatorStatus? Status { get; set; }
    public BLElevatorDirection Direction { get; set; }
    public BLElevatorDoorStatus DoorStatus { get; set; }
    public List<BLTargetFloors> TargetFloors { get; set; } = new List<BLTargetFloors>();

    public void AddTargetFloor(int floor)
    {
        TargetFloors.Add(new BLTargetFloors { Floor = floor });
    }

    public void Move()
    {
        if (!TargetFloors.Any()) return;

        int targetFloor = (int)TargetFloors.Min(tf => tf.Floor);
        if (CurrentFloor < targetFloor)
        {
            CurrentFloor++;
            Status = BLElevatorStatus.MovingUp;
        }
        else if (CurrentFloor > targetFloor)
        {
            CurrentFloor--;
            Status = BLElevatorStatus.MovingDown;
        }

        if (CurrentFloor == targetFloor)
        {
            TargetFloors.Remove(TargetFloors.First(tf => tf.Floor == targetFloor));
            Status = BLElevatorStatus.OpeningDoors;
        }
    }
}
