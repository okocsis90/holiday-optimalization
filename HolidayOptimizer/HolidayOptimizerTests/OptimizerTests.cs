﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            Optimizer optimizer = new Optimizer(new List<DestinationNode>());
            optimizer.Optimize();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
        "Circular dependency is not allowed. Please check your input nodes.")]
        public void OptimizeTest_InputHasCircularDependency_ThrowsException()
        {
            DestinationNode locA = new DestinationNode("Location A");
            DestinationNode locB = new DestinationNode("Location B");
            DestinationNode locC = new DestinationNode("Location C");
            locA.Previous = locB;
            locB.Previous = locC;
            locC.Previous = locA;
            List<DestinationNode> allLocations = new List<DestinationNode> {
                locA,
                locB,
                locC
            };
            Optimizer optimizer = new Optimizer(allLocations);
            optimizer.Optimize();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
        "One Destination cannot be the previous Destination for multiple Destinations.")]
        public void OptimizeTest_DestinationIsThePreviousOfMultipleDestinations_ThrowsException()
        {
            DestinationNode locA = new DestinationNode("Location A");
            DestinationNode locB = new DestinationNode("Location B", locA);
            DestinationNode locC = new DestinationNode("Location C", locA);
            List<DestinationNode> allLocations = new List<DestinationNode> {
                locA,
                locB,
                locC
            };
            Optimizer optimizer = new Optimizer(allLocations);
            optimizer.Optimize();
        }

        [TestMethod()]
        public void OptimizeTest_InputHasNoDependencies_GivesBackInput()
        {
            List<DestinationNode> allLocations = new List<DestinationNode> {
                new DestinationNode("Location A"),
                new DestinationNode("Location B"),
                new DestinationNode("Location C")
            };
            Optimizer optimizer = new Optimizer(allLocations);
            CollectionAssert.AreEqual(allLocations, optimizer.Optimize());
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
            Optimizer optimizer = new Optimizer(allLocations);
            CollectionAssert.AreEqual(expectedLocations, optimizer.Optimize());
        }
        
        [TestMethod()]
        public void OptimizeTest_InputHasTwoDependencies_GivesProperOutput()
        {
            DestinationNode locA = new DestinationNode("Location A");
            DestinationNode locC = new DestinationNode("Location C", locA);
            DestinationNode locB = new DestinationNode("Location B", locC);
            List<DestinationNode> allLocations = new List<DestinationNode> {
                locB,
                locC,
                locA
            };
            List<DestinationNode> expectedLocations = new List<DestinationNode>
            {
                locA,
                locC,
                locB
            };
            Optimizer optimizer = new Optimizer(allLocations);
            CollectionAssert.AreEqual(expectedLocations, optimizer.Optimize());
        }

        [TestMethod()]
        public void OptimizeTest_InputHasDependenciesOfTwoGroup_GivesProperOutput()
        {
            DestinationNode locA = new DestinationNode("Location A");
            DestinationNode locC = new DestinationNode("Location C", locA);
            DestinationNode locB = new DestinationNode("Location B", locC);
            DestinationNode locE = new DestinationNode("Location D");
            DestinationNode locF = new DestinationNode("Location E", locE);
            DestinationNode locD = new DestinationNode("Location F", locF);
            List<DestinationNode> allLocations = new List<DestinationNode> {
                locB,
                locF,
                locD,
                locC,
                locA,
                locE
            };
            List<DestinationNode> expectedLocations = new List<DestinationNode>
            {
                locA,
                locC,
                locB,
                locE,
                locF,
                locD
            };
            Optimizer optimizer = new Optimizer(allLocations);
            CollectionAssert.AreEqual(expectedLocations, optimizer.Optimize());            
        }        
    }
}