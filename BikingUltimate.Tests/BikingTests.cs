using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using ApiEjemplo.Features.Bikes;
using ApiEjemplo.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BikingUltimate.Server.Features.Users;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BikingUltimate.Tests
{
    public class BikingTests
    {
        [Fact]
        public async Task Adding_bike_to_user_the_bike_is_added()
        {
            var context = CreateDbContext(nameof(Adding_bike_to_user_the_bike_is_added));
            var user = new User
            {
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            var command = new CreateBike.Request(user.Id, new CreateBike.BikeInfo("Brand", "Model"));
            IConfigurationProvider mapping = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            var handler = new CreateBike.Handler(context, mapping);

            var createdId = await handler.Handle(command, CancellationToken.None);

            createdId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Adding_component_to_bike_the_component_is_added()
        {
            var context = CreateDbContext(nameof(Adding_component_to_bike_the_component_is_added));

            var bike = new Bike
            {
                Brand = "sadfd",
                Model = "asdfasdf",
            };

            await context.Bikes.AddAsync(bike);
            await context.SaveChangesAsync();

            var command = new CreateComponent.Request(bike.Id,
                new CreateComponent.ComponentInfo("Brand", "Model", 12, 1.3D, ComponentType.Brake, DateTimeOffset.Now));

            IConfigurationProvider mapping = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            var handler = new CreateComponent.Handler(context, mapping);

            var createdId = await handler.Handle(command, CancellationToken.None);

            createdId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Given_user_with_bikes_that_require_maintenance_the_user_is_returned()
        {
            var context = CreateDbContext(nameof(Given_user_with_bikes_that_require_maintenance_the_user_is_returned));
            await context.Users.AddAsync(new User()
            {
                Bikes = new List<Bike>()
                {
                    new()
                    {
                        Components = new List<Component>()
                        {
                            new()
                            {
                                AddedOn = 12.January(2020),
                            }
                        }
                    }
                }
            });
            await context.SaveChangesAsync();

            IConfigurationProvider mapping = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            var request = new GetUsersWhoRequireMaintence.Request(12.August(2020));
            var handler = new GetUsersWhoRequireMaintence.Handler(context, mapping);
            var users = await handler.Handle(request, CancellationToken.None);

            users.Count.Should().Be(1);
        }

        private BikingContext CreateDbContext(string dbName)
        {
            var dbContextOptions = new DbContextOptionsBuilder<BikingContext>()
                .UseInMemoryDatabase(dbName)
                .EnableDetailedErrors()
                .Options;
            BikingContext context = new BikingContext(dbContextOptions);
            return context;
        }
    }
}
