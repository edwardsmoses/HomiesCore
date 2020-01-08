using System;
using System.Threading;
using System.Threading.Tasks;
using DataPersist;
using Domain;
using MediatR;

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
                return Domain.API.FoodApiModel.MapSingleFoodToApiModel(Meal);
            }
        }
    }
}