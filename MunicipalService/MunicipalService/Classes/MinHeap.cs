using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    // This class implements a MinHeap specifically for IssueReport objects.
    public class MinHeap
    {
        // List to store the heap elements
        private List<IssueReport> heap = new List<IssueReport>();

        // Method to insert a new issue report into the heap
        public void Insert(IssueReport report)
        {
            heap.Add(report);
            HeapifyUp(heap.Count - 1);
        }

        // Method to maintain the heap property after insertion
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].Priority >= heap[parentIndex].Priority) break;

                // Swap the current element with its parent
                var temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;

                index = parentIndex;
            }
        }

        // Method to extract the minimum element (root) from the heap
        public IssueReport ExtractMin()
        {
            if (heap.Count == 0) return null;
            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

        // Method to maintain the heap property after extraction
        private void HeapifyDown(int index)
        {
            while (true)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int smallestIndex = index;

                if (leftChildIndex < heap.Count && heap[leftChildIndex].Priority < heap[smallestIndex].Priority)
                    smallestIndex = leftChildIndex;
                if (rightChildIndex < heap.Count && heap[rightChildIndex].Priority < heap[smallestIndex].Priority)
                    smallestIndex = rightChildIndex;

                if (smallestIndex == index) break;

                // Swap the current element with the smallest child
                var temp = heap[index];
                heap[index] = heap[smallestIndex];
                heap[smallestIndex] = temp;

                index = smallestIndex;
            }
        }

        // Method to get all issue reports in the heap
        public List<IssueReport> GetAllReports()
        {
            return heap;
        }
    }
}
