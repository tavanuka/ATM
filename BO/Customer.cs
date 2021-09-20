using System;

namespace BO
{
    public class Customer 
    {
        private object user;

        public Customer()
        {
         
        }

        public Customer(object user)
        {
            this.user = user;
        }

        public string Username { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string accountType { get; set; }
        public int Balance { get; set; }
        public bool Status { get; set; }
        public int accountNumber { get; set; }

    }
}
