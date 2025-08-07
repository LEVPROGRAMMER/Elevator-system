using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bo;
using Dal.Do;
using Dal;
using BL.BlApi;
using Dal.DalApi;

namespace BL.BlImplementation
{
    public class BLUserService : IBLUser
    {
        DalManager Dal;
        public BLUserService(DalManager manager)
        {
            this.Dal = manager;
        }

        public List<BLUser> ReadAll() =>
            CastListToBl(Dal.User.GetAll());


        public BLUser CastingToBl(User daluser)
        {
            BLUser u = new BLUser()
            {
                Id = daluser.Id,
                Email = daluser.Email,
                Password = daluser.Password,
            };
            return u;
        }

        public List<BLUser> CastListToBl(List<User> list)
        {
            List<BLUser> lst = new List<BLUser>();
            list.ForEach(x => lst.Add(CastingToBl(x)));
            return lst;
        }

        public User CastingToDal(BLUser bluser)
        {
            User u = new User()
            {
                Id = bluser.Id,
                Email = bluser.Email,
                Password = bluser.Password,
            };
            return u;
        }

        public bool Create(BLUser bluser)
        {
            return Dal.User.Create(CastingToDal(bluser));
        }

        public bool Delete(int id)
        {
            return Dal.User.Delete(Dal.User.GetAll().Find(x => x.Id == id));
        }

        public bool Update(BLUser bluser)
        {
            return Dal.User.Update(CastingToDal(bluser));
        }

        public BLUser Read(string password)
        {
            User temp = Dal.User.GetAll().Find(x => x.Password.Equals(password));
            if (temp == null)
                return null;
            return CastingToBl(temp);
        }
    }
}
