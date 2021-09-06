using BO;
using System;
using Xunit;
using ATM.DataLayer;

namespace ATMTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Data data = new Data();
            Admin admin = new Admin { Username = "admin", Pin = "12345"};

            

            data.AddtoFile<Admin>(admin);
        }
    }
}
