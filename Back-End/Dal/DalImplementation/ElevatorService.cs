using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class ElevatorService : IElevator
    {
        private dbcontext db;
        public ElevatorService(dbcontext db)
        {
            this.db = db;
        }

        public List<Elevator> GetAll()
        {
            return db.Elevators.ToList();
        }

        public bool Create(Elevator item)
        {
            try
            {
                db.Elevators.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool Delete(Elevator item)
        {
            try
            {
                db.Elevators.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool Update(Elevator item)
        {
            try
            {
                int index = db.Elevators.ToList().FindIndex(x => x.Id == item.Id);
                if (index == -1)
                    throw new Exception("Elevator does not exist in DB");
                Elevator e = db.Elevators.ToList()[index];
                e.Id = item.Id;
                e.BuildingId = item.BuildingId;
                e.Direction = item.Direction;
                e.DoorStatus = item.DoorStatus;
                e.CurrentFloor = item.CurrentFloor;
                e.Status = item.Status;
                db.Elevators.ToList()[index] = e;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Elevator> Read(Predicate<Elevator> filter)
        {
            return db.Elevators.ToList().FindAll(x => filter(x));
        }

    }
}
