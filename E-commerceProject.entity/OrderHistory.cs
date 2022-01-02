

using System;

namespace E_commerceProject.entity
{
    public class OrderHistory
    {
        public int HistoryId { get; set; }
        public string Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}