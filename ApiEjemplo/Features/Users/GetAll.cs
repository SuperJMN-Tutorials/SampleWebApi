using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikingUltimate.Server.Features.Users
{
    public class GetAll
    {
        public class Request : IRequest<ICollection<UserRead>>
        {
        }

        public class Handler : IRequestHandler<Request, ICollection<UserRead>>
        {
            private readonly BikingContext bikingContext;
            private readonly IConfigurationProvider mappingConfig;

            public Handler(BikingContext bikingContext, IConfigurationProvider mappingConfig)
            {
                this.bikingContext = bikingContext;
                this.mappingConfig = mappingConfig;
            }

            public async Task<ICollection<UserRead>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await bikingContext.Users
                    .ProjectTo<UserRead>(mappingConfig)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
        }

        public class UserRead
        {
            public string Username { get; set; }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}