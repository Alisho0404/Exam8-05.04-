using Domain.DTOs.CustomerDTOs;
using Domain.Filters;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Razor.Pages.Customer
{
    [IgnoreAntiforgeryToken]
    public class GetCustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public GetCustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
            Customers = new List<GetCustomerDto>();
        }

        [BindProperty(SupportsGet = true)]
        public CustomerFilter Filter { get; set; }
        public int TotalPages { get; set; }

        public List<GetCustomerDto> Customers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _customerService.GetCustomersAsync(Filter);
                Customers = response.Data;
                TotalPages = response.TotalPages;
                return Page();
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }


        }
    }
}
