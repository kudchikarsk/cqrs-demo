using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class AddressDto
    {
        public long Id { get; set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
    }
}
