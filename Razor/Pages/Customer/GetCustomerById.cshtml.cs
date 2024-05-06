using System.Threading.Tasks;
using Domain.DTOs.CustomerDTOs;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Customer
{
    public class GetCustomerByIdModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public GetCustomerDto Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            Customer = customer.Data;
            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}