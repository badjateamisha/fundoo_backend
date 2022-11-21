using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(registration User);

        public string Login(login loginModel);

        public string forgetPassword(string email);

        public bool ResetPassword(string email, string password, string confirmpassword);

    }
}
