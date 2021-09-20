using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class User : EventArgs
    {
        public User()
        {

        }

        public string Username { get; set; }
        public string Pin { get; set; }
        public bool IsAdmin { get; set; }
        public int ID { get; set; }
        public bool IsSignedIn { get; set; }
    }
}
