
using Rental.Core.Contracts;
using System;

namespace Rental.Core.Models
{
    public class BaseModel : IEntity
    {
        
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
