using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    // This class represents an issue report with various details.
    public class IssueReport
    {
        // Unique number for the report
        public int ReportNumber { get; set; }

        // Title of the issue report
        public string Title { get; set; }

        // Location where the issue is reported
        public string Location { get; set; }

        // Category of the issue
        public string Category { get; set; }

        // Detailed description of the issue
        public string Description { get; set; }

        // List of file paths for attachments related to the issue
        public List<string> Attachments { get; set; } = new List<string>();

        // Feedback provided for the issue report
        public string Feedback { get; set; }

        // Date when the issue report was created
        public DateTime Date { get; set; }

        // Priority level of the issue
        public int Priority { get; set; }

        // Status of the issue report based on the time elapsed since creation
        public string Status
        {
            get
            {
                // If more than a minute has passed since the report was created, it's marked as "Succeeded", otherwise "Pending"
                return (DateTime.Now - Date).TotalMinutes > 1 ? "Succeeded" : "Pending";
            }
        }

        // List of image file paths related to the issue
        public List<string> ImagePaths { get; set; } = new List<string>();

        // Default constructor
        public IssueReport() { }
    }
}