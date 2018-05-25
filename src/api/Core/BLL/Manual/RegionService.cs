using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class RegionService : EntityService<Region>
    {
        public RegionService(IEntityRepository<Region> repository) : base(repository)
        {
        }
    }
}