using BL.BlApi;
using BL.Bo;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorCallAssignmentController : ControllerBase
    {
        IBLElevatorCallAssignment IblelevatorCallAssignment;
        public ElevatorCallAssignmentController(BLManager blManager)
        {
            this.IblelevatorCallAssignment = blManager.BLElevatorCallAssignment;
        }

        [Route("GetAllElevatorCallAssignment")]
        [HttpGet]
        public List<BLElevatorCallAssignment> GetElevatorCallAssignmentList()
        {
            return IblelevatorCallAssignment.ReadAll();
        }

        [Route("addElevatorCallAssignment/ElevatorCallAssignment")]
        [HttpPost()]
        public bool Create(BLElevatorCallAssignment ElevatorCallAssignment) =>
           IblelevatorCallAssignment.Create(ElevatorCallAssignment);

        [Route("DeleteElevatorCallAssignment/Id")]
        [HttpDelete()]
        public bool Delete(int Id) =>
            IblelevatorCallAssignment.Delete(Id);


        [Route("UpDateElevatorCallAssignment/Id")]
        [HttpPut()]
        public bool UpDate(BLElevatorCallAssignment ElevatorCallAssignment) =>
            IblelevatorCallAssignment.Update(ElevatorCallAssignment);

        [Route("GetElevatorCallAssignmentById/Id")]
        [HttpGet()]
        public BLElevatorCallAssignment GetElevatorCallAssignmentById(int Id) =>
            IblelevatorCallAssignment.Read(Id);
    }
}
