using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BO;
using ATM.DataLayer;
using System.Text.RegularExpressions;
using System.Threading;

namespace ATM.LogicLayer
{

    public class Logic
    {

        

        

        public bool IsValidUsername(string username)
        {
            
                if (Regex.IsMatch(username, "[a-zA-Z0-9]"))
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
            var user = new User();
            var get = new UserCreation();
            Console.Clear();
            Console.WriteLine("   ----Creating new Account----");

        while(true)
            {
                data.OnIsInFileEvent += (s, args) =>
                {
                    if (args is User)
                    {
                        Console.WriteLine("This user already exists. Please try again!");
                        Thread.Sleep(1200);
                        
                        CreateAccount();
                    }
                    
                };

                user.Username = Encrypt(get.User());
         
                user.Pin = Encrypt(get.Pin());

                data.OnIsInFile(user);

                if ((user.IsAdmin = get.CustomerAccountType()))
                {
                    data.AddtoFile<User>(user);
                }

                customer.Name = get.Holder();
                customer.accountType = get.AccountType();
                customer.Balance = get.Balance();
                customer.Status = get.AccountStatus();

                customer.accountNumber = data.GetLastAccountNumber();
                user.accountNumber = customer.accountNumber;
                data.AddtoFile<User>(user);
                data.AddtoFile<Customer>(customer);
                break;
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
