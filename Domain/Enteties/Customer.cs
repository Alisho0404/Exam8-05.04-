using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public required string PhoneNumber { get; set; }  
        public List<Account>? Accounts { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
