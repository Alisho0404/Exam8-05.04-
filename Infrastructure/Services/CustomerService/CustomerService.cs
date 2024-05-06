using AutoMapper;
using Domain.DTOs.CustomerDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CustomerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            try
            {
                var existing = await _context.Customers.AnyAsync(x => x.Name == customerDto.Name);
                if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Already Exist");
                var newCustomer = _mapper.Map<Customer>(customerDto);

                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();

                return new Response<string>("Succesfully added");
            }
            catch (Exception e)
            {

                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> RemoveCustomerAsync(int id)
        {
            try
            {
                var existing = await _context.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();
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

        public async Task<Response<GetCustomerDto>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var existing=await _context.Customers.FirstOrDefaultAsync(x=>x.Id== id);
                if (existing==null)
                {
                    return new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Not found");
                } 

                var mapped=_mapper.Map<GetCustomerDto>(existing);
                return new Response<GetCustomerDto>(mapped);
            }
            catch (Exception e)
            {

                return new Response<GetCustomerDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PagedResponse<List<GetCustomerDto>>> GetCustomersAsync(CustomerFilter filter)
        {
            try
            {
                var customers = _context.Customers.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    customers=customers.Where(x=>x.Name.ToLower().Contains(filter.Name.ToLower()));
                } 

                var result=await customers
                    .Skip((filter.PageNumber-1)*filter.PageSize)
                    .Take(filter.PageSize).ToListAsync();
                var totalRecord = await customers.CountAsync();
                var response=_mapper.Map<List<GetCustomerDto>>(result);

                return new PagedResponse<List<GetCustomerDto>>(response, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {

                return new PagedResponse<List<GetCustomerDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto customerDto)
        {
            try
            {
                var existing=await _context.Customers.AnyAsync(x=>x.Id==customerDto.Id);
                if (!existing)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "Not Found");
                }

                var newCustomer = _mapper.Map<Customer>(customerDto);
                _context.Customers.Update(newCustomer);
                await _context.SaveChangesAsync();
                return new Response<string>("Customer succesfully updated");
            }
            catch (Exception e)
            {

                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
