using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataPersist;
using Domain;
using Domain.API;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Foods

{
    public class List
    {
        public class Query : IRequest<List<Domain.API.FoodApiModel>> { }

        public class Handler : IRequestHandler<Query, List<FoodApiModel>>
        {
            private readonly DataContext context;
            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<FoodApiModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var foods = await context.Foods.ToListAsync();

                var foodMapper = new Mappers.FoodApiMapper(this.context);

                var apiFoods = new List<FoodApiModel>();
                foreach (var c in foods)
                {
                    var apiModel = await foodMapper.MapFoodToApiModel(c);
                    apiFoods.Add(apiModel);
                }

                return apiFoods;
            }
        }
    }
}