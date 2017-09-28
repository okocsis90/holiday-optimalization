using System;
using System.Collections.Generic;

namespace HolidayOptimizer
{
    public class Optimizer
    {
        private List<DestinationNode> OutputNodes { get; set; }
        private List<DestinationNode> Nodes { get; set; }        

        public Optimizer(List<DestinationNode> inputNodes)
        {
            Nodes = inputNodes;
            OutputNodes = new List<DestinationNode>();
        }

        public List<DestinationNode> Optimize()
        {
            CheckInputValidity();
            if (CheckIfDependent())
            {
                return Nodes;
            }            
            Stack<DestinationNode> destinationTracker = new Stack<DestinationNode>();
            DestinationNode currentNode;
            for (int i = 0; i < Nodes.Count; i++)
            {
                currentNode = Nodes[i];
                while (currentNode.Previous != null)
                {
                    CheckCircularDependency(currentNode, destinationTracker);
                    CheckMultipleNextNodes(currentNode);
                    destinationTracker.Push(currentNode);
                    SwapCurrentNode(ref currentNode);
                }
                destinationTracker.Push(currentNode);
                while (destinationTracker.Count > 0)
                {
                    FillOutputFromStack(ref destinationTracker);
                }
            }
            return OutputNodes;
        }

        private void CheckInputValidity()
        {
            if (Nodes.Count == 0)
            {
                throw new ArgumentException("Input list is empty, please add Destinations to your list.");
            }
        }

        private void FillOutputFromStack(ref Stack<DestinationNode> destinationTracker)
        {
            if (!OutputNodes.Contains(destinationTracker.Peek()))
            {
                OutputNodes.Add(destinationTracker.Pop());
            }
            else
            {
                destinationTracker.Pop();
            }
        }

        private void SwapCurrentNode(ref DestinationNode currentNode)
        {
            DestinationNode savedCurrent = currentNode;
            currentNode = currentNode.Previous;
            currentNode.Next = savedCurrent;
        }

        private void CheckMultipleNextNodes(DestinationNode currentNode)
        {
            if (OutputNodes.Contains(currentNode.Previous) && (currentNode.Previous.Next != currentNode && currentNode.Previous.Next != null))
            {
                throw new ArgumentException("One Destination cannot be the previous Destination for multiple Destinations.");
            }
        }

        private void CheckCircularDependency(DestinationNode currentNode, Stack<DestinationNode> destinationTracker)
        {
            if (destinationTracker.Contains(currentNode))
            {
                throw new ArgumentException("Circular dependency is not allowed. Please check your input nodes.");
            }
        }

        private bool CheckIfDependent()
        {
            bool inputIsEqualToOutput = true;
            foreach (DestinationNode destination in Nodes)
            {
                if (destination.Previous != null)
                {
                    inputIsEqualToOutput = false;
                    break;
                }
            }
            return inputIsEqualToOutput;
        }
    }
}
