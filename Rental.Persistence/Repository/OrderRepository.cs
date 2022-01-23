using Rental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Persistence.Repository
{
    internal class OrderRepository:BaseRepository<Order>
    {
        public OrderRepository(RentalContext context) : base(context)
        {

        }


    }
}
