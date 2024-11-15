using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class IssueReport
    {
        public int ReportNumber { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> Attachments { get; set; } = new List<string>();
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
        public string Status
        {
            get
            {
                return (DateTime.Now - Date).TotalMinutes > 1 ? "Succeeded" : "Pending";
            }
        }
        public List<string> ImagePaths { get; set; } = new List<string>();
        public int Priority { get; set; }

        public IssueReport() { }
    }
}
