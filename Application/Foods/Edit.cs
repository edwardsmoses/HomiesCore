using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using DataPersist;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Foods

{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string CategoryId { get; set; }

            public string Description { get; set; }

            public string Price { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.Name).NotEmpty();
                RuleFor(m => m.Description).NotEmpty();
                RuleFor(m => m.Price).NotEmpty();
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
                var food = await context.Foods.FindAsync(request.Id);
                if (food == null)
                    throw new RestException(HttpStatusCode.NotFound, new { food = "This meal couldn't be found" });


                food.Name = request.Name ?? food.Name;
                food.Description = request.Description ?? food.Description;
                food.Price = Convert.ToDecimal(request.Price);
                food.ModifiedOn = DateTime.Now;


                var categoryDbModel = await context.Categories.FirstOrDefaultAsync(m => m.Id == Guid.Parse(request.CategoryId));
                if (categoryDbModel != null)
                    food.CategoryId = categoryDbModel.Id;
                else
                    food.CategoryName = request.CategoryId;



                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Activities");
            }
        }
    }
}