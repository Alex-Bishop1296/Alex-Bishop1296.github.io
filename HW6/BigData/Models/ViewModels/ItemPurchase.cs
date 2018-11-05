namespace BigData.Models.ViewModels
{
    /// <summary>
    /// Model that holds the details for each particular item purchase from a particular customer
    /// </summary>
    public class ItemPurchase
    {
            public int StockItemID { get; set; }
            public string ItemDescription { get; set; }
            public decimal LineProfit { get; set; }
            public string SalesPerson { get; set; }
    }
}