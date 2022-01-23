using Rental.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental.Core.Contracts
{
    public interface IEntity
    {
         int Id { get; set; }
         DateTime CreatedAt { get; set; }
         DateTime? ModifiedAt { get; set; }
         bool SoftDeleted { get; set; }
    }
}
