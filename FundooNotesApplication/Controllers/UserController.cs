using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using Model_Layer.Models;
using Business_Layer.Interfaces;
using Repository_Layer.Entity;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser_BL iuser_BL;
      //  private readonly ILogger<UserController> logger;
        public UserController(IUser_BL iuser_BL)
        {
            this.iuser_BL = iuser_BL;
            
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration_ML userRegistration)
        {
            try
            {
                var result = iuser_BL.UserRegistrations(userRegistration);
                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "User Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User Registration unsuccessufull" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin_ML userLogin)
        {
            try
            {
                var result = iuser_BL.UserLogins(userLogin);

                if (result != null)
                {
                   
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "Login successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Invalid login credentials" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("check-email-exists")]
        public IActionResult checkEmailExists(string email)
        {
            try
            {
                bool result = iuser_BL.IsEmailExists(email);

                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "email exists" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "email doesn't exist" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Get-all-ussers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserEntity> result = iuser_BL.GetAllUsers();

                if (result != null)
                {
                    return this.Ok(new Response_ML<List<UserEntity>> { Status = true, Message = "users retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "No user found" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Forget-Password")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                string result = iuser_BL.ForgetPassword(Email);
                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "forget password" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Reset-Password")] 
        public IActionResult ResetPassword(resetPassword_ML reset)
        {
            
            try
            {

                string email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var result = iuser_BL.ResetPassword(email, reset);
                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "reset password" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetUserByFirstName")]
        public IActionResult GetDetailsOfUser(string FirstName)
        {
            try
            {
                List<UserEntity> result = iuser_BL.GetDetailsOfUser(FirstName);
                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "Fetched Details" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Details Not fetched" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login-with-jwt")]
        public IActionResult LoginWithJwt(UserLogin_ML userLogin)
        {
            try
            {
                var result = iuser_BL.LoginWithJwt(userLogin);
                if (result != null)
                {
                    return this.Ok(new Response_ML<string> { Status = true, Message = "login successfully with token", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Fetch-User")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var result = iuser_BL.GetUserById(userId);

                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "fetched the data", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "didnt fetch the data" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("Update-User")]
        public IActionResult UpdateUser(long userId, UserRegistration_ML userRegistration)
        {
            try
            {
                bool result = iuser_BL.UpdateUser(userId, userRegistration);
                if (result != null)
                {
                    return this.Ok(new Response_ML<UserEntity> { Status = true, Message = "updated user successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update user" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

