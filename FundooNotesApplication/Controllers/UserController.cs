using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Registration(UserRegModel model)
        {
            var result = userBusiness.UserReg(model);
            if (result != null)
            {
                return this.Ok(new { Succes = true, Message = "User registered Sucessfully", Data = result });
            }
            else
            {
                return this.BadRequest(new { Succes = false, Message = "User registation UnSucessfull", Data = result });

            }
        }
        [HttpPost]
        [Route("Login")]

        public IActionResult Login(UserLoginModel model)
        {
            var result =userBusiness.UserLogin(model);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "User Login Sucessfully" ,data =result});

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "User Login UnSucessfull" });

            }
        }
    }
}
