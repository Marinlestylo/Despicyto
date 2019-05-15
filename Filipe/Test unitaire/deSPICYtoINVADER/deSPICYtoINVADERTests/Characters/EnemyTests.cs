using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deSPICYtoINVADER.utils;

namespace deSPICYtoINVADER.Characters.Tests
{
    [TestClass()]
    public class EnemyTests
    {
        [TestMethod()]
        public void EnemyTest()
        {
            // Arrange
            Enemy e;
            Point p;
            int X = 5;
            int Y = 5;

            //Act
            e = new Enemy(p = new Point(X, Y), Sprites.enemyDesign);

            // Assert
            Assert.IsNotNull(e);
        }

        [TestMethod()]
        public void MoveInSwarmTest()
        {
            // Arrange
            Enemy e;
            Point p;
            int X = 5;
            int Y = 5;

            //Act
            e = new Enemy(p = new Point(X, Y), Sprites.enemyDesign);
            e.MoveInSwarm(1);

            // Assert
            Assert.AreEqual(6, p.X);
        }

        [TestMethod()]
        public void GoDownTest()
        {
            // Arrange
            Enemy e;
            Point p;
            int X = 5;
            int Y = 5;


            //Act
            e = new Enemy(p = new Point(X, Y), Sprites.enemyDesign);
            e.GoDown();

            // Assert
            Assert.AreEqual(8, p.Y);
        }

        [TestMethod()]
        public void GetShotTest()
        {
            // Arrange
            Enemy e;
            Point p;
            Bullet b;
            int X = 5;
            int Y = 5;
            int Direction = -1;


            //Act
            e = new Enemy(p = new Point(X, Y), Sprites.enemyDesign);
            b = new Bullet(p, Direction);
            e.GetShot(b);

            // Assert
            Assert.AreEqual(true, e.GonnaDelete);
        }
    }
}