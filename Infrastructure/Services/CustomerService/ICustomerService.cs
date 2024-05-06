using Domain.DTOs.CustomerDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CustomerService
{
    public interface ICustomerService
    { 
        Task<PagedResponse<List<GetCustomerDto>>> GetCustomersAsync(CustomerFilter filter);
        Task<Response<GetCustomerDto>>GetCustomerByIdAsync(int id);
        Task<Response<string>> CreateCustomerAsync(CreateCustomerDto customerDto);
        Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto customerDto);
        Task<Response<bool>> RemoveCustomerAsync(int id);

    }
}
