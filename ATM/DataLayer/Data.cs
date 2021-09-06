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
        private readonly string adminFile = "admin.txt";
        private readonly string customerFile = "customer.txt";

        
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
            if (obj is Admin)
            {
                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, adminFile), jsonOutput + Environment.NewLine);
            }
            else if (obj is Customer)
            {
                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, customerFile), jsonOutput + Environment.NewLine);
            }
        }

        public void StartIsInFile(object user)
        {
            
            var adminList = ReadFile<Admin>(adminFile);
            var customerList = ReadFile<Customer>(customerFile);
           

            if(user is Admin)
            {
                foreach (Admin admin in adminList)
                {
                    if (admin.Username == ((Admin)user).Username) //&& admin.Pin == ((Admin)user).Pin has been removed for testing purposes
                    {
                        OnIsInFile(admin);

                    }
                    else continue;
                }
            }    
            else if (user is Customer)
            {
                foreach (Customer customer in customerList)
                {
                    if (customer.Username == ((Customer)user).Username && customer.Pin == ((Customer)user).Pin)
                    {
                        OnIsInFile(customer);
                    }
                    
                } 
            }
            else OnIsInFile(null);





        }
        protected virtual void OnIsInFile(object user)
        {
            IsInFile?.Invoke(user);
        }

    }

    
}
