using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class ElevatorCallAssignmentService : IElevatorCallAssignment
    {
        private dbcontext db;
        public ElevatorCallAssignmentService(dbcontext db)
        {
            this.db = db;
        }

        public List<ElevatorCallAssignment> GetAll()
        {
            return db.ElevatorCallAssignments.ToList();
        }

        public bool Create(ElevatorCallAssignment item)
        {
            try
            {
                db.ElevatorCallAssignments.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool Delete(ElevatorCallAssignment item)
        {
            try
            {
                db.ElevatorCallAssignments.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool Update(ElevatorCallAssignment item)
        {
            try
            {
                int index = db.ElevatorCallAssignments.ToList().FindIndex(x => x.Id == item.Id);
                if (index == -1)
                    throw new Exception("ElevatorCallAssignment does not exist in DB");
                ElevatorCallAssignment eca = db.ElevatorCallAssignments.ToList()[index];
                eca.Id = item.Id;
                eca.ElevatorId = item.ElevatorId;
                eca.AssignmentTime = item.AssignmentTime;
                db.ElevatorCallAssignments.ToList()[index] = eca;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<ElevatorCallAssignment> Read(Predicate<ElevatorCallAssignment> filter)
        {
            return db.ElevatorCallAssignments.ToList().FindAll(x => filter(x));
        }

    }
}
