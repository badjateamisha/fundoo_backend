using CommonLayer.Model;
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

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    { 
        FundooContext fundooContext;

        private readonly IConfiguration config;

    public UserRL(FundooContext fundooContext, IConfiguration config)
    {
        this.fundooContext = fundooContext;
        this.config = config;
    }
    

        public UserEntity Registration(registration User)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = User.FirstName;
                userEntity.LastName = User.LastName;
                userEntity.Email = User.Email;
                userEntity.Password = User.Password;

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();

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
                return null; 
            }

        }

        public string Login(login loginModel)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    return token;
                }
            }
            catch (Exception)
            {
               throw;
            }

        }

        public string GenerateSecurityToken(string email,long UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config[("JWT:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserID",UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string forgetPassword(string email)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(u => u.Email == email);
                if(result!=null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    msmqModel msmqMod = new msmqModel();
                    msmqMod.sendData2Queue(token);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmpassword)
        {
            try
            {
                if (password.Equals(confirmpassword))
                {
                    var user = fundooContext.UserTable.FirstOrDefault(u => u.Email == email);
                    user.Password = password;
                    fundooContext.SaveChanges();
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

    }
}
