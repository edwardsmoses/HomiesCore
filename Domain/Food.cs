using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public enum Currency
    {
        Naira
    }

    public class Food : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Reciept { get; set; }

        public DateTime? LastDateSetAsMealOfTheDay { get; set; }

        public bool IsMealOfTheDay { get; set; }

        public bool CanFoodShowOnApp { get; set; }


        [Required]
        public decimal Price { get; set; }

        public Currency Currency { get; set; }

        public string CategoryName { get; set; }

        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<FoodPicture> Pictures { get; set; }
    }
}
