using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Apis.Core.Security
{
    public interface IMembershipService<TKey>
    {
        bool IsAutenticated { get; }
        TKey UserId { get; }
    }
}
