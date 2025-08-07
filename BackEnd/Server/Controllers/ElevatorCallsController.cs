using BL;
using BL.BlApi;
using BL.Bo;
using Dal;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorCallsController : ControllerBase
    {
        IBLElevatorCall IblelevatorCall;
        public ElevatorCallsController(BLManager blManager)
        {
            this.IblelevatorCall = blManager.BLElevatorCall;
        }

        [Route("GetAllElevatorCall")]
        [HttpGet]
        public List<BLElevatorCalls> GetElevatorCallList()
        {
           return IblelevatorCall.ReadAll();
        }

        [Route("AddElevatorCall/ElevatorCall")]
        [HttpPost()]
        public bool Create(BLElevatorCalls elevatorCall) =>
           
            IblelevatorCall.Create(elevatorCall);

        [Route("DeleteElevatorCall/Id")]
        [HttpDelete()]
        public bool Delete(int Id) =>
            IblelevatorCall.Delete(Id);

        [Route("UpDateElevatorCall/{id}")]
        [HttpPut()]
        public bool UpDate(int id, string fileName , object value) =>
        IblelevatorCall.Update(id, fileName, value);

        [Route("GetElevatorCallById/Id")]
        [HttpGet()]
        public BLElevatorCalls GetElevatorCallById(int Id) =>
            IblelevatorCall.Read(Id);
    }
}
