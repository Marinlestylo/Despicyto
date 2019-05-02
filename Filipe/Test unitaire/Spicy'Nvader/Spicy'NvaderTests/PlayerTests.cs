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
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerTest()
        {
            // Arrange
            Player play;

            // Act
            play = new Player();

            // Assert
            Assert.IsNotNull(play);
        }

        [TestMethod()]
        public void MoveTest()
        {
            // Arrange
            Player play;
            int move = 1;

            // Act
            play = new Player();
            play.Move(move);

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void ShootTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddOnScoreTest()
        {
            // Arrange
            Player play;

            // Act
            play = new Player();
            play.AddOnScore();

            // Assert
            Assert.AreEqual(25, play.PlayerScore);
        }
      
        [TestMethod()]
        public void GetShotTest()
        {
            // Arrange


            // Act


            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHitBoxTest()
        {
            Assert.Fail();
        }
    }
}