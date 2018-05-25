using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Enums;
using Portal.Apis.Core.Extensions;
using Portal.Apis.Models;

namespace Portal.Apis.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Comment { get; set; }
        public Age? Age { get; set; }
        public string Photo { get; set; }
        public string PostalAddress { get; set; }
        public int? ZIP { get; set; }
        public int? CountryId { get; set; }
        public long? FacebookId { get; set; }
        public FileModel Avatar { get; set; }

        public string[] Roles { get; set; }

    }
}