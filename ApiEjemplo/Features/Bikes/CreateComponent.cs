using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiEjemplo.Features.Bikes
{
    public class CreateComponent
    {
        public class Request : IRequest<int>
        {
            public int BikeId { get; }
            public ComponentInfo ComponentInfo { get; }

            public Request(int bikeId, ComponentInfo componentInfo)
            {
                BikeId = bikeId;
                ComponentInfo = componentInfo;
            }
        }

        public class Handler : IRequestHandler<Request, int>
        {
            private readonly BikingContext context;
            private readonly IMapper mapper;

            public Handler(BikingContext context, IConfigurationProvider mappingConfig)
            {
                this.context = context;
                mapper = mappingConfig.CreateMapper();
            }

            public async Task<int> Handle(Request request, CancellationToken cancellationToken)
            {
                var bike = context.Bikes
                    .Include(b => b.Components)
                    .FirstOrDefault(b =>  b.Id == request.BikeId);

                if (bike != null)
                {
                    var component = mapper.Map<Component>(request.ComponentInfo);
                    bike.Components.Add(component);
                    await context.SaveChangesAsync(cancellationToken);
                    return component.Id;
                }
                else
                {
                    throw new InvalidOperationException("El id de bike no existe");
                }
            }
        }

        public class ComponentInfo
        {
            public ComponentInfo(string brand, string model, double distance, double weight, ComponentType componentType, DateTimeOffset addedOn)
            {
                Brand = brand;
                Model = model;
                Distance = distance;
                Weight = weight;
                ComponentType = componentType;
                AddedOn = addedOn;
            }

            public string Brand { get; set; }
            public string Model { get; set; }
            public double Distance { get; set; }
            public double Weight { get; set; }
            public ComponentType ComponentType { get; set; }
            public DateTimeOffset AddedOn { get; set; }
        }
    }
}