using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deSPICYtoINVADER.Characters;

namespace deSPICYtoINVADER.Tests
{
    [TestClass()]
    public class SwarmTests
    {
        [TestMethod()]
        public void SwarmTest()
        {
            // Arrange
            Swarm sw;
            int row = 5;
            int col = 5;

            //Act
            sw = new Swarm(row, col);

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
            sw = new Swarm(row, col);

            // Assert
            Assert.AreEqual(25, sw.Enemies.Count);
        }

        [TestMethod()]
        public void DeleteEnemyTest()
        {
            // Arrange
            Swarm sw;
            Bullet bul;
            int row = 5;
            int col = 5;
            int D = -1;

            //Act
            sw = new Swarm(row, col);
            bul = new Bullet(new Point(5,5), D);
            sw.Enemies[0].GetShot(bul);
            sw.DeleteEnemy();

            // Assert
            Assert.AreEqual(24, sw.Enemies.Count);
        }
    }
}