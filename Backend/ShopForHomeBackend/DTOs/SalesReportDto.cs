// Backend/DTOs/SalesReportDto.cs
using System.Collections.Generic;

namespace ShopForHomeBackend.DTOs
{
    public class SalesReportDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<OrderSummaryDto> Orders { get; set; }
    }

    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderDate { get; set; }
    }
}
