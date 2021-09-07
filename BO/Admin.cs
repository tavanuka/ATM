using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    
    public class Admin : User
    {
        private object obj;

        public Admin()
        {
        }

        public Admin(object obj)
        {
            this.obj = obj;
        }

        public override string Username { get; set; }
        public override string Pin { get; set; }
    }
}
