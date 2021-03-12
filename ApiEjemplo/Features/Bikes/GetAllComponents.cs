using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BikingUltimate.Server.Features.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiEjemplo.Features.Bikes
{
    public class GetAllComponents
    {
        public class GetAllComponentsRequest : IRequest<ICollection<ComponentRead>>
        {
            public int BikeId { get; }

            public GetAllComponentsRequest(int bikeId)
            {
                BikeId = bikeId;
            }
        }

        public class Handler : IRequestHandler<GetAllComponentsRequest, ICollection<ComponentRead>>
        {
            private readonly BikingContext context;
            private readonly IConfigurationProvider mappingConfig;

            public Handler(BikingContext context, IConfigurationProvider mappingConfig)
            {
                this.context = context;
                this.mappingConfig = mappingConfig;
            }

            public async Task<ICollection<ComponentRead>> Handle(GetAllComponentsRequest request, CancellationToken cancellationToken)
            {
                if (await context.Bikes.FindAsync(new object[] { request.BikeId }, cancellationToken) is null)
                {
                    throw new NotFoundException($"Bike {request.BikeId} cannot be found");
                }

                var components = context.Bikes
                    .Where(x => x.Id == request.BikeId)
                    .SelectMany(bike => bike.Components);

                var componentReads = await components
                    .ProjectTo<ComponentRead>(mappingConfig)
                    .ToListAsync(cancellationToken);

                return new Collection<ComponentRead>(componentReads);
            }
        }

        public class ComponentRead
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public double Distance { get; set; }
            public double Weight { get; set; }
            public ComponentType ComponentType { get; set; }
            public DateTimeOffset AddedOn { get; set; }
            public DateTimeOffset? RetiredOn { get; set; }
        }
    }
}