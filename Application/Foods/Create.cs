using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataPersist;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Foods
{
    public class Create
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

            public async Task<Category> GetCategoryAsync(string categoryName)
            {
                var category = await context.Categories.FirstOrDefaultAsync(m => m.Name.Equals(categoryName,
                    StringComparison.InvariantCultureIgnoreCase));

                if (category == null)
                {
                    category = new Category()
                    {
                        Name = categoryName,
                    };
                    context.Categories.Add(category);
                    await context.SaveChangesAsync();
                };

                return category;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await GetCategoryAsync(request.CategoryName);

                var food = new Food()
                {
                    CategoryId = category.Id,
                    IsMealOfTheDay = false,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price,
                    Currency = Currency.Naira,
                    CanFoodShowOnApp = true //remove this later, and only show the food when the Admin is ready to show it
                };

                context.Foods.Add(food);
                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Foods");
            }
        }
    }
}