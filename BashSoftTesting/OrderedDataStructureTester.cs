namespace BashSoftTesting
{
    using System;
    using BashSoft.DataStructures;
    using BashSoft.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class OrdredDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }



        [Test]
        public void TestEmptyConstructor()
        {

            //Assert
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);

        }
        [Test]
        public void TestConstructorWithInitialCapacity()
        {
            //Arrange
            this.names = new SimpleSortedList<string>(20);

            //Assert
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);

        }

        [Test]
        public void TestConstructorWithAllParams()
        {
            //Arrange
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);

            //Assert
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);

        }
        [Test]
        public void TestConstructorWithInitialParam()
        {
            //Arrange
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);

            //Assert
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);

        }

        [Test]
        public void TestAddIncreasesSize()
        {
            //Act
            this.names.Add("Mitaka");

            //Assert
            Assert.AreEqual(1, this.names.Size);

        }

        [Test]
        public void TestAddNullThrowsException()
        {
            //Act 
            var ex = Assert.Throws<InvalidOperationException>(() => this.names.Add(null));

            //Assert
            Assert.AreEqual("The element Cannot be null", ex.Message);
        }
    }
}
