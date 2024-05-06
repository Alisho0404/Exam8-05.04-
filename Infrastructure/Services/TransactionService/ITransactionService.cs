using Domain.DTOs.TransactionDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<PagedResponse<List<GetTransactionDto>>> GetTransactionsAsync(TransactionFilter filter);
        Task<Response<GetTransactionDto>> GetTransactionByIdAsync(int id);
        Task<Response<string>> CreateTransactionAsync(CreateTransactionDto transactionDto);
        Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto transactionDto);
        Task<Response<bool>> RemoveTransactionAsync(int id);
    }
}
