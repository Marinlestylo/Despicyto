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
            // Arrange
            Player play;
            Bullet b;
            int x = 75;
            int y = 69;
            int direction = 1;

            //Act
            play = new Player();
            b = new Bullet(new Point(x,y), direction);
            play.GetShot(b);

            // Assert
            Assert.AreEqual(8, play.Life);
        }
    }
}