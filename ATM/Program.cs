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
            Logic logic = new Logic();
            ////Console.WriteLine(logic.IsValidPin(test));
            var add = new Data();
           // var admin = new User() { Username = logic.Encrypt("tavanuka"), Pin = logic.Encrypt("12345"), IsAdmin = true };
            
           // add.AddtoFile(admin);


            View Login = new View();

            Login.LoginScreen();

        }
    }
}
