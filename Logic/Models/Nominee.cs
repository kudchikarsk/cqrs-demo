using Logic.Utils;

namespace Logic.Models
{
    public class Nominee : BaseEntity<long>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string RelationshipWithPolicyHolder { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}