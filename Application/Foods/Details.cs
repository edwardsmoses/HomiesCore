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
        public class Query : IRequest<Food>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Food>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Food> Handle(Query request, CancellationToken cancellationToken)
            {
                var Activity = await context.Foods.FindAsync(request.Id);
                return Activity;
            }
        }
    }
}