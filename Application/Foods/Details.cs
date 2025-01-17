using Application.Errors;
using DataPersist;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Foods

{
    public class Details
    {
        public class Query : IRequest<Domain.API.FoodApiModel>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Domain.API.FoodApiModel>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Domain.API.FoodApiModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var Meal = await context.Foods.FindAsync(request.Id);

                if (Meal == null)
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This meal couldn't be found" });

                var foodMapper = new Mappers.FoodApiMapper(this.context);
                return await foodMapper.MapFoodToApiModel(Meal);
            }
        }
    }
}