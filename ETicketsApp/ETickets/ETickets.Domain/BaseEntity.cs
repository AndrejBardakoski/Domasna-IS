using System;
using System.ComponentModel.DataAnnotations;

namespace ETickets.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
