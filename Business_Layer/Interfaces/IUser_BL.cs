using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IUser_BL
    {
        public UserEntity UserRegistrations(UserRegistration_ML userRegistration);
        public UserEntity UserLogins(UserLogin_ML userLogin);
        public bool IsEmailExists(string email);
        public List<UserEntity> GetAllUsers();

        public string ForgetPassword(string Email);
        public string GenerateToken(string Email, int UserId);
        public bool ResetPassword(string email, resetPassword_ML reset);
        public List<UserEntity> GetDetailsOfUser(string firstName);
        public string LoginWithJwt(UserLogin_ML userLogin);
        public UserEntity GetUserById(int userId);
        public bool UpdateUser(long userId, UserRegistration_ML userRegistration);

    }
}
