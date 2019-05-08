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
    public class BulletTests
    {
        [TestMethod()]
        public void BulletTest()
        {
            // Arrange
            Point p;
            Bullet bul;
            int X = 5;
            int Y = 5;
            int D = -1;

            // Act
            p = new Point(X, Y);
            bul = new Bullet(p, D);

            // Assert
            Assert.IsNotNull(bul);
        }

        [TestMethod()]
        public void MoveTest()
        {
            // Arrange
            Point p;
            Bullet bul;
            int X = 5;
            int Y = 5;
            int D = -1;

            // Act
            p = new Point(X, Y);
            bul = new Bullet(p, D);
            bul.Move();

            // Assert
            Assert.AreEqual(4, bul.Position.Y);
        }
    }
}