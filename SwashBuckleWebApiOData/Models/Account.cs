using System;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Query;

namespace SwashBuckleWebApiOData.Models
{
    [Select(SelectType = SelectExpandType.Automatic)]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        [Required]
        public System.Net.Mail.MailAddress Email { get; set; }

        [Required]
        public string AccountName { get; set; }

        public string PasswordHash { get; set; }

        [Phone]
        public string MobilePhone { get; set; }

    }
}
