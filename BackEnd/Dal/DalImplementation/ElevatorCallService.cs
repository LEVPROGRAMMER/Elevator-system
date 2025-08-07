using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class ElevatorCallService : IElevatorCall
    {

        private dbcontext db;
        public ElevatorCallService(dbcontext db)
        {
            this.db = db;
        }

        public bool Create(ElevatorCall item)
        {

            try
            {
                db.ElevatorCalls.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.ElevatorCalls.Remove(item);
                return false;
            }
        }
        public bool Delete(ElevatorCall item)
        {
            try
            {
                db.ElevatorCalls.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public List<ElevatorCall> GetAll()
        {
            return db.ElevatorCalls.ToList();
        }

        public List<ElevatorCall> Read(Predicate<ElevatorCall> filter)
        {
            return db.ElevatorCalls.ToList().FindAll(x => filter(x));
        }
        public bool Update(int id, string fieldName, object value)
        {
            try
            {
                var ec = db.ElevatorCalls.FirstOrDefault(x => x.Id == id);
                if (ec == null)
                    throw new Exception("ElevatorCalls does not exist in DB");

                switch (fieldName.ToLower())
                {
                    case "buildingid":
                        ec.BuildingId = Convert.ToInt32(value);
                        break;
                    case "requestedfloor":
                        ec.RequestedFloor = (int?)value;
                        break;
                    case "destinationfloor":
                        ec.DestinationFloor = (int?)(value);
                        break;
                    case "calltime":
                        ec.CallTime = Convert.ToDateTime(value);
                        break;
                    case "ishandled":
                        ec.IsHandled = Convert.ToBoolean(value);
                        break;
                    default:
                        throw new Exception("Invalid field name");
                }

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

