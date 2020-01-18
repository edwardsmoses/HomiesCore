using DataPersist;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FoodCategory
{
    public class Create
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
                var checkExists = await context.Categories.AnyAsync(m => m.Name.ToLower() == request.Name.ToLower());
                if (checkExists)
                    throw new Exception("This Category already exists already.");

                var foodCategory = new Category()
                {
                    Id = request.Id,
                    Name = request.Name
                };

                context.Categories.Add(foodCategory);
                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Food Categories");
            }
        }
    }
}