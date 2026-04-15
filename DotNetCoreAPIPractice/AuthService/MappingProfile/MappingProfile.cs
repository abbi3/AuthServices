using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();
        }
    }
}
