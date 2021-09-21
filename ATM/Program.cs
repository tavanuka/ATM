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
            //logic.FileEncryption(add.GetFilePath("customer.txt"),add.GetFilePath("encrypted.key"));
            logic.FileDecryption(add.GetFilePath("encrypted.key"), add.GetFilePath("customer_decrypted.txt"), "bm92YWsAAAAAAAAAAAAAAA==");
            View Login = new View();

            Login.LoginScreen();

        }
    }
}
