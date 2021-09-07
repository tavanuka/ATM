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
            data.IsInFile += data_IsInFile;
        getUsername:
            {
                Console.Write("Username: ");
                string un = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(un))
                {
                    Console.WriteLine("Enter a valid username!");
                    goto getUsername;
                }
                else if (!IsValidUsername(un))
                {
                    Console.WriteLine("Please enter a username that contains correct characters.");
                    goto getUsername;
                }
                customer.Username = Encrypt(un);
            }

        getPin:
            {
                string pin = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(pin))
                {
                    Console.WriteLine("Enter a valid pin!");
                    goto getPin;

                }
                else if (!IsValidPin(pin))
                {
                    Console.WriteLine("Please enter a digit pin that contains numbers, and is 5 numbers long.");
                    goto getPin;
                }
                customer.Pin = Encrypt(pin);
            }

            data.StartIsInFile(customer);
        }

        private void data_IsInFile(object user)
        {
            if(user is null)
            {
                Console.WriteLine("test");
            } else
            {
                Console.WriteLine("good test");
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
