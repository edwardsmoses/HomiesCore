using Application.Errors;
using DataPersist;
using Domain;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FoodCategory

{
    public class Details
    {
        public class Query : IRequest<Category>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Category>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Category> Handle(Query request, CancellationToken cancellationToken)
            {
                var foodCategory = await context.Categories.FindAsync(request.Id);

                if (foodCategory == null)
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This Category couldn't be found" });


                return foodCategory;
            }
        }
    }
}