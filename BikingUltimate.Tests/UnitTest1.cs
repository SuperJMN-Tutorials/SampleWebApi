using System;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using ApiEjemplo.Features.Users;
using ApiEjemplo.Model;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BikingUltimate.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Adding_bike_it_gets_added()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BikingContext>()
                .UseInMemoryDatabase(nameof(Adding_bike_it_gets_added))
                .EnableDetailedErrors()
                .Options;
            BikingContext context = new BikingContext(dbContextOptions);
            var user = new User()
            {
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            CreateBike.BikeInfo bikeInfo = new CreateBike.BikeInfo();
            var command = new CreateBike.Request(user.Id, bikeInfo);

            IConfigurationProvider mapping = new MapperConfiguration(c => c.AddProfile<MappingProfile>());

            var handler = new CreateBike.Handler(context, mapping);
            var createdId = await handler.Handle(command, CancellationToken.None);

            createdId.Should().BeGreaterThan(0);
        }
    }
}
