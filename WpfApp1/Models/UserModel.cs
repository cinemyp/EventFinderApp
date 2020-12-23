using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserModel()
        {

        }
        public UserModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
