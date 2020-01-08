using System;
using System.Threading;
using System.Threading.Tasks;
using DataPersist;
using MediatR;

namespace Application.Foods

{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }

            public decimal Price { get; set; }

            public string CategoryName { get; set; }
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
                    throw new Exception("Couldn't find the Activity");

                food.Name = request.Name ?? food.Name;
                food.Description = request.Description ?? food.Description;
                food.Price = request.Price;


                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Activities");
            }
        }
    }
}