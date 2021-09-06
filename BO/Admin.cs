using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    
    public class Admin
    {
        private object obj;

        public Admin()
        {
        }

        public Admin(object obj)
        {
            this.obj = obj;
        }

        public string Username { get; set; }
        public string Pin { get; set; }
    }
}
