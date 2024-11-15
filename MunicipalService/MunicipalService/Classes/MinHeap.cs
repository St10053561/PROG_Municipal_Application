using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class MinHeap
    {
        private List<IssueReport> heap = new List<IssueReport>();

        public void Insert(IssueReport report)
        {
            heap.Add(report);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].Priority >= heap[parentIndex].Priority) break;

                // Swap
                var temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;

                index = parentIndex;
            }
        }

        public IssueReport ExtractMin()
        {
            if (heap.Count == 0) return null;
            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

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

                // Swap
                var temp = heap[index];
                heap[index] = heap[smallestIndex];
                heap[smallestIndex] = temp;

                index = smallestIndex;
            }
        }

        public List<IssueReport> GetAllReports()
        {
            return heap;
        }
    }
}
