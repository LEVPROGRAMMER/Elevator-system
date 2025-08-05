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
                return false; }
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

            public bool Update(ElevatorCall item)
            {
                try
                {
                    int index = db.ElevatorCalls.ToList().FindIndex(x => x.Id == item.Id);
                    if (index == -1)
                        throw new Exception("ElevatorCalls does not exist in DB");
                    ElevatorCall ec = db.ElevatorCalls.ToList()[index];
                    ec.Id = item.Id;
                    ec.BuildingId = item.BuildingId;
                    ec.RequestedFloor = item.RequestedFloor;
                    ec.DestinationFloor = item.DestinationFloor;
                    ec.CallTime = item.CallTime;
                    ec.IsHandled = item.IsHandled;
                    db.ElevatorCalls.ToList()[index] = ec;
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



