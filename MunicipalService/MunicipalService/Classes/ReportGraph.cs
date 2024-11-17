using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class ReportGraph
    {
        private Dictionary<int, List<int>> adjacencyList;
        private Dictionary<int, int> reportPriorities;

        public ReportGraph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
            reportPriorities = new Dictionary<int, int>();
        }

        public void AddReport(int reportId, int priority)
        {
            if (!adjacencyList.ContainsKey(reportId))
            {
                adjacencyList[reportId] = new List<int>();
            }
            reportPriorities[reportId] = priority; // Set the priority based on the IssueReport's Priority property
        }

        public void AddDependency(int reportId, int dependentReportId)
        {
            if (adjacencyList.ContainsKey(reportId) && adjacencyList.ContainsKey(dependentReportId))
            {
                adjacencyList[reportId].Add(dependentReportId);
            }
        }

        public List<int> GetDependencies(int reportId)
        {
            return adjacencyList.ContainsKey(reportId) ? adjacencyList[reportId] : new List<int>();
        }

        public int GetResolutionTime(int reportId)
        {
            if (reportPriorities.TryGetValue(reportId, out int priority))
            {
                // Higher priority (1) gets a shorter resolution time
                return priority == 0 ? 2 : 1; // 1 minute for high priority, 2 minutes for normal
            }
            throw new ArgumentException("Report ID not found in report priorities.");
        }
    }
}
