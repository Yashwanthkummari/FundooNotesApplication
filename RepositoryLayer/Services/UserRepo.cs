using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public string GenerateJwtToken(string Email, long UserId)
        { 
            var claims = new List<Claim>
            {

               new Claim("UserId", UserId.ToString()),
               new Claim("Email", Email)
             
                // Add any other claims you want to include in the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["JwtSettings:Issuer"], configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(15), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                var EmailValidity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (EmailValidity != null)
                {
                    string JwtToken = GenerateJwtToken(EmailValidity.Email, EmailValidity.UserId);
                    return JwtToken;
                }

                return null;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var EmailValidity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email);

                if (EmailValidity != null)
                {
                    var Token = GenerateJwtToken(EmailValidity.Email, EmailValidity.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(Token);
                    return Token;
                }
                else

                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(ResetPasswordModel model)
        {
            try
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    var Emailvalidity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email);
                    {
                        if (Emailvalidity != null)
                        {
                            Emailvalidity.Password = model.ConfirmPassword;
                            // Emailvalidity.Password = EncryptionDecryption.EncryptPassword(ConfirmPassword);
                            fundooContext.Users.Update(Emailvalidity);
                            fundooContext.SaveChanges();
                            return true;

                        }

                    }
                }
                return false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
