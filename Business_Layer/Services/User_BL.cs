using Business_Layer.Interfaces;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class User_BL : IUser_BL
    {
        private readonly IUser_RL iuser_RL;

        public User_BL(IUser_RL iuser_RL)
        {
            this.iuser_RL = iuser_RL;
        }

        public UserEntity UserRegistrations(UserRegistration_ML userRegistration)
        {
            try
            {
                return this.iuser_RL.UserRegistrations(userRegistration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserEntity UserLogins(UserLogin_ML userLogin)
        {
            try
            {
                UserEntity userEntity = iuser_RL.UserLogins(userLogin);
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
            return iuser_RL.IsEmailExists(email);
        }

        public List<UserEntity> GetAllUsers()
        {
            return iuser_RL.GetAllUsers();
        }



        public string ForgetPassword(string Email)
        {
            return iuser_RL.ForgetPassword(Email);
        }

        public string GenerateToken(string Email, int UserId)
        {
            try
            {
                return iuser_RL.GenerateToken(Email, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, resetPassword_ML reset)
        {
            try
            {
                return iuser_RL.ResetPassword(email, reset);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserEntity> GetDetailsOfUser(string firstName)
        {
            try
            {
                return iuser_RL.GetDetailsOfUser(firstName);
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
                return iuser_RL.LoginWithJwt(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public UserTicket CreateTicketForPassword(string emailId, string token)
        //{
        //    try
        //    {
        //        return userInterfaceRL.CreateTicketForPassword(emailId, token);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public UserEntity GetUserById(int userId)
        {
            try
            {
                return iuser_RL.GetUserById(userId);
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
                return iuser_RL.UpdateUser(userId, userRegistration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
