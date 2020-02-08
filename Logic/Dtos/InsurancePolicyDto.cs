using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Dtos
{
    public class InsurancePolicyDto
    {
        public long Id { get; set; }
        public string PolicyHolderName { get; set; }
        public string SumInsured { get; set; }
        public decimal PremiumAmount { get; set; }

        public virtual ICollection<NomineeDto> Nominees { get; set; }
    }
}
