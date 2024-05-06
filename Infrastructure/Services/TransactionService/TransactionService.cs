using AutoMapper;
using Domain.DTOs.TransactionDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TransactionService
{
    public class TransactionService:ITransactionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TransactionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
            public async Task<Response<string>> CreateTransactionAsync(CreateTransactionDto transactionDto)
            {
                try
                {
                    var existing = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == transactionDto.FromAccountId);
                    var existingUser = await _context.Accounts.FirstOrDefaultAsync(e => e.Id == transactionDto.ToAccountId);
                    if (existing == null && transactionDto.Amount <= 0  && existingUser == null)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "User not found");
                }                
                    
                    if (existing!.Balance >= transactionDto.Amount)
                    {
                        existing.Balance -= transactionDto.Amount;
                        existingUser!.Balance += transactionDto.Amount;
                        await _context.SaveChangesAsync();

                    }
                    var newTransaction = _mapper.Map<Transaction>(transactionDto);
                    await _context.Transactions.AddAsync(newTransaction);
                    await _context.SaveChangesAsync();
                    return new Response<string>("Successfully created ");
                }
                catch (DbException e)
                {
                    return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
                }
                catch (Exception e)
                {
                    return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
                }
            }
        

        public async Task<Response<bool>> RemoveTransactionAsync(int id)
        {
            try
            {
                var existing = await _context.Transactions.Where(x => x.Id == id).ExecuteDeleteAsync();
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

        public async Task<Response<GetTransactionDto>> GetTransactionByIdAsync(int id)
        {
            try
            {
                var existing = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
                if (existing == null)
                {
                    return new Response<GetTransactionDto>(HttpStatusCode.BadRequest, "Not found");
                }

                var mapped = _mapper.Map<GetTransactionDto>(existing);
                return new Response<GetTransactionDto>(mapped);
            }
            catch (Exception e)
            {

                return new Response<GetTransactionDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PagedResponse<List<GetTransactionDto>>> GetTransactionsAsync(TransactionFilter filter)
        {
            try
            {
                var transactions = _context.Transactions.AsQueryable();
                

                var result = await transactions
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToListAsync();
                var totalRecord = await transactions.CountAsync();
                var response = _mapper.Map<List<GetTransactionDto>>(result);

                return new PagedResponse<List<GetTransactionDto>>(response, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {

                return new PagedResponse<List<GetTransactionDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto transactionDto)
        {
            try
            {
                var existing = await _context.Transactions.AnyAsync(x => x.Id == transactionDto.Id);
                if (!existing)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "Not Found");
                }

                var newTransaction = _mapper.Map<Transaction>(transactionDto);
                _context.Transactions.Update(newTransaction);
                await _context.SaveChangesAsync();
                return new Response<string>("Transaction succesfully updated");
            }
            catch (Exception e)
            {

                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
