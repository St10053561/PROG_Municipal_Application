using System.Collections.Generic;
using MunicipalService.Classes;

namespace MunicipalService
{
    // This static class is used to store and manage issue reports.
    public static class ReportStorage
    {
        // A private list to hold all the issue reports
        private static List<IssueReport> issueReports = new List<IssueReport>();

        // Method to add a new issue report to the storage
        public static void AddReport(IssueReport report)
        {
            issueReports.Add(report);
        }

        // Method to retrieve all the issue reports from the storage
        public static List<IssueReport> GetReports()
        {
            // Return a new list containing all the issue reports to avoid external modifications
            return new List<IssueReport>(issueReports);
        }
    }
}
