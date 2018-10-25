using System.Data.Entity;
using ApartmentsWeb.Models;

namespace ApartmentsWeb.DAL
{
	/// <summary>
    /// Appends requests made to the actual database itself
    /// </summary>
	public class OrderContext : DbContext
    {
		//base points to the name of the database
		public OrderContext() : base("name=WorkOrders")
        {

        }

		public virtual DbSet<OrderSheet> OrderSheets { get; set; }
    }
}
