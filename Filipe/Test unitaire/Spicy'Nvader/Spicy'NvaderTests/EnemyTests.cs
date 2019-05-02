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
    public class EnemyTests
    {
        [TestMethod()]
        public void EnemyTest()
        {
            // Arrange
            Enemy enmy;
            int x = 5;
            int y = 5;

            // Act
            enmy = new Enemy(x, y);

            // Assert
            Assert.IsNotNull(enmy);
        }

        [TestMethod()]
        public void MoveEnemyTest()
        {
            // Arrange
            Enemy enmy;
            int x = 5;
            int y = 5;
            int direction = 1;

            // Act
            enmy = new Enemy(x, y);
            enmy.MoveEnemy(direction);

            // Assert
            Assert.AreEqual(6, enmy.MinX);
        }

        [TestMethod()]
        public void GoDownTest()
        {
            // Arrange
            Enemy enmy;
            int x = 5;
            int y = 5;
            int val = 2;

            // Act
            enmy = new Enemy(x, y);
            enmy.GoDown(val);

            // Assert
            Assert.AreEqual(7, enmy.CurrentTopPos);
        }

        [TestMethod()]
        public void GetHitBoxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EnemyGetShotTest()
        {
            // Arrange
            Enemy enmy;
            Bullet bul;
            int enmyX = 10;
            int enmyY = 5;
            int bulX = 13;
            int bulY = 5;
            int maxY = 1;
            int direction = -1;

            // Act
            enmy = new Enemy(enmyX, enmyY);
            bul = new Bullet(bulX, bulY, maxY, direction);
            enmy.EnemyGetShot(bul);

            // Assert
            Assert.AreEqual(true, enmy.EnemyGetShot(bul));
        }

        [TestMethod()]
        public void EnemyShootsTest()
        {
            Assert.Fail();
        }
    }
}