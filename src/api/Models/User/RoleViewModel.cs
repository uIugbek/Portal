using System.Collections.Generic;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int UsersCount { get; set; }
        
        public string[] Permissions { get; set; }
    }
}