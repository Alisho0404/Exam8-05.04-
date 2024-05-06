using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Account
    {
        public int Id { get; set; }
        public required string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int OwnerId { get; set; }
        public required string Type { get; set; }
        public Customer? Customer { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
