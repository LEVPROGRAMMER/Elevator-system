using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class UserService:IUser
    {
        private dbcontext db;
        public UserService(dbcontext db)
        {
            this.db = db;
        }

        public bool Create(User item)
        {
            try
            {
                db.Users.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.Users.Remove(item);
                return false; }
        }

        public bool Delete(User item)
        {
            try
            {
                db.Users.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();

        }
   
        public List<User> Read(Predicate<User> filter)
        {
            return db.Users.ToList().FindAll(x => filter(x));
        }

        public bool Update(User item)
        {
            try
            {
                int index = db.Users.ToList().FindIndex(x => x.Id == item.Id);
                if (index == -1)
                    throw new Exception("User does not exist in DB");
                User u = db.Users.ToList()[index];
                u.Id = item.Id;
                u.Email = item.Email;
                u.Password = item.Password;
                db.Users.ToList()[index] = u;
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
