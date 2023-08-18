using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(UserRegModel model);
        public UserEntity UserLogin(UserLoginModel model);

    }
}
