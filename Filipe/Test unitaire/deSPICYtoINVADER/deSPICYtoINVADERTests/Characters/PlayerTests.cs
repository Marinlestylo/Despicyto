using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Characters.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerTest()
        {
            // Arrange
            Player p;

            //Act
            p = new Player();

            // Assert
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void AddOnScoreTest()
        {
            // Arrange
            Player p;

            //Act
            p = new Player();

            // Assert
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void GetShotTest()
        {
            Assert.Fail();
        }
    }
}