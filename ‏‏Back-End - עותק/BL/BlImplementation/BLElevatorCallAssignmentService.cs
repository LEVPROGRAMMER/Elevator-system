using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BlApi;
using BL.Bo;
using Dal;
using Dal.Do;

namespace BL.BlImplementation
{
    public class BLElevatorCallAssignmentService : IBLElevatorCallAssignment
    {

        DalManager Dal;
        public BLElevatorCallAssignmentService(DalManager manager)
        {
            this.Dal = manager;
        }


        public List<BLElevatorCallAssignment> ReadAll() =>
            CastListToBl(Dal.ElevatorCallAssignment.GetAll());


        public BLElevatorCallAssignment CastingToBl(ElevatorCallAssignment dalelevatorcallassignment)
        {
            BLElevatorCallAssignment eca = new BLElevatorCallAssignment()
            {
                Id = dalelevatorcallassignment.Id,
                ElevatorId = dalelevatorcallassignment.ElevatorId,
                AssignmentTime = dalelevatorcallassignment.AssignmentTime
            };
            return eca;
        }

        public List<BLElevatorCallAssignment> CastListToBl(List<ElevatorCallAssignment> list)
        {
            List<BLElevatorCallAssignment> lst = new List<BLElevatorCallAssignment>();
            list.ForEach(x => lst.Add(CastingToBl(x)));
            return lst;
        }

        public ElevatorCallAssignment CastingToDal(BLElevatorCallAssignment blelevatorcallassignment)
        {
            ElevatorCallAssignment eca = new ElevatorCallAssignment()
            {
                Id = blelevatorcallassignment.Id,
                ElevatorId = blelevatorcallassignment.ElevatorId,
                AssignmentTime = blelevatorcallassignment.AssignmentTime
            };
            return eca;
        }

        public bool Create(BLElevatorCallAssignment blelevatorcallassignment)
        {
            return Dal.ElevatorCallAssignment.Create(CastingToDal(blelevatorcallassignment));
        }

        public bool Delete(int id)
        {
          return  Dal.ElevatorCallAssignment.Delete(Dal.ElevatorCallAssignment.GetAll().Find(x => x.Id == id));
        }

        public bool Update(BLElevatorCallAssignment blelevatorcallassignment)
        {
            return Dal.ElevatorCallAssignment.Update(CastingToDal(blelevatorcallassignment));
        }

        public BLElevatorCallAssignment Read(int filter)
        {
            ElevatorCallAssignment temp = Dal.ElevatorCallAssignment.GetAll().Find(x => x.Id == filter);
            if (temp == null)
                return null;
            return CastingToBl(temp);
        }
    }
}

