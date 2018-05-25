using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class NewsService : EntityService<News>
    {
        public NewsService(IEntityRepository<News> repository) : base(repository)
        {

        }        
    }
}
