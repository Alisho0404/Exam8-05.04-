using Domain.DTOs.TransactionDTOs;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Transaction
{
    [IgnoreAntiforgeryToken]
    public class CreateTransactionModel : PageModel
    {

        private readonly ITransactionService _transactionService;
        public CreateTransactionModel(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
        }

        [BindProperty] public CreateTransactionDto TransactionDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _transactionService.CreateTransactionAsync(TransactionDto);
            return RedirectToPage("/Transaction/GetTransactions");

        }
    }
}
