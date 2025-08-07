//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BL.BlApi;
//using BL.Bo;
//using Dal;
//using Dal.Do;

//namespace BL.BlImplementation
//{
//    public class BLElevatorCallService: IBLElevatorCall
//    {

//        DalManager Dal;
//        public BLElevatorCallService(DalManager manager)
//        {
//            this.Dal = manager;
//        }

//        public List<BLElevatorCalls> ReadAll() =>
//                CastListToBl(Dal.ElevatorCall.GetAll());

//        public BLElevatorCalls CastingToBl(ElevatorCall dalelevatorcall)
//        {
//            BLElevatorCalls ec = new BLElevatorCalls()
//            {
//                Id = dalelevatorcall.Id,
//                BuildingId = dalelevatorcall.BuildingId,
//                RequestedFloor = dalelevatorcall.RequestedFloor,
//                DestinationFloor = dalelevatorcall.DestinationFloor,
//                CallTime = dalelevatorcall.CallTime,
//                IsHandled = dalelevatorcall.IsHandled,

//            };
//            return ec;
//        }

//        public List<BLElevatorCalls> CastListToBl(List<ElevatorCall> list)
//        {
//            List<BLElevatorCalls> lst = new List<BLElevatorCalls>();
//            list.ForEach(x => lst.Add(CastingToBl(x)));
//            return lst;
//        }

//        public ElevatorCall CastingToDal(BLElevatorCalls blelevatorcall)
//        {
//            ElevatorCall ec = new ElevatorCall()
//            {
//                Id = blelevatorcall.Id,
//                BuildingId = blelevatorcall.BuildingId,
//                RequestedFloor = blelevatorcall.RequestedFloor,
//                DestinationFloor = blelevatorcall.DestinationFloor,
//                CallTime = blelevatorcall.CallTime,
//                IsHandled = blelevatorcall.IsHandled
//            };
//            return ec;
//        }

//        public bool Create(BLElevatorCalls blelevatorcall)
//        {
//            return Dal.ElevatorCall.Create(CastingToDal(blelevatorcall));
//        }

//        public bool Delete(int elevatorcallId)
//        {
//            return Dal.ElevatorCall.Delete(Dal.ElevatorCall.GetAll().Find(x => x.Id == elevatorcallId));
//        }

//        public bool Update(int id , string filename, object value)
//        {
//            return Dal.ElevatorCall.Update(id,filename, value);
//        }

//        public BLElevatorCalls Read(int filter)
//        {
//            ElevatorCall temp = Dal.ElevatorCall.GetAll().Find(x => x.Id == filter);
//            if (temp == null)
//                return null;
//            return CastingToBl(temp);
//        }

//    }
//}
using BL.BlApi;
using Dal;
using Dal.Do;

public class BLElevatorCallService : IBLElevatorCall
{
    private readonly DalManager Dal;

    public BLElevatorCallService(DalManager manager)
    {
        this.Dal = manager;
    }
    public bool Create(BLElevatorCalls blelevatorcall)
    {
        bool result = Dal.ElevatorCall.Create(CastingToDal(blelevatorcall));
        if (result)
        {
            // שלח עדכון על קריאה חדשה
            _hubContext.Clients.All.SendAsync("ElevatorCallHandled", blelevatorcall);
        }
        return result;
    }

    public List<BLElevatorCalls> ReadAll() =>
        CastListToBl(Dal.ElevatorCall.GetAll());

    public List<BLElevatorCalls> GetPendingCalls() =>
        ReadAll().Where(call => call.IsPending()).ToList();

    public BLElevatorCalls CastingToBl(ElevatorCall dalelevatorcall)
    {
        return new BLElevatorCalls
        {
            Id = dalelevatorcall.Id,
            BuildingId = dalelevatorcall.BuildingId,
            RequestedFloor = dalelevatorcall.RequestedFloor,
            DestinationFloor = dalelevatorcall.DestinationFloor,
            CallTime = dalelevatorcall.CallTime,
            IsHandled = dalelevatorcall.IsHandled,
        };
    }

    public List<BLElevatorCalls> CastListToBl(List<ElevatorCall> list)
    {
        return list.Select(CastingToBl).ToList();
    }

    public ElevatorCall CastingToDal(BLElevatorCalls blelevatorcall)
    {
        return new ElevatorCall
        {
            Id = blelevatorcall.Id,
            BuildingId = blelevatorcall.BuildingId,
            RequestedFloor = blelevatorcall.RequestedFloor,
            DestinationFloor = blelevatorcall.DestinationFloor,
            CallTime = blelevatorcall.CallTime,
            IsHandled = blelevatorcall.IsHandled
        };
    }

    public bool Create(BLElevatorCalls blelevatorcall)
    {
        return Dal.ElevatorCall.Create(CastingToDal(blelevatorcall));
    }

    public bool Delete(int elevatorcallId)
    {
        return Dal.ElevatorCall.Delete(Dal.ElevatorCall.GetAll().Find(x => x.Id == elevatorcallId));
    }

    public bool Update(int id, string filename, object value)
    {
        return Dal.ElevatorCall.Update(id, filename, value);
    }

    public BLElevatorCalls Read(int filter)
    {
        ElevatorCall temp = Dal.ElevatorCall.GetAll().Find(x => x.Id == filter);
        return temp == null ? null : CastingToBl(temp);
    }
}
