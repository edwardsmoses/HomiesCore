using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Foods;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly IMediator mediator;
        public MealsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Domain.API.FoodApiModel>>> List()
        {
            return await mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> Details(Guid id)
        {
            return await mediator.Send(new Details.Query()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await mediator.Send(command);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await mediator.Send(new Delete.Command { Id = id });
        }

    }
}