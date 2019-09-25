using System;

public class HistoryEntity
{
    public int ID { get; set; }
    public int CustomerID { get; set; }
    public int SellerID { get; set; }
    public int StockID { get; set; }
    public string StockType { get; set; }
    public int StockAmount { get; set; }
    public decimal TransactionCost { get; set; }
    public DateTime TransactionTime { get; set; }
}
