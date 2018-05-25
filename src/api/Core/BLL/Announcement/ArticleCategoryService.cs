using System;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class ArticleCategoryService : EntityService<ArticleCategory>
    {
        public ArticleCategoryService(IEntityRepository<ArticleCategory> repository) : base(repository)
        {
            
        }
    }
}