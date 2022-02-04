using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BO;
using ATM.DataLayer;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace ATM.LogicLayer
{

    public class Logic
    {




        // User validation method that uses Regular expression to determine if the string is in correct format
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

        // Password validation method that uses the same algorithm as IsValidUsername to determine if the varaible is in correct format 
        public bool IsValidPin(string pin)
        {

            if (pin.Length != 5)
            {
                return false;
            }
            else if (Regex.IsMatch(pin, "[0-9]"))
            {
                return true;
            }
            return false;
        }

        // Administration method to initialize account creation
        public void CreateAccount()
        {
            // Initialization of classes for method usage
            Data data = new Data();
            var get = new UserCreation();

            // Initialization of objects 
            var customer = new Customer();
            var user = new User();

            Console.Clear();
            Console.WriteLine("   ----Creating new Account----");

            while (true)
            {

                // Event Subscriber that triggers if the given username already exists
                data.OnIsInFileEvent += (s, args) =>
                {
                    if (args is User)
                    {
                        Console.WriteLine("This user already exists. Please try again!");
                        Thread.Sleep(1200);

                        CreateAccount();
                    }

                };

                // User() assignment and encryption with AES 
                user.Username = Encrypt(get.User());
                user.Pin = Encrypt(get.Pin());

                // Trigger to check if user actually exists in the database
                data.OnIsInFile(user);

                // Validation and assignment of the User if the account will be an admin or not.
                // if true, it will directly write the credentials and exit the loop, as no further data is needed
                if ((user.IsAdmin = get.CustomerAccountType()))
                {
                    user.accountNumber = data.GetLastAccountNumber();
                    data.AddtoFile<User>(user);
                    Console.WriteLine($"User {0} successfully added.", user.Username);
                    Task.Delay(1000);
                    break;
                    
                }

                // object Customer() variables assignment through class UserCreation
                customer.Name = get.Holder();
                customer.accountType = get.AccountType();
                customer.Balance = get.Balance();
                customer.Status = get.AccountStatus();

                // ID assignment. Due to login credentials and customer data being created in two different repositories,
                // it is necessary to link them with some sort of identifier for lookup. All of this provides easier data 
                // manipulation and integrity to the framework 
                customer.accountNumber = data.GetLastAccountNumber();
                user.accountNumber = customer.accountNumber;

                // Writing the customer to file(s)
                data.AddtoFile<User>(user);
                data.AddtoFile<Customer>(customer);
                break;
            }
        }



        public void DeleteAccount()
        {
            throw new NotImplementedException();
        }

        // Updates CUSTOMER accounts. admin accounts have to be added manually via an pre-existing administration 
        // (master)
        public void UpdateAccount()
        {
            var data = new Data();
            List<User> users = data.ReadFile<User>("user.txt");
            foreach(User user in users)
            {
                Console.WriteLine(Decrypt(user.Username)+" admin: "+user.IsAdmin);           
            }
            while (true)
            {
                
                Customer customer = new Customer();
                var get = new UserCreation();
                getCustomer:
                Console.Write("Enter the account username you want to change: ");
                  
                var _customer = Console.ReadLine();
 

                    customer = (Customer)data.GetCustomer(_customer);

                if (customer != null)
                {
                    Console.Write("Name: Current({0}) ", customer.Name); 
                }
                else
                {
                    Console.WriteLine("Customer you entered does not exist or is an admin. please try again");
                    goto getCustomer;
                }
                

                string _name = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(_name))
                {
                    customer.Name = _name;
                }

                Console.WriteLine("Account Type: Current({0}) ", customer.accountType);
                customer.accountType = get.AccountType();

                Console.Write("Balance: Current({0})", customer.Balance);
                string _balance = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(_balance))
                {
                    customer.Balance = Convert.ToInt32(_balance);
                }

                Console.WriteLine("Account Status: Current({0})", Convert.ToString(customer.Status));

                customer.Status = get.AccountStatus();

                data.WritetoFile(customer);
                Console.WriteLine("The account with username {0} and ID {1} has been successfully updated.", _customer, customer.accountNumber);
                break;

            }
        }


        //Encryption method that takes parameter of string.
        public string Encrypt(string text)
        {
            // Sets the given string to a byte stream
            var b = Encoding.UTF8.GetBytes(text);

            // Encrypts via fetching GetAes(), creating an encryptor, etc
            var encrypted = GetAes().CreateEncryptor().TransformFinalBlock(b, 0, b.Length);

            // Returns encrypted string to the source 
            return Convert.ToBase64String(encrypted);

        }

        public string Decrypt(string encrypted)
        {
            var b = Convert.FromBase64String(encrypted);
            var decrypted = GetAes().CreateDecryptor().TransformFinalBlock(b, 0, b.Length);

            return Encoding.UTF8.GetString(decrypted);
        }

        // AES init
        // Improve the AES code and generation of it to be a bit more fool proof 
        private Aes GetAes()
        {
            try
            {
                // Assignment of a public key with an array of 16 bytes.
                var publickey = new Byte[16];

                // Secret key assignment. Hardcoded for now .
                string secretkey = "novak"; //make it connect to SQL to read secret key, but who the fuck needs this now?

                // Converting the secret key to byte encoding .
                var skeyByte = Encoding.UTF8.GetBytes(secretkey);

                // Copies all the bytes in the private key into the public key, and sets length with math.min.
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
        public void FileEncryption(string source, string output)
        {
            using (var sourceStream = File.OpenRead(source))
            using (var destinationStream = File.Create(output))
            using (var provider = GetAes())

            using (var cryptoTransform = provider.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
            {

                destinationStream.Write(provider.IV, 0, provider.IV.Length);
                sourceStream.CopyTo(cryptoStream);

                // Return the key of the encryption
                Console.WriteLine(Convert.ToBase64String(provider.Key));
            }
        }

        // Note to self: string key input is not necessary. Implement so that the hardcoded key for AES is 
        // already coded into the GetAes() method. Within the file, it is another key, that will decrypt the 
        // repository of data and decrypt it for actual access and read/write. However, only doing it real-time
        // when read and write is actually happening.

        public void FileDecryption(string source, string output, string key)
        {

            using (var sourceStream = File.OpenRead(source))
            using (var destinationStream = File.Create(output))
            using (var provider = GetAes())
            {

                var IV = new byte[provider.IV.Length];
                sourceStream.Read(IV, 0, IV.Length);
                using (var cryptoTransform = provider.CreateDecryptor(Convert.FromBase64String(key), IV))
                using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(destinationStream);
                }
            }
        }
    }
}
