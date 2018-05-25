using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Core.Security
{
    public class MembershipService : IMembershipService<int>
    {
        public MembershipService()
        {
        }

        public static MembershipService Instance { get { return ServiceProvider.GetService<MembershipService>(); } }

        public bool IsAutenticated { get; set; }

        public int UserId { get; set; }
    }
}
