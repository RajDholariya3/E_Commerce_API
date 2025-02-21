using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Data;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashboardController(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("Metrics")]
        public IActionResult GetDashboardMetrics()
        {
            var dashboard = _dashboardRepository.GetDashboardMetrics();
            return Ok(dashboard);
        }

        [HttpGet("Revenue")]
        public IActionResult GetDashboardRevenue()
        {
            var revenue = _dashboardRepository.GetYearWiseRevenueData();
            return Ok(revenue);
        }

      

        [HttpGet("PaymentsSummary")]
        public IActionResult GetPaymentsSummary()
        {
            var payments = _dashboardRepository.GetPaymentsSummary();
            return Ok(payments);
        }

       

        [HttpGet("TransactionsSummary")]
        public IActionResult GetTransactionsSummary()
        {
            var transactionsSummary = _dashboardRepository.GetTransactionsSummary();
            return Ok(transactionsSummary);
        }
    }
}