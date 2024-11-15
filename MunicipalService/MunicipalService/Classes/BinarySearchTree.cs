using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class BinarySearchTree
    {
        private class Node
        {
            public IssueReport Report;
            public Node Left;
            public Node Right;

            public Node(IssueReport report)
            {
                Report = report;
                Left = Right = null;
            }
        }

        private Node root;

        public void Insert(IssueReport report)
        {
            root = Insert(root, report);
        }

        private Node Insert(Node node, IssueReport report)
        {
            if (node == null) return new Node(report);
            if (report.ReportNumber < node.Report.ReportNumber)
                node.Left = Insert(node.Left, report);
            else
                node.Right = Insert(node.Right, report);
            return node;
        }

        public IssueReport Search(int reportNumber)
        {
            return Search(root, reportNumber);
        }

        private IssueReport Search(Node node, int reportNumber)
        {
            if (node == null) return null;
            if (reportNumber == node.Report.ReportNumber) return node.Report;
            return reportNumber < node.Report.ReportNumber ? Search(node.Left, reportNumber) : Search(node.Right, reportNumber);
        }
    }
}
