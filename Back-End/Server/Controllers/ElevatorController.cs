using BL.BlApi;
using BL.Bo;
using BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dal.DalApi;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
       

            IBLElevator Iblelevator;
        public ElevatorController(BLManager blManager)
            {
                this.Iblelevator = blManager.BLElevator;
            }

            [Route("GetAllElevator")]
            [HttpGet]
            public List<BLElevator> GetElevatorList()
            {
                return Iblelevator.ReadAll();
            }

        //[Route("AddElevator")]
        //[HttpPost()]
        //public bool Create([FromBody] BLElevator elevator) =>
        //Iblelevator.Create(elevator);

        [Route("AddElevator")]
        [HttpPost()]
        public bool Create(BLElevator elevator) =>
               Iblelevator.Create(elevator);

        [Route("DeleteElevator/Id")]
            [HttpDelete()]
            public bool Delete(int Id) =>
                Iblelevator.Delete(Id);


            [Route("UpDateElevator/Id")]
            [HttpPut()]
            public bool UpDate(BLElevator elevator) =>
                Iblelevator.Update(elevator);

            [Route("GetElevatorById/Id")]
            [HttpGet()]
            public BLElevator GetElevatorById(int Id) =>
                Iblelevator.Read(Id);
        }
}

