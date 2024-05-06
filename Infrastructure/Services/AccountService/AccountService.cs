using AutoMapper;
using Domain.DTOs.AccountDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AccountService
{
    public class AccountService:IAccountService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AccountService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAccountAsync(CreateAccountDto accountDto)
        {
            try
            {
                var existing = await _context.Accounts.AnyAsync(x => x.AccountNumber == accountDto.AccountNumber);
                if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Already Exist");
                var newAccount = _mapper.Map<Account>(accountDto);

                await _context.Accounts.AddAsync(newAccount);
                await _context.SaveChangesAsync();

                return new Response<string>("Succesfully added");
            }
            catch (Exception e)
            {

                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> RemoveAccountAsync(int id)
        {
            try
            {
                var existing = await _context.Accounts.Where(x => x.Id == id).ExecuteDeleteAsync();
                if (existing == 0)
                {
                    return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
                }
                return new Response<bool>(true);

            }
            catch (Exception e)
            {

                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetAccountDto>> GetAccountByIdAsync(int id)
        {
            try
            {
                var existing = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
                if (existing == null)
                {
                    return new Response<GetAccountDto>(HttpStatusCode.BadRequest, "Not found");
                }

                var mapped = _mapper.Map<GetAccountDto>(existing);
                return new Response<GetAccountDto>(mapped);
            }
            catch (Exception e)
            {

                return new Response<GetAccountDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PagedResponse<List<GetAccountDto>>> GetAccountsAsync(AccountFilter filter)
        {
            try
            {
                var accounts = _context.Accounts.AsQueryable();
                if (!string.IsNullOrEmpty(filter.AccountNumber))
                {
                    accounts = accounts.Where(x => x.AccountNumber.ToLower().Contains(filter.AccountNumber.ToLower()));
                }

                var result = await accounts
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToListAsync();
                var totalRecord = await accounts.CountAsync();
                var response = _mapper.Map<List<GetAccountDto>>(result);

                return new PagedResponse<List<GetAccountDto>>(response, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {

                return new PagedResponse<List<GetAccountDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateAccountAsync(UpdateAccountDto accountDto)
        {
            try
            {
                var existing = await _context.Accounts.AnyAsync(x => x.Id == accountDto.Id);
                if (!existing)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "Not Found");
                }

                var newAccount = _mapper.Map<Account>(accountDto);
                _context.Accounts.Update(newAccount);
                await _context.SaveChangesAsync();
                return new Response<string>("Account succesfully updated");
            }
            catch (Exception e)
            {

                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
