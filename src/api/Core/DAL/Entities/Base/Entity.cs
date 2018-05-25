using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Apis.Core.DAL.Entities
{
    public interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }

    public class Entity<TKey> : IEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }
    }

    public class BaseEntity : Entity<int>
    {
        
    }

    public class AuditableBaseEntity : AuditableEntity<int>
    {

    }
}
