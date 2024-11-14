using System.Collections.Generic;
using MunicipalService.Classes;

namespace MunicipalService
{
    public static class ReportStorage
    {
        private static List<IssueReport> issueReports = new List<IssueReport>();

        public static void AddReport(IssueReport report)
        {
            issueReports.Add(report);
        }

        public static List<IssueReport> GetReports()
        {
            return new List<IssueReport>(issueReports);
        }
    }
}
