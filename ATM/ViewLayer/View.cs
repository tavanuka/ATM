﻿using System;
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
            Console.WriteLine("   ----BANK OF M8IT----\n\n");
            
            try
            {
                Logic user = new Logic();
                Data data = new Data();

                bool isSignedIn = false;

                while (!isSignedIn)
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

                    var userLogin = new User()
                    { Pin = user.Encrypt(pass), Username = user.Encrypt(username) };
                    
                    data.OnVerifyLoginEvent += (s, args) =>
                    {
                        if (args is User && ((User)args).IsAdmin == true)
                        {
                            AdminScreen();
                        }
                    };
                    data.OnVerifyLogin(userLogin);

                Console.Clear();
                Console.WriteLine("   ----BANK OF M8IT----\n\n");
                Console.WriteLine("\nWrong Username/Pin. Try again!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n{0}",ex.Message);
            }
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
        private void CustomerScreen(Customer user, bool signedIn)
        {
            throw new NotImplementedException();
        }

        private void AdminScreen()
        {
            Logic logic = new Logic();


            
            Console.Clear();
            Console.WriteLine("   ----BANK OF M8IT----");
            Console.WriteLine("  Administration account\n\n");
            Console.WriteLine("1----Create New Account\n" + 
                              "2----Delete Existing Account\n" +
                              "3----Update Account Information\n" +
                              "4----Search for Account\n" +
                              "5----View Reports\n\n" + 
                              "6----Exit");
        adminScreen:
            try
                {
                
                    var keyPressed = Console.ReadKey(intercept: true);
                    var key = keyPressed.Key;

                    if(key == ConsoleKey.D1 || key == ConsoleKey.D2|| key == ConsoleKey.D3|| key == ConsoleKey.D4 || key == ConsoleKey.D5|| key == ConsoleKey.D6)
                    {
                        switch (key)
                        {
                            case ConsoleKey.D1:
                                logic.CreateAccount();
                                break;
                            case ConsoleKey.D2:
                                logic.DeleteAccount();
                                break;
                            case ConsoleKey.D3:
                                logic.UpdateAccount();
                            break;
                            case ConsoleKey.D4:
                                break;
                            case ConsoleKey.D5:
                                break;
                            case ConsoleKey.D6:
                            System.Environment.Exit(0);
                            break;
                        }
                    }
                    else
                    {
                        goto adminScreen;
                    }


                    /*
                     string option = Console.ReadLine();
                    if(option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6")
                    {
                        
                        switch (option)
                        {
                            case "1":
                                logic.CreateAccount();
                                break;

                            case "2":
                                
                                break;

                            case "3":
                                logic.UpdateAccount();
                                break;

                            case "4":

                                break;
                            case "5":

                                break;
                            case "6":
                                
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wrong input!");
                        goto AdminScreen;
                    }

*/
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
        }
    }
}
