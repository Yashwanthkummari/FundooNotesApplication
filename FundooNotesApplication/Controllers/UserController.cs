using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBusiness userBusiness;
        private readonly FundooContext _fundooContext;
        public UserController(IUserBusiness userBusiness, FundooContext fundooContext)
        {
            this.userBusiness = userBusiness;
            this._fundooContext = fundooContext;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Registration(UserRegModel model)
        {
            var result = userBusiness.UserReg(model);
            if (result != null)
            {
                return this.Ok(new { Succes = true, Message = "User registered Successfully", Data = result });
            }
            else
            {
                return this.BadRequest(new { Succes = false, Message = "User registration UnSuccessfull", Data = result });

            }
        }
        [HttpPost]
        [Route("Login")]

        public IActionResult Login(UserLoginModel model)
        {
            var result =userBusiness.UserLogin(model);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "User Login Successfully" ,data =result});

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "User Login UnSuccessfull" });

            }
        }
        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {


            var result = userBusiness.ForgetPassword(model);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Token sent Successfully to  Updated password " });

            }
            else
            {
                return NotFound(new { Success = false, Message = "Token not sent" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var email = User.FindFirst(x => x.Type == "Email").Value;
            if (email != null)
            {
                var result = userBusiness.ResetPassword( model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Password Reseted Sucessfully" });
                }
                else
                {
                    return NotFound(new { success = false, message = "Password reset not successful" });
                }
            }
            return null;
        }


    }
}
