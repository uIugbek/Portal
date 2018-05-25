using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class SkipAndTake 
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
