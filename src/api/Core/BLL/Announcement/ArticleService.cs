using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    
    public class ArticleService : EntityService<Article>
    {
        public ArticleService(IEntityRepository<Article> repository) : base(repository)
        {

        }
    }
}
