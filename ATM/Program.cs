using System;
using ATM.DataLayer;
using ATM.LogicLayer;
using BO;
using ATM.ViewLayer;

namespace ATM
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            ////string test = "56741";
            //Logic logic = new Logic();
            ////Console.WriteLine(logic.IsValidPin(test));
            //var add = new Data();
            //var admin = new Admin() { Username = logic.Encrypt("tavanuka"), Pin = logic.Encrypt("12345") };
            
            //add.AddtoFile<Admin>(admin);


            View Login = new View();

            Login.LoginScreen();

        }
    }
}
