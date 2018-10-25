using System.Data.Entity;
using ApartmentsWeb.Models;

namespace ApartmentsWeb.DAL
{
	/// <summary>
    /// Appends requests made to the actual database itself
    /// </summary>
	public class OrderContext : DbContext
    {
		public OrderContext() : base("name=OurOrders")
        {

        }

		public virtual DbSet<OrderSheet> Order { get; set; }
    }
}
