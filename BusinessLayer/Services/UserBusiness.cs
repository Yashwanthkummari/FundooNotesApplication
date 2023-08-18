using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness   : IUserBusiness
    {
        private readonly IUserRepo _userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            _userRepo = userRepo;

        }

        public UserEntity UserReg(UserRegModel model)

        {
            try
            {
                return _userRepo.UserReg(model);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                return _userRepo.UserLogin(model);
            }
            catch (Exception )
            {
                throw ;
            }
        }
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                return _userRepo.ForgetPassword(model);
            }
            catch(Exception )
            {
                throw;
            }
        }
    }
}
