using System;
using System.Collections.Generic;

namespace Server.Do;

public partial class Building
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public int? NumberOfFloors { get; set; }

    public virtual ICollection<ElevatorCall> ElevatorCalls { get; set; } = new List<ElevatorCall>();

    public virtual ICollection<Elevator> Elevators { get; set; } = new List<Elevator>();

    public virtual User? User { get; set; }
}
