using System;
using System.Collections.Generic;
using System.Text;
using ATM.LogicLayer;
using ATM.DataLayer;
using BO;
using System.Threading;

namespace ATM.ViewLayer
{
    public class View
    {
        public void LoginScreen()
        {
            Console.WriteLine("----BANK OF M8IT---\n\n");

            try
            {
                Logic user = new Logic();
               // Data account = new Data();
                _ = new Customer();
                
                Customer customer;
            
                while (true)
                {
                    Console.Write("Username: ");

                    var username = Console.ReadLine();

                    Console.Write("Pin: ");
                    var pass = "";
                    ConsoleKey key;
                    do
                    {
                        var KeyInfo = Console.ReadKey(intercept: true);
                        key = KeyInfo.Key;

                        if (key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            Console.Write("\b \b");
                            pass = pass[0..^1];
                        }
                        else if (!char.IsControl(KeyInfo.KeyChar))
                        {
                            Console.Write("*");
                            pass += KeyInfo.KeyChar;
                        }
                    }
                    while (key != ConsoleKey.Enter);


                    var adminLogin = new Admin() 
                    { Pin = user.Encrypt(pass), Username = user.Encrypt(username) };
                    var userLogin = new Customer()
                    { Pin = user.Encrypt(pass), Username = user.Encrypt(username) };

                    Data data = new Data();
                    data.IsInFile += Data_IsInFile;
                    data.StartIsInFile(userLogin);
                    data.StartIsInFile(adminLogin);
                    break;
                   /* 
*/
                }
                   
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n{0}",ex.Message);
            }
        }

        private void Data_IsInFile(object user)
        {
            bool isSignedIn = false;
            if (user is Admin)
            {
                isSignedIn = true;
                AdminScreen();
            }
            else if (user is Customer)
            {
                isSignedIn = true;
                CustomerScreen((Customer)user);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("----BANK OF M8IT---\n\n");
                Console.WriteLine("\nWrong Username/Pin. Try again!");
            };
        }

        /* private void DNAnalysis()
         {
             Console.Clear();
             Console.WriteLine(@" _____   ______  ______  ______  _   _  _    _  _______  _____ ");
             Thread.Sleep(100);
             Console.WriteLine(@"|  __ \ |  ____||  ____||___  / | \ | || |  | ||__   __|/ ____|");
             Thread.Sleep(100);
             Console.WriteLine(@"| |  | || |__   | |__      / /  |  \| || |  | |   | |  | (___  ");
             Thread.Sleep(100);
             Console.WriteLine(@"| |  | ||  __|  |  __|    / /   | . ` || |  | |   | |   \___ \ ");
             Thread.Sleep(100);
             Console.WriteLine(@"| |__| || |____ | |____  / /__  | |\  || |__| |   | |   ____) |");
             Thread.Sleep(100);
             Console.WriteLine(@"|_____/ |______||______|/_____| |_| \_| \____/    |_|  |_____/ ");
             Thread.Sleep(10000);
         }
        */
        private void CustomerScreen(Customer user)
        {
            throw new NotImplementedException();
        }

        private void AdminScreen()
        {
            AdminScreen:
            {

            
            Console.Clear();
            Console.WriteLine("----BANK OF M8IT---");
            Console.WriteLine("    Administration account\n\n");
            Console.WriteLine("1----Create New Account\n" + 
                              "2----Delete Existing Account\n" +
                              "3----Update Account Information\n" +
                              "4----Search for Account\n" +
                              "5----View Reports\n\n" + 
                              "6----Exit");

                try
                {
                    string option = Console.ReadLine();
                    if(option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6")
                    {
                        Logic logic = new Logic();
                        switch (option)
                        {
                            case "1":
                                
                                break;
                            case "2":

                                break;
                            case "3":

                                break;
                            case "4":

                                break;
                            case "5":

                                break;
                            case "6":
                                System.Environment.Exit(0);
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wrong input!");
                        goto AdminScreen;
                    }
                    

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
