using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikingUltimate.Server.Features.Users
{
    public class GetUsersWhoRequireMaintence
    {
        public class Request : IRequest<ICollection<MaintanceUserRead>>
        {
            public Request(DateTimeOffset now)
            {
                Now = now;
            }

            public DateTimeOffset Now { get; }
        }

        public class Handler : IRequestHandler<Request, ICollection<MaintanceUserRead>>
        {
            private readonly BikingContext context;
            private readonly IConfigurationProvider mappingConfiguration;

            public Handler(BikingContext context, IConfigurationProvider mappingConfiguration)
            {
                this.context = context;
                this.mappingConfiguration = mappingConfiguration;
            }

            public async Task<ICollection<MaintanceUserRead>> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from u in context.Users
                    from b in u.Bikes
                    from c in b.Components
                    where c.AddedOn <= request.Now.AddMonths(-6)
                    select u;

                var uniqueUsers = query.Distinct();

                var usersToReturn = await uniqueUsers
                    .ProjectTo<MaintanceUserRead>(mappingConfiguration)
                    .ToListAsync(cancellationToken);

                return usersToReturn;
            }
        }

        public class MaintanceUserRead
        {
            public string Username { get; set; }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}