using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using BO;
using ATM.LogicLayer;
using System.Security.Cryptography;

namespace ATM.DataLayer
{

    public class Data 
    {
        //Event handlers and providers 
        public EventHandler OnVerifyLoginEvent;
        public EventHandler OnIsInFileEvent;

        
        private readonly string customerFile = "customer.txt";
        private readonly string userFile = "user.txt";

        //Helper method to get current file path
        public string GetFilePath(string filePath)
        {
            return Path.Combine(Environment.CurrentDirectory, filePath);
        }

        //Reads a file according to the provided object type 
        public List<T> ReadFile<T>(string FileName)
        {
            List<T> list = new List<T>();
            string FilePath = GetFilePath(FileName);
            StreamReader sr = new StreamReader(FilePath);

            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(JsonSerializer.Deserialize<T>(line));
            }
            sr.Close();

            return list;
        }

        public void WritetoFile(Customer customer)
        {
            var customers = ReadFile<Customer>(customerFile);
            if(customers.Exists(x => x.accountNumber == customer.accountNumber))
            {
                string jsonOutput = "";
                customers.Remove(customers.Find(x => x.accountNumber == customer.accountNumber));
                customers.Add(customer);
                File.Delete(GetFilePath(customerFile));
                foreach (Customer _customer in customers)
                {
                   jsonOutput = JsonSerializer.Serialize(_customer);
                    
                    File.AppendAllText(GetFilePath(customerFile), jsonOutput + Environment.NewLine);
                }
                
            }
        }
        //Adds the given account type to the right database
        public void AddtoFile<T>(T obj)
        {
            
            string jsonOutput = JsonSerializer.Serialize(obj);
            if (obj is User)
            {
                File.AppendAllText(GetFilePath(userFile), jsonOutput + Environment.NewLine);
            }
            else if (obj is Customer)
            {
                File.AppendAllText(GetFilePath(customerFile), jsonOutput + Environment.NewLine);
            }
        }

        //public void OnVerifyLogin(object user)
        //{
        //    Logic logic = new Logic();

        //    var userList = ReadFile<User>(userFile);
        //    var customerList = ReadFile<Customer>(customerFile);

        //    if (user is User)
        //    {
        //        foreach (User un in userList)
        //        {
        //            if (un.Username == ((User)user).Username && un.Pin == ((User)user).Pin && un.IsAdmin == true) //&& admin.Pin == ((Admin)user).Pin has been removed for testing purposes
        //            {
        //                OnVerifyLoginEvent?.Invoke(this, un);
        //            }
        //        }
        //    }
        //    else if (user is Customer)
        //    {
        //        foreach (Customer customer in customerList)
        //        {
        //            if (customer.Username == ((Customer)user).Username && customer.Pin == ((Customer)user).Pin)
        //            {
        //                OnVerifyLoginEvent?.Invoke(this, customer);
        //            }
        //        }
        //    }
        //}

        //Method to check if the account exists in the repository
        public void OnIsInFile(User user)
        {
            var userList = ReadFile<User>(userFile);
            var customerList = ReadFile<Customer>(customerFile);
            foreach (User un in userList)
            {
                if (un.Username == user.Username && un.Pin == user.Pin)
                {
                    OnIsInFileEvent?.Invoke(this, un);
                }
            }
            
        }

        // Gets the account number of the given user. Utilizes User database 
        public int GetAccountNumber(string customer)
        {
            var logic = new Logic();
            List<User> list = ReadFile<User>(userFile);
            foreach(User user in list)
            {
                if(logic.Decrypt(user.Username) == customer)
                {
                    return user.accountNumber;
                }
            }
            return 0;
        }

        public object GetCustomer(string customer)
        {
            List<Customer> customers = ReadFile<Customer>(customerFile);
            int accNumber =GetAccountNumber(customer);

            foreach(Customer _customer in customers)
            {
                if (_customer.accountNumber == accNumber)
                {
                    return _customer;
                }
            }
            return null;
        }

        // Gets the account number for the new assignment.
        // should be used only as creating new accounts!!
        public int GetLastAccountNumber()
        {
            List<Customer> list = ReadFile<Customer>(customerFile);
            if (list.Count > 0)
            {
                int result = list.Count - 1;
                return result;
            }else 
            return 0;
        }
       
      

    }

    
}
