using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class AccountFilter:PaginationFilter
    {
        public string? AccountNumber { get; set; }
    }
}
