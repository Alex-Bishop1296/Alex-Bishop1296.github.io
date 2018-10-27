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
		// Db set needs to be singular, the actual name of this function needs to be plural
		public virtual DbSet<OrderSheet> OrderSheets { get; set; }
    }
}
