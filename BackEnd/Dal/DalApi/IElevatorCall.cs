using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Do;

namespace Dal.DalApi
{
    public interface IElevatorCall
    {
        public List<ElevatorCall> GetAll();

        public List<ElevatorCall> Read(Predicate<ElevatorCall> filter);

        public bool Create(ElevatorCall item);

        public bool Delete(ElevatorCall item);

        public bool Update(int id, string fieldName, object value);
    }
}
