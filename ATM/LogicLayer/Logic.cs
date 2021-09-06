using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BO;
using ATM.DataLayer;
using System.Text.RegularExpressions;

namespace ATM.LogicLayer
{
    
    class Logic
    {
        /*
         public void VerifyLogin(User obj)
        {

            var user = new Data();
            
            user.IsInFile += User_IsInFile;
            user.StartIsInFile(obj);

            if ((admin = (Admin)user.IsInFile(obj)) is Admin)
            {
                return admin;
            }
            else if ((customer = (Customer)user.IsInFile(obj)) is Customer)
            {
                return customer;
            }
            return null;
           
           
        }

        private object User_IsInFile(object user)
        {
            return user;
        }
    */
        public bool IsValidUsername(string username)
        {
            
                if (Regex.IsMatch(username, "[a - zA - Z0 - 9]"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public bool IsValidPin(string pin)
        {

            if(pin.Length != 5)
            {
                return false;
            }
            else if(Regex.IsMatch(pin, "[0-9]"))
            {
                return true;
            }
            return false;
        }


        public void CreateAccount()
        {
            Data data = new Data();
            var customer = new Customer();
            Console.WriteLine("----Creating new Account---");
            var reset = true;
            while (reset)
            {
                
                Console.Write("Username: ");
                string un = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(un) || !IsValidUsername(un))
                {
                    Console.WriteLine("Enter a valid username!");
                    reset = false;
                }
                else continue;
                string pin = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(pin) || !IsValidPin(pin))
                {
                    Console.WriteLine("Enter a valid pin!");
                    reset = false;
                }
                else continue;
                customer.Username = Encrypt(un);
                customer.Pin = Encrypt(pin);

                data.IsInFile += data_IsInFile;
                data.StartIsInFile(customer);
            }
            


           // data.AddtoFile();

        }

        public static void data_IsInFile(object user)
        {
           if(user is Customer)
            {
                Console.WriteLine("the password and username already exist!");
            }
           else if(user is Admin)
            {
                Console.Write("Holders Name: ");
                Console.ReadLine();
            }
        }

        public void DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount()
        {
            throw new NotImplementedException();
        }


        public string Encrypt(string text)
        {
            var b = Encoding.UTF8.GetBytes(text);
            var encrypted = GetAes().CreateEncryptor().TransformFinalBlock(b, 0, b.Length);

            return Convert.ToBase64String(encrypted);
            
        }

        public string Decrypt(string encrypted) 
        {
            var b = Convert.FromBase64String(encrypted);
            var decrypted = GetAes().CreateDecryptor().TransformFinalBlock(b, 0, b.Length);

            return Encoding.UTF8.GetString(decrypted);
        }

        private Aes GetAes()
        {
            try
            {
                var publickey = new Byte[16];
                string secretkey = "novak"; //make it connect to SQL to read secret key, but who the fuck needs this now?
                var skeyByte = Encoding.UTF8.GetBytes(secretkey);
                Array.Copy(skeyByte, publickey, Math.Min(publickey.Length, skeyByte.Length));
                Aes aes = Aes.Create();

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;
                aes.Key = publickey;
                aes.IV = publickey;

                return aes;
            }
            
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            
            
            
        }
    }
}
