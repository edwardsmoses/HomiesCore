using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPersist.Seed
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>() {
                    new AppUser() {
                        DisplayName = "Eddy",
                    UserName = "eddy",
                    Email = "edwardsmoses@gmail.com" } ,
                  new AppUser()
                  {
                      DisplayName = "Mike",
                      UserName = "mike",
                      Email = "mikeddy12@gmail.com"
                  }
            };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Categories.Any())
            {
                var activities = new List<Category>
                {
                    new Category
                    {
                        Name = "Chinese Meals",
                         CreatedOn = DateTime.Now
                    },
                  new Category()
                  {
                      Name = "Nigerian Meals",
                      CreatedOn = DateTime.Now
                  }
                };

                context.Categories.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}