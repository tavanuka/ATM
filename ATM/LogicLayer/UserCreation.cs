using ATM.DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.LogicLayer
{
    public class UserCreation
    {
        Logic logic = new Logic();
        public string User()
        {
            
            while (true)
            {
                Console.Write("Username: ");
                string un = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(un))
                {
                    Console.WriteLine("Enter a valid username!");
                    
                }
                else if (!logic.IsValidUsername(un))
                {
                    Console.WriteLine("Please enter a username that contains correct characters.");
                    
                }
                return un;
            }
        }

        public string Pin()
        {
            Console.Write("Pin: ");
            while (true)
            {
                
                string pin = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(pin))
                {
                    Console.WriteLine("Enter a valid pin!");
                }
                else if (!logic.IsValidPin(pin))
                {
                    Console.WriteLine("Please enter a digit pin that contains numbers, and is 5 numbers long.");
                }
                return pin;
            }
        }
        public bool CustomerAccountType()
        {
            Console.WriteLine("User account type: ");
            Console.Write("1---- User\n");
            Console.Write("2---- Admin\n");

            while (true)
            {

                var pressedKey = Console.ReadKey(intercept: true);
                var key = pressedKey.Key;

                if (key == ConsoleKey.D1)
                {
                    return false;
                }
                else if (key == ConsoleKey.D2)
                {
                    return true;
                } 
            }
        }

        public string Holder()
        {
            while (true)
            {
                Console.Write("Holders name: ");
                string name = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Please enter a Name into the field!");
                }
                return name;
            }
        }

        public string AccountType()
        {
            Console.WriteLine("Account type: ");
            Console.Write("1---- Savings\n");
            Console.Write("2---- Current\n");
            while (true)
            {
                var pressedKey = Console.ReadKey(intercept: true);
                var key = pressedKey.Key;

                if (key == ConsoleKey.D1)
                {
                    return "Savings";
                }
                else if (key == ConsoleKey.D2)
                {
                    return "Current";
                }
            }
        }
        public int Balance()
        {
            Console.WriteLine("Starting Balance: ");
            while (true)
            {
                
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a correct value!");
                }
            }
        }
        public bool AccountStatus()
        {
            Console.WriteLine("Account status:");
            Console.WriteLine("1---- Active \n2---- Disabled");
            while (true)
            {
                var pressedKey = Console.ReadKey(intercept: true);
                var key = pressedKey.Key;

                if (key == ConsoleKey.D1 || key == ConsoleKey.D2)
                {
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            return true;
                        case ConsoleKey.D2:
                            return false;
                    }
                }
            }
        }
    }
}
