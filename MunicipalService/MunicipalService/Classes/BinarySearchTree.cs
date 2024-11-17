using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class BinarySearchTree
    {
        // This nested class represents a node in our binary search tree
        private class Node
        {
            // Each node holds an issue report
            public IssueReport Report;
            // References to the left and right child nodes
            public Node Left;
            public Node Right;

            // Constructor to create a new node with an issue report
            public Node(IssueReport report)
            {
                Report = report; // Store the report in the node
                Left = Right = null; // Initialize left and right children as null
            }
        }

        // The root node of our binary search tree
        private Node root;

        // Method to insert a new issue report into the tree
        public void Insert(IssueReport report)
        {
            // Start the insertion process from the root
            root = Insert(root, report);
        }

        // Recursive method to handle the insertion of a new issue report
        private Node Insert(Node node, IssueReport report)
        {
            // If we find a null spot, create a new node here
            if (node == null) return new Node(report);

            // Decide whether to go left or right based on the report number
            if (report.ReportNumber < node.Report.ReportNumber)
                node.Left = Insert(node.Left, report); // Go left
            else
                node.Right = Insert(node.Right, report); // Go right

            // Return the current node to link it back to its parent
            return node;
        }

        // Method to search for an issue report by its report number
        public IssueReport Search(int reportNumber)
        {
            // Start the search from the root
            return Search(root, reportNumber);
        }

        // Recursive method to search for an issue report
        private IssueReport Search(Node node, int reportNumber)
        {
            // If we hit a null node, the report isn't in the tree
            if (node == null) return null;

            // If we find the report number, return the report
            if (reportNumber == node.Report.ReportNumber) return node.Report;

            // Decide whether to search left or right based on the report number
            return reportNumber < node.Report.ReportNumber ? Search(node.Left, reportNumber) : Search(node.Right, reportNumber);
        }
    }
}
