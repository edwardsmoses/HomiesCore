using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Category : BaseEntity
    {
       
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}