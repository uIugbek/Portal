using System;

namespace Portal.Apis.Core.DAL.Entities
{
    public class Culture : Entity<int>
    {
        public string Code { get; set; }
        
        public string Icon { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}