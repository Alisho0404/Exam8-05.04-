using Domain.DTOs.TransactionDTOs;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Transaction
{
    public class UpdateTransactionModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public UpdateTransactionModel(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
        }

        [BindProperty]
        public UpdateTransactionDto Transaction { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            Transaction.Id = id;
            await _transactionService.UpdateTransactionAsync(Transaction);

            return RedirectToPage("/Transaction/GetTransactions");
        }
    }
}
