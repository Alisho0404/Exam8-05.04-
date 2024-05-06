using System.Threading.Tasks;
using Domain.DTOs.AccountDTOs;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Account
{
    public class GetAccountByIdModel : PageModel
    {
        private readonly IAccountService _accountService;

        public GetAccountByIdModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public GetAccountDto Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            Account = account.Data;
            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}