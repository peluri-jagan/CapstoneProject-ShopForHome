// Backend/Services/ReportService.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SalesReportDto> GenerateSalesReportAsync(DateTime fromDate, DateTime toDate)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .ToListAsync();

            var report = new SalesReportDto
            {
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                Orders = orders.Select(o => new OrderSummaryDto
                {
                    OrderId = o.Id,
                    Username = o.User.Username,
                    TotalAmount = o.TotalAmount,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd")
                }).ToList()
            };

            return report;
        }
    }
}
