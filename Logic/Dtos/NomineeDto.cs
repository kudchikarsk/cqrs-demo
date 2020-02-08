using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class NomineeDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string RelationshipWithPolicyHolder { get; set; }
    }
}
