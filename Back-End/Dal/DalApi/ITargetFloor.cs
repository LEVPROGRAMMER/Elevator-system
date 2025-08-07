using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Do;

namespace Dal.DalApi
{
    public interface ITargetFloor
    {
        //public List<TargetFloor> GetAll();

        //public List<TargetFloor> Read(int id);
        bool Create(int value, int id);
    }
}
