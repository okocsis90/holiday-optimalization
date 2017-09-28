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
                if (!outputNodes.Contains(currentNode)){
                    while (currentNode.Previous != null)
                    {
                        destinationTracker.Push(currentNode);
                        currentNode = currentNode.Previous;
                    }
                    destinationTracker.Push(currentNode);
                    while (destinationTracker.Count > 0)
                    {
                        outputNodes.Add(destinationTracker.Pop());
                    }
                }
            }
            return outputNodes;
        }
    }
}
