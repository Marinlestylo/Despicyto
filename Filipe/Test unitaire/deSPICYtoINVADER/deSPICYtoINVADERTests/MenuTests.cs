using Microsoft.VisualStudio.TestTools.UnitTesting;
using deSPICYtoINVADER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Tests
{
    /// <summary>
    /// Tests unitaires classe Menu
    /// </summary>
    [TestClass()]
    public class MenuTests
    {
        [TestMethod()]
        public void MenuTest()
        {
            // Arrange
            Menu m;

            //Act
            m = new Menu();

            // Assert
            Assert.IsNotNull(m);
        }
    }
}