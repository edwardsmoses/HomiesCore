using DataPersist;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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