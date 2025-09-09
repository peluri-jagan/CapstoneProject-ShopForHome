// Backend/Services/IReportService.cs
using System;
using System.Threading.Tasks;
using ShopForHomeBackend.DTOs;

namespace ShopForHomeBackend.Services
{
    public interface IReportService
    {
        Task<SalesReportDto> GenerateSalesReportAsync(DateTime fromDate, DateTime toDate);
    }
}
