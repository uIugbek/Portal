using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.Apis.Core.DAL.Entities
{
    public interface IAuditableEntity<TKeyUser>
    {
        DateTime CreatedDate { get; set; }

        TKeyUser CreatedBy { get; set; }

        DateTime UpdatedDate { get; set; }

        TKeyUser UpdatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
    
    // public interface IAuditableEntity<TKey, TKeyUser> : IEntity<TKey>
    //     where TKey : struct
    //     where TKeyUser : struct
    // {
        
    // }

    public abstract class AuditableEntity<TKeyUser> : BaseEntity, IAuditableEntity<TKeyUser>
        where TKeyUser : struct
    {
        public DateTime CreatedDate { get; set; }

        public TKeyUser CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public TKeyUser UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}