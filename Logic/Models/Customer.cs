using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class Customer : BaseEntity<long>
    {
        public          string                  FirstName   { get; private set; }
        public          string                  LastName    { get; private set; }
        public          int                     Age         { get; private set; }
        public virtual  ICollection<Address>    Addresses   { get; private set; }

        private Customer() //For EF
        {
            Addresses = new List<Address>();
        }

        public Customer(
            string                  firstName   ,
            string                  lastName    ,
            int                     age         
            ):base()
        {
            FirstName   = firstName   ;
            LastName    = lastName    ;
            Age         = age         ;            
        }

        public void Update(
            string firstName,
            string lastName,
            int age
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;            
        }

        public void AddAddress(Address address)
        {
            Addresses.Add(address);
        }

        public void RemoveAddress(Address address)
        {
            Addresses.Remove(address);
        }
    }
}
