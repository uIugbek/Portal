using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class CountryService : EntityService<Country>
    {
        public CountryService(IEntityRepository<Country> repository) : base(repository)
        {
        }
    }
}