using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.API
{
    public class FoodApiModel
    {


        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string PriceInCurrency
        {
            get
            {
                return $"{CurrencySymbol} {string.Format("{0:n}", Price)}";
            }
        }

        public decimal Price { get; set; }
        public string CurrencySymbol
        {
            get
            {
                if (Currency == "Naira")
                    return "NGN";
                else
                    return "$";
            }
        }

        public string Currency { get; set; }
        public string CategoryName { get; set; }
        public string PictureUrl { get; set; }

        public string FullPictureUrl
        {
            get
            {
                return $"https://localhost:44355//Content/uploads//{PictureUrl}";
            }
        }

        public List<string> Pictures { get; set; }


        public static FoodApiModel MapSingleFoodToApiModel(Food foodDbModel)
        {
            if (foodDbModel == null)
                return null;

            var foodApiModel = new FoodApiModel()
            {
                CategoryName = foodDbModel.Category.Name,
                Currency = foodDbModel.Currency.ToString(),
                Description = foodDbModel.Description,
                Id = foodDbModel.Id,
                Name = foodDbModel.Name,
                Price = foodDbModel.Price,
                Pictures = foodDbModel.Pictures.Select(m => m.FileName).ToList()
            };
            if (foodDbModel.Pictures.Any())
                foodApiModel.PictureUrl = foodDbModel.Pictures.FirstOrDefault().FileName;
            else
                foodApiModel.PictureUrl = "defaultFood.jpg";

            return foodApiModel;
        }
    }
}
