using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using BO;
using ATM.LogicLayer;

namespace ATM.DataLayer
{

    public class Data 
    {
        public EventHandler OnVerifyLoginEvent;
        public EventHandler OnIsInFileEvent;

        
        private readonly string customerFile = "customer.txt";
        private readonly string userFile = "user.txt";
        

        public List<T> ReadFile<T>(string FileName)
        {
            List<T> list = new List<T>();
            string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
            StreamReader sr = new StreamReader(FilePath);

            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(JsonSerializer.Deserialize<T>(line));
            }
            sr.Close();

            return list;
        }

        public void AddtoFile<T>(T obj)
        {
            
            string jsonOutput = JsonSerializer.Serialize(obj);
            if (obj is User)
            {
                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, userFile), jsonOutput + Environment.NewLine);
            }
            else if (obj is Customer)
            {
                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, customerFile), jsonOutput + Environment.NewLine);
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

        public int GetLastAccountNumber()
        {
            List<Customer> list = ReadFile<Customer>(customerFile);
            if (list.Count > 0)
            {
                Customer customer = list[list.Count - 1];
                return customer.accountNumber;
            }
            return 0;
        }
       


    }

    
}
