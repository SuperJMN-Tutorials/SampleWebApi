﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiEjemplo.Controllers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiEjemplo.Features.Activities
{
    public class GetAll
    {
        public class ListActivitiesRequest : IRequest<ListActivitiesResponse>
        {
            public bool IsSortingDescending { get; set; }
        }

        public class ListActivitiesResponse : Collection<ActivityRead>
        {
            public ListActivitiesResponse(IEnumerable<ActivityRead> items) : base(items.ToList())
            {
            }
        }

        public class Handler : IRequestHandler<ListActivitiesRequest, ListActivitiesResponse>
        {
            private readonly StravaContext context;
            private readonly IConfigurationProvider provider;

            public Handler(StravaContext context, IConfigurationProvider provider)
            {
                this.context = context;
                this.provider = provider;
            }

            public async Task<ListActivitiesResponse> Handle(ListActivitiesRequest request, CancellationToken cancellationToken)
            {
                var source = context.Activities.AsQueryable();

                if (request.IsSortingDescending)
                {
                    source = source.OrderByDescending(x => x.Id);
                }
                else
                {
                    source = source.OrderBy(x => x.Id);
                }

                var activities = await source
                    .ProjectTo<ActivityRead>(provider)
                    .ToListAsync(cancellationToken);

                return new ListActivitiesResponse(activities);
            }
        }
    }
}