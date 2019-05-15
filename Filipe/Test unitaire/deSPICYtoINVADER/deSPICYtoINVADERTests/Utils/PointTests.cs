using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Tests
{
    [TestClass()]
    public class PointTests
    {
        [TestMethod()]
        public void PointTest()
        {
            // Arrange
            Point p;
            int x = 5;
            int y = 5;

            //Act
            p = new Point(x, y);

            // Assert
            Assert.IsNotNull(p);
        }
    }
}