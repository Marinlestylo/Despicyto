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
    public class GameTests
    {
        [TestMethod()]
        public void GameTest()
        {
            // Arrange
            Game g;

            //Act
            g = new Game();

            // Assert
            Assert.IsNotNull(g);
        }
    }
}