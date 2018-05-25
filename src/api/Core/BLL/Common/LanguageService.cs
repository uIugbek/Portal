using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class LanguageService : EntityService<Language>
    {
        public LanguageService(IEntityRepository<Language> repository) : base(repository)
        {
        }
    }
}