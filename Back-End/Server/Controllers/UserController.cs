using BL.BlApi;
using BL.Bo;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IBLUser IblUser;
        public UserController(BLManager blManager)
        {
            this.IblUser = blManager.BLUser;
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public List<BLUser> GetUserList()
        {
            return IblUser.ReadAll();
        }

        [Route("AddUser/User")]
        [HttpPost()]
        public bool Create(BLUser user) =>
          IblUser.Create(user);

        [Route("DeleteUser/Id")]
        [HttpDelete()]
        public bool Delete(int Id) =>
            IblUser.Delete(Id);


        [Route("UpDateUser/Id")]
        [HttpPut()]
        public bool UpDate(BLUser user) =>
           IblUser.Update(user);

        [Route("GetUserById/Id")]
        [HttpGet()]
        public BLUser GetUserById(int Id) =>
            IblUser.Read(Id);
    }
}
