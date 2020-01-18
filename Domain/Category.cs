using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category : BaseEntity
    {

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}