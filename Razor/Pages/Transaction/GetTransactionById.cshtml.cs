using System.Threading.Tasks;
using Domain.DTOs.TransactionDTOs;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Transaction
{
    public class GetTransactionByIdModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionByIdModel(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
        }

        public GetTransactionDto Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            Transaction = transaction.Data;
            if (Transaction == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}