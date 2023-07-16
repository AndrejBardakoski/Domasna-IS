using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.DTO
{
    public class EmailDTO : BaseEntity
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
