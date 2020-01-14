using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataPersist;
using Domain;
using Domain.API;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FoodCategory

{
    public class List
    {
        public class Query : IRequest<List<Category>> { }

        public class Handler : IRequestHandler<Query, List<Category>>
        {
            private readonly DataContext context;
            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<Category>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await context.Categories.ToListAsync();

                return categories;
            }
        }
    }
}