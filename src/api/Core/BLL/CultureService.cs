using Portal.Apis.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.Apis.Core.Helpers;
using AutoMapper;

namespace Portal.Apis.Core.BLL
{
    public class CultureService : EntityService<Culture>
    {
        public CultureService(IEntityRepository<Culture> repository) : base(repository)
        {
        }
    }
}
