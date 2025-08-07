using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bo;
using Dal;
using Dal.Do;

namespace BL.BlImplementation
{
    public class BLTargetFloorService
    {
        DalManager Dal;
        public BLTargetFloorService(DalManager manager)
        {
            this.Dal = manager;
        }
        public TargetFloor CastingToDal(BLTargetFloors bltargetfloor)
        {
            TargetFloor tf = new TargetFloor()
            {
                Id = bltargetfloor.Id,
                Position = bltargetfloor.Position,
                ElevatorId = bltargetfloor.ElevatorId,
                Floor = bltargetfloor.Floor
            };
            return tf;
        }
        public bool Create(BLTargetFloors bltargetfloor)
        {
            return Dal.TargetFloor.Create(CastingToDal(bltargetfloor));
        }
    }
}
