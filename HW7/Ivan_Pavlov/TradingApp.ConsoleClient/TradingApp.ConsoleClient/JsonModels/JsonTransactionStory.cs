namespace TradingApp.ConsoleClient.JsonModels
{
    using System;

    public class JsonTransactionStory
    {
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int ShareId { get; set; }
        public int AmountOfShares { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TransactionCost { get; set; }

        //public virtual JsShare Share { get; set; }
        public override string ToString()
        {
            return $"Продавец {SellerId} Покупатель {CustomerId} Акция {SellerId} Кол-во {AmountOfShares} Общая стоимость {TransactionCost}";
        }
    }
}
