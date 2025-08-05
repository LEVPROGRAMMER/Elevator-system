using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bo;
using Dal.Do;
using Dal;
using BL.BlApi;

namespace BL.BlImplementation
{
    public class BLElevatorService : IBLElevator
    {
        DalManager Dal;
        public BLElevatorService(DalManager manager)
        {
            this.Dal = manager;
        }


        public List<BLElevator> ReadAll() =>
            CastListToBl(Dal.Elevator.GetAll());


        public BLElevator CastingToBl(Elevator dalelevator)
        {
            BLElevator e = new BLElevator()
            {
                Id = dalelevator.Id,
                BuildingId = dalelevator.BuildingId,
                CurrentFloor = dalelevator.CurrentFloor,
                Status = (BLElevatorStatus?)dalelevator.Status,
                Direction = (BLElevatorDirection?)dalelevator.Direction,
                DoorStatus = (BLElevatorDoorStatus?)dalelevator.DoorStatus
            };
            return e;
        }

        public List<BLElevator> CastListToBl(List<Elevator> list)
        {
            List<BLElevator> lst = new List<BLElevator>();
            list.ForEach(x => lst.Add(CastingToBl(x)));
            return lst;
        }

        public Elevator CastingToDal(BLElevator blelevator)
        {
            Elevator e = new Elevator()
            {
                Id = blelevator.Id,
                BuildingId = (int)blelevator.BuildingId,
                CurrentFloor = (int)blelevator.CurrentFloor,
                Status = (int)blelevator.Status,
                Direction= (int)blelevator.Direction,
                DoorStatus = (int)blelevator.DoorStatus
            };
            return e;
        }

        public bool Create(BLElevator blelevator)
        {
            return Dal.Elevator.Create(CastingToDal(blelevator));
        }

        public bool Delete(int id)
        {
            return Dal.Elevator.Delete(Dal.Elevator.GetAll().Find(x => x.Id == id));
        }

        public bool Update(BLElevator blelevator)
        {
            return Dal.Elevator.Update(CastingToDal(blelevator));
        }

        public BLElevator Read(int filter)
        {
            Elevator temp = Dal.Elevator.GetAll().Find(x => x.Id == filter);
            if (temp == null)
                temp = Dal.Elevator.GetAll().Find(x => x.BuildingId == filter);
                 if (temp == null)
                 return null;
            return CastingToBl(temp);
        }
    }
}
