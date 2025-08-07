using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bo;

namespace BL.BlApi
{
    public interface IBLBuilding : IBLcrud<BLBuilding>
    {
        List<BLBuilding >Read(int filter);

    }
}
