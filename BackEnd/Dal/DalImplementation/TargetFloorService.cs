using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;
using Microsoft.EntityFrameworkCore;

namespace Dal.DalImplementation
{
    public class TargetFloorService : ITargetFloor
    {
       private dbcontext db;

        public TargetFloorService(dbcontext db)
        {
            this.db = db;

        }
       
        public List<TargetFloor> Read(Predicate<TargetFloor> filter)
        {
            return db.TargetFloors.ToList().FindAll(x => filter(x));
        }
        public List<TargetFloor> GetAll() => db.TargetFloors.ToList();

        public bool Create(int floor, int elevatorId)
        {

            try
            {
                TargetFloor item = new TargetFloor();
                item.ElevatorId = elevatorId;
                item.Floor = floor;
                int position = (int)(db.TargetFloors
                    .Where(tf => tf.ElevatorId == item.ElevatorId).Select(tf => tf.Position).DefaultIfEmpty(0).Max() + 1);

                item.Position = position; 
                db.TargetFloors.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(TargetFloor item)
        {
            try
            {
                db.TargetFloors.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.TargetFloors.Remove(item);
                return false;
            }
        }

        public bool Update(TargetFloor item)
        {
            try
            {
                int index = db.TargetFloors.ToList().FindIndex(x => x.Id == item.Id);
                if (index == -1)
                    throw new Exception("TargetFloor does not exist in DB");
                TargetFloor tf = db.TargetFloors.ToList()[index];
                tf.Id = item.Id;
                tf.ElevatorId = item.ElevatorId;
                tf.Floor = item.Floor;
                db.TargetFloors.ToList()[index] = tf;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
