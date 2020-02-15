using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class AddressDto
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }

    public class CreateAddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
