using System;
using System.Diagnostics;

namespace AzureB2CUserFunction.Models
{
    
    public class User
    {
        public string SignInName { get; set; }
        public string password { get; set; }
        public bool UserExist { get; set; }

        public User(string SignInName, string password)
        {
            this.SignInName = SignInName;
             this.password = password;
        }

        
    }
}
