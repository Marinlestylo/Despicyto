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
    public class BulletTests
    {
        [TestMethod()]
        public void BulletTest()
        {
            // Arrange
            Bullet bul;
            int posX = 5;
            int posY = 5;
            int maxY = 1;
            int direction = -1;

            // Act
            bul = new Bullet(posX, posY, maxY, direction);

            // Assert
            Assert.IsNotNull(bul);
        }

        [TestMethod()]
        public void BulletMoveTest()
        {
            // Arrange
            Bullet bul;
            int posX = 5;
            int posY = 5;
            int maxY = 1;
            int direction = 1;

            // Act
            bul = new Bullet(posX, posY, maxY, direction);
            bul.BulletMove();

            // Assert
            Assert.AreEqual(4, bul.PosY);
        }
    }
}