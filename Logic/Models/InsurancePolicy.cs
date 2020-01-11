using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class InsurancePolicy : BaseEntity<long>
    {
        public string PolicyHolderName { get; set; }
        public string SumInsured { get; set; }
        public decimal PremiumAmount { get; set; }

        public virtual ICollection<Nominee> Nominees { get; set; }
    }
}
