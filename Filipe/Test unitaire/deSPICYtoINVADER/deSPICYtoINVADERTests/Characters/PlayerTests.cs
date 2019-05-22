using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace deSPICYtoINVADER.Characters.Tests
{
    /// <summary>
    /// Tests unitaires classe Player
    /// </summary>
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
            //Act
            Player.AddOnScore(37);

            // Assert
            Assert.AreEqual(37, Player.Score);
        }
    }
}