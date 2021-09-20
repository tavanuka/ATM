using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    interface IUser
    {
        string Name { get; set; }
        string Pin { get; set;  }
    }
}
