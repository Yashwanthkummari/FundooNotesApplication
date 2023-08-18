using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public UserRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;

        }
        public UserEntity UserReg(UserRegModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.DateOfBirth = model.DateOfBirth;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;

                fundooContext.Users.Add(userEntity);

                fundooContext.SaveChanges();

                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public UserEntity UserLogin(UserLoginModel model)
        {
            try
            {

                var userEntity= fundooContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (userEntity != null)
                {
                    return userEntity;  
                }

                return null;

            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
