using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    // This class represents a graph structure to manage issue reports and their dependencies.
    public class ReportGraph
    {
        // Dictionary to store the adjacency list of the graph
        private Dictionary<int, List<int>> adjacencyList;
        // Dictionary to store the priorities of the reports
        private Dictionary<int, int> reportPriorities;

        // Constructor to initialize the dictionaries
        public ReportGraph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
            reportPriorities = new Dictionary<int, int>();
        }

        // Method to add a new report to the graph
        public void AddReport(int reportId, int priority)
        {
            if (!adjacencyList.ContainsKey(reportId))
            {
                adjacencyList[reportId] = new List<int>();
            }
            // Set the priority for the report
            reportPriorities[reportId] = priority;
        }

        // Method to add a dependency between two reports
        public void AddDependency(int reportId, int dependentReportId)
        {
            if (adjacencyList.ContainsKey(reportId) && adjacencyList.ContainsKey(dependentReportId))
            {
                adjacencyList[reportId].Add(dependentReportId);
            }
        }

        // Method to get the list of dependencies for a given report
        public List<int> GetDependencies(int reportId)
        {
            return adjacencyList.ContainsKey(reportId) ? adjacencyList[reportId] : new List<int>();
        }

        // Method to get the resolution time for a given report based on its priority
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
