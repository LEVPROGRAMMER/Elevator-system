using System;
using System.Collections.Generic;

namespace Dal.Do;

public partial class TargetFloor
{
    public int Id { get; set; }

    public int? ElevatorId { get; set; }

    public int? Floor { get; set; }

    public int? Position { get; set; }

    public virtual Elevator? Elevator { get; set; }
}
