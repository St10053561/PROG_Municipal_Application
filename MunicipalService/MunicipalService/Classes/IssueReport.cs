﻿using System;
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
        public List<string> Attachments { get; set; } = new List<string>(); // Change to List<string>
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
        public string Status
        {
            get
            {
                return (DateTime.Now - Date).TotalDays > 2 ? "Succeeded" : "Pending";
            }
        }
        public List<string> ImagePaths { get; set; } = new List<string>(); // Change to List<string>

        // Empty constructor
        public IssueReport()
        {
        }
    }
}
