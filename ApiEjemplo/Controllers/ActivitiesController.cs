using System.Threading.Tasks;
using ApiEjemplo.Features.Activities;
using BikingUltimate.Server.Features.Activities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiEjemplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<GetAll.ListActivitiesResponse> Get([FromQuery]GetAll.Query request)
        {
            return await mediator.Send(request);
        }

        [HttpPost]
        public async Task<int> Create(Create.Command request)
        {
            return await mediator.Send(request);
        }
        
        [HttpGet("{id}")]
        public async Task<ActivityRead> Get(int id)
        {
            return await mediator.Send(new Get.Query { Id = id });
        }

        [HttpPut("{id}")]
        public async Task Update(int id, Update.ActivityInfo activityInfo)
        {
            await mediator.Send(new Update.Command { Id = id, ActivityInfo = activityInfo });
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mediator.Send(new Delete.Command { Id = id });
        }
    }
}