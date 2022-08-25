using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CorporateCustomer : Customer
    {
        public string TaxNumber { get; set; }
        public string CompanyName{ get; set; }

        public CorporateCustomer()
        {

        }

        public CorporateCustomer(int id, string email, string taxNumber, string companyName, string userName, 
            byte[] passwordSalt, byte[] passwordHash, bool status) : this()
        {
            Id = id;
            Email = email;
            TaxNumber = taxNumber;
            CompanyName = companyName;
            UserName = userName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
        }
    }
}
