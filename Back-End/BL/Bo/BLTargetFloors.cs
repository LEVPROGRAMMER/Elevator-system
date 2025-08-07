using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bo
{
    public class BLTargetFloors
    {
        public int Id { get; set; }

        public int? ElevatorId { get; set; }

        public int? Floor { get; set; }
        public int? Position { get; set; }

    }
}
