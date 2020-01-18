using Application.Errors;
using DataPersist;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Foods

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
                var food = await context.Foods.FindAsync(request.Id);
                if (food == null)
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This meal couldn't be found" });

                context.Foods.Remove(food);

                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Foods");
            }
        }

    }
}