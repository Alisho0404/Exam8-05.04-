using Domain.DTOs.AccountDTOs;
using Domain.Filters;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Razor.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class GetAccountsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public GetAccountsModel(IAccountService AccountService)
        {
            _accountService = AccountService;
            Accounts = new List<GetAccountDto>();
        }

        [BindProperty(SupportsGet = true)]
        public AccountFilter Filter { get; set; }
        public int TotalPages { get; set; }

        public List<GetAccountDto> Accounts { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _accountService.GetAccountsAsync(Filter);
                Accounts = response.Data;
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
