using Logic.Utils;

namespace Logic.Models
{
    public class Address : BaseEntity<long>
    {
        public          string      Street      { get; private set; }
        public          string      City        { get; private set; }
        public          string      ZipCode     { get; private set; }
        public          bool        IsPrimary   { get; private set; }
        public virtual  Customer    Customer    { get; private set; }
        
        private Address() //For EF
        {

        }

        public Address(
            string  street      ,
            string  city        ,
            string  zipCode     
            )               
        {
            Street  = street  ;
            City    = city    ;
            ZipCode = zipCode ;
        }

        public void MarkPrimay()
        {
            IsPrimary = true;
        }

        public void RemovePrimay()
        {
            IsPrimary = false;
        }
    }
}