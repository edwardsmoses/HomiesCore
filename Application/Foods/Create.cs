using DataPersist;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Foods
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }

            public string Price { get; set; }

            public string CategoryId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.Name).NotEmpty();
                RuleFor(m => m.Description).NotEmpty();
                RuleFor(m => m.Price).NotEmpty();
                RuleFor(m => m.CategoryId).NotEmpty();
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
                var food = new Food()
                {
                    Id = request.Id,
                    IsMealOfTheDay = false,
                    Description = request.Description,
                    Name = request.Name,
                    Price = Convert.ToDecimal(request.Price),
                    Currency = Currency.Naira,
                    CreatedOn = DateTime.Now,
                    CanFoodShowOnApp = true //remove this later, and only show the food when the Admin is ready to show it
                };

                var categoryDbModel = await context.Categories.FirstOrDefaultAsync(m => m.Id == Guid.Parse(request.CategoryId));
                if (categoryDbModel != null)
                    food.CategoryId = categoryDbModel.Id;
                else
                    food.CategoryName = request.CategoryId;

                context.Foods.Add(food);
                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Foods");
            }
        }
    }
}