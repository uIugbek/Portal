using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class CityService : EntityService<City>
    {
        public CityService(IEntityRepository<City> repository) : base(repository)
        {
        }
    }
}