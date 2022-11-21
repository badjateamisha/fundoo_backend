using Businesslayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserEntity Registration(registration User)
        {
            try
            {
                return userRL.Registration(User);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public string Login(login loginModel)
        {
            try
            {
                return this.userRL.Login(loginModel);

            }
            catch (Exception)
            {
                return null;

            }
        }

        public string forgetPassword(string email)
        {
            try
            {
                return this.userRL.forgetPassword(email);
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
                return this.userRL.ResetPassword(email,password,confirmpassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
