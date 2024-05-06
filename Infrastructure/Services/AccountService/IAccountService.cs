using Domain.DTOs.AccountDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AccountService
{
    public interface IAccountService
    {
        Task<PagedResponse<List<GetAccountDto>>> GetAccountsAsync(AccountFilter filter);
        Task<Response<GetAccountDto>> GetAccountByIdAsync(int id);
        Task<Response<string>> CreateAccountAsync(CreateAccountDto accountDto);
        Task<Response<string>> UpdateAccountAsync(UpdateAccountDto accountDto);
        Task<Response<bool>> RemoveAccountAsync(int id);
    }
}
