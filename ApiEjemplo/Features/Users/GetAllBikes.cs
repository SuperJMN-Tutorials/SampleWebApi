using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiEjemplo.Features.Users
{
    public class GetAllBikes
    {
        public class Request : IRequest<ICollection<BikeRead>>
        {
            public Request(int userId)
            {
                UserId = userId;
            }

            public int UserId { get; }
        }

        public class Handler : IRequestHandler<Request, ICollection<BikeRead>>
        {
            private readonly BikingContext bikingContext;
            private readonly IConfigurationProvider mappingConfig;

            public Handler(BikingContext bikingContext, IConfigurationProvider mappingConfig)
            {
                this.bikingContext = bikingContext;
                this.mappingConfig = mappingConfig;
            }

            public async Task<ICollection<BikeRead>> Handle(Request request, CancellationToken cancellationToken)
            {
                var listAsync = await bikingContext.Users
                    .Include(s => s.Bikes)
                    .Where(x => x.Id == request.UserId)
                    .SelectMany(s => s.Bikes)
                    .ProjectTo<BikeRead>(mappingConfig)
                    .ToListAsync(cancellationToken);

                return listAsync;
            }
        }

        public class BikeRead
        {
            public int Id { get; set; }
            public string Brand { get; set; }
            public double Distance { get; set; }
            public string Model { get; set; }
        }
    }
}