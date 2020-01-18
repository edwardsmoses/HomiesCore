using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    public class BaseEntity : AuditInfo
    {
        [Key]
        public Guid Id { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
