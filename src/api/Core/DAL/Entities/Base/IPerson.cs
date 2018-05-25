using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Apis.Core.DAL.Entities
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
    }
}