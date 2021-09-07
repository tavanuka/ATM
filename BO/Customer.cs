using System;

namespace BO
{
    public class Customer : User
    {
        private object user;

        public Customer()
        {
         
        }

        public Customer(object user)
        {
            this.user = user;
        }

        public override string Username { get; set; }
        public  override string Pin { get; set; }
        public string Name { get; set; }
        public string accountType { get; set; }
        public int Balance { get; set; }
        public bool Status { get; set; }
        public int accountNumber { get; set; }

    }
}
