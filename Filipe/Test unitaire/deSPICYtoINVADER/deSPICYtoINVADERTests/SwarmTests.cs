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
            Point p;
            Bullet bul;
            Enemy e;
            int row = 5;
            int col = 5;
            int D = 1;

            //Act
            sw = new Swarm(row, col);
            p = new Point(row, col);
            bul = new Bullet(p, D);
            e = new Enemy(p, utils.Sprites.playerDesign);
            e.GetShot(bul);
            sw.DeleteEnemy();

            // Assert
            Assert.AreEqual(24, sw.Enemies.Count);
        }

        [TestMethod()]
        public void MoveTest()
        {
            // Arrange
            Swarm sw;
            int row = 5;
            int col = 5;
            int D = 1;

            //Act
            sw = new Swarm(row, col);
            sw.Move(D);

            // Assert
            Assert.AreEqual(6, sw.Enemies[0]);
        }
    }
}