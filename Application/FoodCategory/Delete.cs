using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using DataPersist;
using MediatR;

namespace Application.FoodCategory

{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var foodCategory = await context.Categories.FindAsync(request.Id);
                if (foodCategory == null)
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This Food Category couldn't be found" });

                context.Categories.Remove(foodCategory);

                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Category");
            }
        }

    }
}