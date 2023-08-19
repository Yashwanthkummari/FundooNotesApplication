﻿using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserReg(UserRegModel model);
        public string UserLogin(UserLoginModel model);
        public string ForgetPassword(ForgetPasswordModel model);
        public bool ResetPassword(ResetPasswordModel model);


    }
}
