using DataPersist;
using Domain.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class FoodApiMapper
    {
        private readonly DataContext context;

        public FoodApiMapper(DataContext context)
        {
            this.context = context;
        }

        private Domain.Food Food { get; set; }

        public async Task<Domain.API.FoodApiModel> MapFoodToApiModel(Domain.Food food)
        {
            this.Food = food;

            var foodApiModel = new FoodApiModel()
            {
                Currency = this.Food.Currency.ToString(),
                Description = this.Food.Description,
                Id = this.Food.Id,
                Name = this.Food.Name,
                Price = this.Food.Price,
                CreatedOn = this.Food.CreatedOn,
                Pictures = this.Food.Pictures != null ? this.Food.Pictures.Select(m => m.FileName).ToList() : new List<string>()
            };

            var categoryModel = await this.context.Categories.FindAsync(this.Food.CategoryId);
            if (categoryModel != null)
            {
                foodApiModel.CategoryId = categoryModel.Id;
                foodApiModel.CategoryName = categoryModel.Name;
            }
            else
                foodApiModel.CategoryName = foodApiModel.CategoryName ?? "Meals";

            if (this.Food == null && this.Food.Pictures.Any())
                foodApiModel.PictureUrl = this.Food.Pictures.FirstOrDefault().FileName;
            else
                foodApiModel.PictureUrl = "defaultFood.jpg";

            return foodApiModel;
        }
    }
}
