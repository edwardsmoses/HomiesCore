using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using DataPersist;
using FluentValidation;
using MediatR;

namespace Application.FoodCategory

{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.Name).NotEmpty();
            }
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
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This food Category couldn't be found" });


                foodCategory.Name = request.Name ?? foodCategory.Name;

                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Category");
            }
        }
    }
}