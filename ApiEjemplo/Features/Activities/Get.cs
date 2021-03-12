using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using ApiEjemplo.Features.Activities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikingUltimate.Server.Features.Activities
{
    public class Get
    {
        public class Query : IRequest<ActivityRead>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityRead>
        {
            private readonly BikingContext context;
            private readonly IConfigurationProvider mappingConfig;

            public Handler(BikingContext context, IConfigurationProvider mappingConfig)
            {
                this.context = context;
                this.mappingConfig = mappingConfig;
            }

            public async Task<ActivityRead> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                    .ProjectTo<ActivityRead>(mappingConfig)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                if (activity is null)
                {
                    throw new NotFoundException($"The activity {request.Id} cannot be found");
                }

                return activity;
            }
        }
    }
}