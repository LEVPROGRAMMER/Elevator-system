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
