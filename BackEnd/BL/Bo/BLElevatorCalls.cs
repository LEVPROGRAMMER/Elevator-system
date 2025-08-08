

public class BLElevatorCalls
{
    public int Id { get; set; }
    public int? BuildingId { get; set; }
    public int? RequestedFloor { get; set; }
    public int? DestinationFloor { get; set; }
    public DateTime? CallTime { get; set; }
    public bool? IsHandled { get; set; }

    public bool IsPending() => !IsHandled.GetValueOrDefault();
}

