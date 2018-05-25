using System.Collections.Generic;

namespace Portal.Apis.Core.DAL.Entities
{
    public class Language : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}