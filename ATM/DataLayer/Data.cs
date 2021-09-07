using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using BO;
using ATM.LogicLayer;

namespace ATM.DataLayer
{
    public delegate void Notify(object user);

    public class Data 
    {
        public event Notify IsInFile;

        private readonly Logic logic = new Logic();
        //private readonly string adminFile = "admin.txt";
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

        public void IsAdminInFile()
        {
            var userList = ReadFile<User>(userFile);

        }


        public void StartIsInFile(object user)
        {
            
            var userList = ReadFile<User>(userFile);
            var customerList = ReadFile<Customer>(customerFile);
            

        
                foreach (User un in userList)
                {
                    if (un.Username == ((User)user).Username && un.Pin == ((User)user).Pin && un.IsAdmin == true) //&& admin.Pin == ((Admin)user).Pin has been removed for testing purposes
                    {
                        OnIsInFile(un);
                    }
                    else
                    {
                        Customer cus = (Customer)JsonSerializer.Deserialize<User>(JsonSerializer.Serialize((User)user));

                        foreach (Customer customer in customerList)
                        {
                            if (customer.Username == cus.Username && customer.Pin == cus.Pin)
                            {
                                OnIsInFile(customer);
                            }
                        }
                    }
                }

        }
        protected virtual void OnIsInFile(object user)
        {
            IsInFile?.Invoke(user);
        }

    }

    
}
