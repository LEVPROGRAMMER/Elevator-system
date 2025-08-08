using BL.BlApi;
using BL.Bo;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal.Do;
using Dal.DalApi;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {

            IBLBuilding Iblbuilding;
            public BuildingController(BLManager blManager)
            {
                this.Iblbuilding = blManager.BLBuilding;
            }

            [Route("GetAllBuilding")]
            [HttpGet]
            public List<BLBuilding> GetBuildingList()
            {
                return Iblbuilding.ReadAll();
            }

        [Route("AddBuilding/Building")]
            [HttpPost()]
            public bool Create(BLBuilding building) =>
               Iblbuilding.Create(building);

            [Route("DeleteBuilding/Id")]
            [HttpDelete()]
            public bool Delete(int Id) =>
                Iblbuilding.Delete(Id);


            [Route("UpDateBuilding/Id")]
            [HttpPut()]
            public bool UpDate(BLBuilding building) =>
                Iblbuilding.Update(building);

            [Route("GetBuildingByUser/Id")]
            [HttpGet()]
            public List<BLBuilding> GetBuildingById(int Id) =>
                Iblbuilding.Read(Id);
        }
    
}
