using AutoMapper;
using Domain.DTOs.AccountDTOs;
using Domain.DTOs.CustomerDTOs;
using Domain.DTOs.TransactionDTOs;
using Domain.Enteties;

namespace Infrastructure.AutoMappper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Account, CreateAccountDto>().ReverseMap();
            CreateMap<Account, GetAccountDto>().ReverseMap();
            CreateMap<Account, UpdateAccountDto>().ReverseMap();

            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<Transaction, GetTransactionDto>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        }

    }
}
