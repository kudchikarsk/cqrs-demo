using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class Customer : BaseEntity<long>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public virtual ICollection<Address> Addresses { get; private set; }

        private Customer() //For EF
        {

        }

        public Customer(
            string firstName,
            string lastName,
            int age,
            ICollection<Address> addresses
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Addresses = addresses;
        }

        public void Update(
            string firstName,
            string lastName,
            int age,
            ICollection<Address> addresses
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Addresses.Clear();
            foreach (var address in addresses)
            {
                Addresses.Add(address);
            }
        }
    }
}
