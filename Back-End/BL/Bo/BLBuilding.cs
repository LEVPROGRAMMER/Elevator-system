using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Do;

namespace BL.Bo
{
    public class BLBuilding
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? Name { get; set; }

        public int? NumberOfFloors { get; set; }

    }
}
