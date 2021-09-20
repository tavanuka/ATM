using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.LogicLayer
{
    interface IUser
    {
        string Name { get; set; }
        string Pin { get; set;  }
        bool IsAdmin { get; set; }
    }
}
