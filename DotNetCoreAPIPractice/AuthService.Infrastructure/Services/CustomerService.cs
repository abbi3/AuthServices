using AuthService.Application.DTOs;
using AuthService.Application.Interface;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AuthDBContext _context;
        private readonly IMapper _mapper;

        public CustomerService(AuthDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        //public async Task<object> GetAll(CustomerQueryParams query)
        //{
        //    var customers = _context.Customers.AsQueryable();

        //    // 🔍 Search
        //    if (!string.IsNullOrEmpty(query.Search))
        //    {
        //        customers = customers.Where(x =>
        //            x.Name.Contains(query.Search) ||
        //            x.Email.Contains(query.Search));
        //    }

        //    // 🔃 Sorting
        //    if (query.SortBy.ToLower() == "name")
        //        customers = query.IsAscending ? customers.OrderBy(x => x.Name) : customers.OrderByDescending(x => x.Name);
        //    else if (query.SortBy.ToLower() == "email")
        //        customers = query.IsAscending ? customers.OrderBy(x => x.Email) : customers.OrderByDescending(x => x.Email);

        //    // 📄 Pagination
        //    var totalRecords = await customers.CountAsync();

        //    var data = await customers
        //        .Skip((query.PageNumber - 1) * query.PageSize)
        //        .Take(query.PageSize)
        //        .Select(x => new CustomerResponse
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Email = x.Email
        //        })
        //        .ToListAsync();

        //    return new
        //    {
        //        totalRecords,
        //        query.PageNumber,
        //        query.PageSize,
        //        data
        //    };
        //}

        public async Task<List<CustomerResponse>> GetAll(CustomerQueryParams query)
        {
            var customers = _context.Customers.AsQueryable();
            // 🔃 SORTING (NEW)
            if (query.SortBy.ToLower() == "name")
            {
                customers = query.IsAscending
                    ? customers.OrderBy(x => x.Name)
                    : customers.OrderByDescending(x => x.Name);
            }
            else if (query.SortBy.ToLower() == "email")
            {
                customers = query.IsAscending
                    ? customers.OrderBy(x => x.Email)
                    : customers.OrderByDescending(x => x.Email);
            }
            if (!string.IsNullOrEmpty(query.Search))
            {
                customers = customers.Where(x =>
                    x.Name.Contains(query.Search) ||
                    x.Email.Contains(query.Search));
            }

            return await _context.Customers
                .Skip((query.PageNumber - 1) * query.PageSize) // skip previous records
                .Take(query.PageSize) // take only required records
                .Select(x => new CustomerResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email
                })
                .ToListAsync();
        }

        public async Task<CustomerResponse> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<string> Create(CustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return "Customer created successfully";
        }

        public async Task<string> Update(int id, CustomerRequest request)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            await _context.SaveChangesAsync();

            return "Customer updated successfully";
        }

        public async Task<string> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return "Customer deleted successfully";
        }

        
    }
   
}
