using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class BuildingService : IBuilding
    {
        private dbcontext db;
        public BuildingService(dbcontext db)
        {
            this.db = db;
        }

        public bool Create(Building item)
        {
            try
            {
                db.Buildings.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool Delete(Building item)
        {
            try
            {
                db.Buildings.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.Buildings.Remove(item);
                return false; }
        }

        public List<Building> GetAll()
        {
            return db.Buildings.ToList();
        }

        public List<Building> Read(Predicate<Building> filter)
        {
            return db.Buildings.ToList().FindAll(x => filter(x));
        }

        public bool Update(Building item)
        {
            try
            {
                int index = db.Buildings.ToList().FindIndex(x => x.Id == item.Id);
                if (index == -1)
                    throw new Exception("Building does not exist in DB");
                Building b = db.Buildings.ToList()[index];
                b.Id = item.Id;
                b.Name = item.Name;
                b.NumberOfFloors = item.NumberOfFloors;
                b.UserId = item.UserId;
                db.Buildings.ToList()[index] = b;
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
