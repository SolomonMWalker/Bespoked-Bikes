using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
