using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public virtual ICollection<AddressDto> Addresses { get; set; }
    }
}
