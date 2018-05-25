using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class NewsCategoryService : EntityService<NewsCategory>
    {
        public NewsCategoryService(IEntityRepository<NewsCategory> repository) : base(repository)
        {
            
        }
    }
}