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
                userEntity.Password = Encryption(User.Password);

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
                var result = fundooContext.UserTable.FirstOrDefault(u => u.Email == loginModel.Email);
                if (result != null && Decryption(result.Password) == loginModel.Password)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
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

        public string Encryption(string password)
        {
            string Key = "bridgelabz@123456789123456789";
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string Decryption(string encryptedPass)
        {
            string Key = "bridgelabz@123456789123456789";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }

    }
}
