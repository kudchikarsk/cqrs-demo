using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public virtual ICollection<AddressDto> Addresses { get; private set; }
    }
}
