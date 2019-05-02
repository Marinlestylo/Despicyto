using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spicy_Nvader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy_Nvader.Tests
{
    [TestClass()]
    public class SwarmTests
    {
        [TestMethod()]
        public void SwarmTest()
        {
            // Arrange
            Swarm sw;

            //Act
            sw = new Swarm();

            // Assert
            Assert.IsNotNull(sw);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            Swarm sw;
            int row = 5;
            int col = 5;

            //Act
            sw = new Swarm();
            sw.Create(row, col);

            // Assert
            Assert.AreEqual(25, sw.Enemies.Count);
        }
    }
}