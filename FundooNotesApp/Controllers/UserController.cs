using Businesslayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Registration(registration user1)
        {
            try
            {
                var result = userBL.Registration(user1);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Registration done successfully",
                        response = result
                    }) ;
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Registration unsuccessfull"
                    });
                    }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(login LoginModel)
        {
            try
            {
                var result = userBL.Login(LoginModel);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "User logged in successfully",
                        response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Login unsuccessfull"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost,]
        [Route("forgetPassword")]
        public IActionResult forgetPassword(string email)
        {
            try
            {
                var result = userBL.forgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Email sent successfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Email has not sent"
                    });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string password,string confirmpassword)
        {
            try
            {
                var useremail = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = userBL.ResetPassword(useremail, password,  confirmpassword);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Password reset successfull",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Password reset unsuccessfull"
                    });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        }
}
