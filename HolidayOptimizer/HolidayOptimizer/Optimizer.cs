using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimizer
{
    public class Optimizer
    {
        public static List<DestinationNode> Optimize(List<DestinationNode> inputNodes)
        {
            if (inputNodes.Count == 0)
            {
                throw new ArgumentException("Input list is empty, please add Destinations to your list.");
            }

            bool inputIsEqualToOutput = true;
            foreach (DestinationNode destination in inputNodes)
            {
                if (destination.Previous != null)
                {
                    inputIsEqualToOutput = false;
                    break;
                }
            }
            if (inputIsEqualToOutput)
            {
                return inputNodes;
            }

            List<DestinationNode> outputNodes = new List<DestinationNode>();
            Stack<DestinationNode> destinationTracker = new Stack<DestinationNode>();
            DestinationNode currentNode;
            for (int i = 0; i < inputNodes.Count; i++)
            {
                currentNode = inputNodes[i];
                while (currentNode.Previous != null)
                {
                    if (destinationTracker.Contains(currentNode))
                    {
                        throw new ArgumentException("Circular dependency is not allowed. Please check your input nodes.");
                    }
                    if (outputNodes.Contains(currentNode.Previous) && (currentNode.Previous.Next != currentNode && currentNode.Previous.Next != null))
                    {
                        throw new ArgumentException("One Destination cannot be the previous Destination for multiple Destinations.");
                    }
                    destinationTracker.Push(currentNode);
                    DestinationNode savedCurrent = currentNode;
                    currentNode = currentNode.Previous;
                    currentNode.Next = savedCurrent;
                }
                destinationTracker.Push(currentNode);
                while (destinationTracker.Count > 0)
                {
                    if (!outputNodes.Contains(destinationTracker.Peek()))
                    {
                        outputNodes.Add(destinationTracker.Pop());
                    } else
                    {
                        destinationTracker.Pop();
                    }
                }

            }
            return outputNodes;
        }
    }
}
