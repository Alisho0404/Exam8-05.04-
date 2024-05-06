using Domain.DTOs.TransactionDTOs;
using Domain.Filters;
using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Razor.Pages.Transaction
{
    [IgnoreAntiforgeryToken]
    public class GetTransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionsModel(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
            Transactions = new List<GetTransactionDto>();
        }

        [BindProperty(SupportsGet = true)]
        public TransactionFilter Filter { get; set; }
        public int TotalPages { get; set; }

        public List<GetTransactionDto> Transactions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _transactionService.GetTransactionsAsync(Filter);
                Transactions = response.Data;
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
