using Domain.Base;
using System;

namespace Domain
{
    public class FoodPicture : BaseEntity
    {
        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public long Length { get; set; }

        public string ContentType { get; set; }

        public Guid FoodId { get; set; }

        public virtual Food Food { get; set; }
    }
}