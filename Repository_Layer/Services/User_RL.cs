using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class User_RL : IUser_RL
    {
        private readonly FundooDB_Context fundoodbContext;
        private readonly IConfiguration configuration;

        public User_RL(FundooDB_Context fundoodbContext, IConfiguration configuration)
        {
            this.fundoodbContext = fundoodbContext;
            this.configuration = configuration;
        }

        public UserEntity UserRegistrations(UserRegistration_ML userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.Email = userRegistration.Email;
                userEntity.Password = EncryptPass(userRegistration.Password);
                fundoodbContext.Users.Add(userEntity);
                int result = fundoodbContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string EncryptPass(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


        public UserEntity UserLogins(UserLogin_ML userLogin)
        {
            try
            {
                UserEntity userEntity = fundoodbContext.Users.FirstOrDefault(a => a.Email == userLogin.Email);
                UserEntity userEntityy = fundoodbContext.Users.FirstOrDefault(a => a.Password == userLogin.Password);

                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool IsEmailExists(string email)
        {
            return fundoodbContext.Users.Any(u => u.Email == email);
        }

        public List<UserEntity> GetAllUsers()
        {
            return fundoodbContext.Users.ToList();
        }



        public string GenerateToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim("Email", Email),
               new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials) ;


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string ForgetPassword(string EmailId)
        {
            try
            {
                var result = fundoodbContext.Users.FirstOrDefault(x => x.Email == EmailId);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    MSMQ_ML mSMQModel = new MSMQ_ML();
                    mSMQModel.SendMessage(token, result.Email, result.FirstName);
                    return token.ToString();
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

        public bool ResetPassword(string Email, resetPassword_ML reset)
        {
            var password = EncryptPass(reset.Password);
            var confirmPassword= EncryptPass(reset.ConfirmPassword);
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var user = fundoodbContext.Users.Where(x => x.Email == Email).FirstOrDefault();
                    user.Password = EncryptPass(reset.ConfirmPassword); 
                    fundoodbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //take input of username and print the details of that user. else throw msg user does not exist
        public List<UserEntity> GetDetailsOfUser(string firstName)
        {
            try
            {
                List<UserEntity> userEntity = fundoodbContext.Users.Where(x => x.FirstName == firstName).ToList();
                return userEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string LoginWithJwt(UserLogin_ML userLogin)
        {
            try
            {
                var EncodedPassword = EncryptPass(userLogin.Password);
                UserEntity result = fundoodbContext.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == EncodedPassword);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

       

        public UserEntity GetUserById(int userId)
        {
            try
            {
                UserEntity userEntity = fundoodbContext.Users.FirstOrDefault(x => x.UserId == userId);
                return userEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(long userId, UserRegistration_ML userRegistration)
        {
            try
            {
                var result = fundoodbContext.Users.FirstOrDefault(x => x.UserId == userId);
                if (result != null)
                {
                    result.FirstName = userRegistration.FirstName;
                    result.LastName = userRegistration.LastName;
                    result.Email = userRegistration.Email;
                    result.Password = userRegistration.Password;
                    fundoodbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


