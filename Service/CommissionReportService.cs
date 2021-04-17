using Data;
using Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public class CommissionReportService
    {
        private static Queries queries;

        public CommissionReportService()
        {
            queries = queries ?? new Queries();
        }

        private Tuple<DateTime, DateTime> GetStartAndEndDateForQuarter(int quarter, int year)
        {
            DateTime startDate, endDate;

            switch (quarter)
            {
                case 1:
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 3, 31);
                    break;

                case 2:
                    startDate = new DateTime(year, 4, 1);
                    endDate = new DateTime(year, 6, 30);
                    break;

                case 3:
                    startDate = new DateTime(year, 7, 1);
                    endDate = new DateTime(year, 9, 30);
                    break;

                default:
                    startDate = new DateTime(year, 10, 1);
                    endDate = new DateTime(year, 12, 31);
                    break;
            }

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        public List<SalespersonCommissionReport> GetCommissionReport(int quarter, int year)
        {
            var dateTuple = GetStartAndEndDateForQuarter(quarter, year);

            return queries.GetQuarterlyCommissionReport(dateTuple.Item1, dateTuple.Item2, quarter, year);
        }
    }
}