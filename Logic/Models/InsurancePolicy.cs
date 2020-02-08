using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class InsurancePolicy : BaseEntity<long>
    {
        public          string                  PolicyHolderName    { get; private set; }
        public          string                  SumInsured          { get; private set; }
        public          decimal                 PremiumAmount       { get; private set; }
        public virtual  ICollection<Nominee>    Nominees            { get; private set; }

        public InsurancePolicy(
            string                  policyHolderName    ,
            string                  sumInsured          ,
            decimal                 premiumAmount       ,
            ICollection<Nominee>    nominees            
            )
        {
            PolicyHolderName    = policyHolderName   ;
            SumInsured          = sumInsured         ;
            PremiumAmount       = premiumAmount      ;
            Nominees            = nominees           ;
        }

        public void Update(
            string                  policyHolderName    ,
            string                  sumInsured          ,
            decimal                 premiumAmount       ,
            ICollection<Nominee>    nominees          
            )
        { 
            PolicyHolderName    = policyHolderName   ;
            SumInsured          = sumInsured         ;
            PremiumAmount       = premiumAmount      ;
            Nominees.Clear();
            foreach (var nominee in nominees)
            {
                Nominees.Add(nominee);
            }
        }
    }
}
