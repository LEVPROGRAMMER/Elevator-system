using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Do;

namespace BL.Bo
{
    public class BLElevator
    {
        public int Id { get; set; }

        public int? BuildingId { get; set; }

        public int? CurrentFloor { get; set; }

        public BLElevatorStatus? Status { get; set; }

        public BLElevatorDirection? Direction { get; set; }

        public BLElevatorDoorStatus? DoorStatus { get; set; }

    }
}
