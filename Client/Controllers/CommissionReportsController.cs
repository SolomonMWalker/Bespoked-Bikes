using Microsoft.AspNetCore.Mvc;
using Service;

namespace Client.Controllers
{
    public class CommissionReportsController : Controller
    {
        private CommissionReportService commissionReportService;

        public CommissionReportsController(CommissionReportService crService)
        {
            commissionReportService = crService;
        }

        // GET: CommissionReportsController
        public ActionResult Index(int quarter, int year)
        {
            var commissionReports = commissionReportService.GetCommissionReport(quarter, year);
            return View("DisplayCommissionReportsView", commissionReports);
        }
    }
}