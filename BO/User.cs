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

        public virtual string Username { get; set; }
        public virtual string Pin { get; set; }
        public virtual bool IsAdmin { get; set; }
    }
}
