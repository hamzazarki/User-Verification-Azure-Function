using System;
using System.Diagnostics;

namespace AzureB2CUserFunction.Models
{
    
    public class User
    {
        public string signInName { get; set; }
        public string password { get; set; }
        public bool UserExist { get; set; }

        public User(string signInName, string password)
        {
            this.signInName = signInName;
             this.password = password;
        }

        
    }
}
