using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bo;

namespace BL.BlApi
{
    public interface IBLElevatorCall
    {
        BLElevatorCalls Read(int filter);
        public List<BLElevatorCalls> ReadAll();

        public bool Create(BLElevatorCalls item);

        public bool Delete(int code);
        public bool Update(int id, string filename, object value);


    }
}
