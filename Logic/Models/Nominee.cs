using Logic.Utils;

namespace Logic.Models
{
    public class Nominee : BaseEntity<long>
    {
        public          string          FullName                        { get; private set; }
        public          string          PhoneNumber                     { get; private set; }
        public          string          RelationshipWithPolicyHolder    { get; private set; }
        public virtual  InsurancePolicy InsurancePolicy                 { get; private set; }

        public Nominee() //For EF
        {

        }

        public Nominee(
            string          fullName                        ,
            string          phoneNumber                     ,
            string          relationshipWithPolicyHolder    
            )
        {
            FullName                        = fullName                      ;
            PhoneNumber                     = phoneNumber                   ;  
            RelationshipWithPolicyHolder    = relationshipWithPolicyHolder  ;  
        }
    }
}