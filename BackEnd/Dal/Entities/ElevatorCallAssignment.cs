using System;
using System.Collections.Generic;

namespace Dal.Do;

public partial class ElevatorCallAssignment
{
    public int Id { get; set; }

    public int? ElevatorId { get; set; }

    public DateTime? AssignmentTime { get; set; }

    public virtual Elevator? Elevator { get; set; }
}
