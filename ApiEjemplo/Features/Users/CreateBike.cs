using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo;
using ApiEjemplo.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikingUltimate.Server.Features.Users
{
    public class CreateBike
    {
        public class Request : IRequest<int>
        {
            public int UserId { get; }
            public BikeInfo BikeInfo { get; }

            public Request(int userId, BikeInfo bikeInfo)
            {
                UserId = userId;
                BikeInfo = bikeInfo;
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
                var user = context.Users
                    .Include(b => b.Bikes)
                    .FirstOrDefault(u => u.Id == request.UserId);

                if (user != null)
                {
                    var bike = mapper.Map<Bike>(request.BikeInfo);
                    user.Bikes.Add(bike);
                    await context.SaveChangesAsync(cancellationToken);
                    return bike.Id;
                }
                else
                {
                    throw new InvalidOperationException("El id del usuario no existe");
                }
            }
        }

        public class BikeInfo
        {
            public BikeInfo(string brand, string model)
            {
                Brand = brand ?? throw new ArgumentNullException(nameof(brand));
                Model = model ?? throw new ArgumentNullException(nameof(model));
            }

            public string Brand { get; }
            public double Distance { get; set; }
            public string Model { get; }
        }
    }
}