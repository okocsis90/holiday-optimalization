using Microsoft.VisualStudio.TestTools.UnitTesting;
using HolidayOptimizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimizer.Tests
{
    [TestClass()]
    public class OptimizerTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
        "Input list is empty, please add Destinations to your list.")]
        public void OptimizeTest_InputIsEmpty_ThrowsException()
        {
            Optimizer.Optimize(new List<DestinationNode>());
        }

        [TestMethod()]
        public void OptimizeTest_InputHasNoDependencies_GivesBackInput()
        {
            List<DestinationNode> allLocations = new List<DestinationNode> {
                new DestinationNode("Location A"),
                new DestinationNode("Location B"),
                new DestinationNode("Location C")
            };
            CollectionAssert.AreEqual(allLocations, Optimizer.Optimize(allLocations));
        }

        [TestMethod()]
        public void OptimizeTest_InputHasOneDependency_GivesProperOutput()
        {
            DestinationNode locA = new DestinationNode("Location A");
            DestinationNode locC = new DestinationNode("Location C");
            DestinationNode locB = new DestinationNode("Location B", locC);
            List<DestinationNode> allLocations = new List<DestinationNode> {
                locB,
                locC,
                locA
            };
            List<DestinationNode> expectedLocations = new List<DestinationNode>
            {
                locC,
                locB,
                locA
            };
            CollectionAssert.AreEqual(expectedLocations, Optimizer.Optimize(allLocations));
        }
        

        
    }
}