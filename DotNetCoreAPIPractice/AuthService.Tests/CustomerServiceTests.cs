using Xunit;
using AuthService.Infrastructure.Services;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using AuthService.Domain.Entities;
using AuthService.Application.DTOs;

public class CustomerServiceTests
{
    private AuthDBContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AuthDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        var context = new AuthDBContext(options);
        context.Customers.Add(new Customer { Name = "Abbas", Email = "abbas@test.com" });
        context.Customers.Add(new Customer { Name = "John", Email = "john@test.com" });
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetAll_ReturnsCustomers()
    {
        // Arrange
        var context = GetDbContext();
        var service = new CustomerService(context, null);

        var query = new CustomerQueryParams
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await service.GetAll(query);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }
}