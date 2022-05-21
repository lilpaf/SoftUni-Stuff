using System;
using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void SmartPhoneCtorShouldWork()
        {
            Smartphone smartphone = new Smartphone("Apple", 100);

            Assert.AreEqual("Apple", smartphone.ModelName);
            Assert.AreEqual(100, smartphone.MaximumBatteryCharge);
            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }

        [TestCase(50)]
        public void CapacityShouldBeSet(int capacity)
        {
            Shop shop = new Shop(capacity);

            Assert.AreEqual(50, shop.Capacity);
        }

        [TestCase(-1)]
        public void CapacityShouldTrowExeprion(int capacity)
        {
            Shop shop;

            Assert.Throws<ArgumentException>(() => shop = new Shop(capacity));
        }

        [Test]
        public void AddMethodShouldWork()
        {
            Shop shop = new Shop(20);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void AddMethodShouldThrowExeceptionWhenPhoneExists()
        {
            Shop shop = new Shop(20);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone));
        }

        [Test]
        public void AddMethodShouldThrowExeceptionWhenShopIsFull()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);
            Smartphone smartphone1 = new Smartphone("Samsung", 50);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone1));
        }

        [Test]
        public void RemoveMethodShouldWorkCorrectly()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            shop.Remove("Apple");

            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void RemoveMethodShouldTrowExeptionWhenPhoneDosentExist()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            Assert.Throws<InvalidOperationException>(() => shop.Remove("Apple"));
        }

        [Test]
        public void TestPhoneMethodShouldTrowExeptionWhenPhoneDosentExist()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Samsung", 10));
        } 
        
        [Test]
        public void TestPhoneMethodShouldTrowExeptionWhenPhoneBatteryIsVeryLow()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 10);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Apple", 20));
        }
        
        [Test]
        public void TestPhoneMethodShouldWorkCorrectly()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            shop.TestPhone("Apple", 10);

            Assert.AreEqual(90, smartphone.CurrentBateryCharge);
        } 
        
        [Test]
        public void ChargePhoneMethodShouldWorkCorrectly()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            shop.Add(smartphone);

            shop.TestPhone("Apple", 10);

            Assert.AreEqual(90, smartphone.CurrentBateryCharge);

            shop.ChargePhone("Apple");

            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }
        
        [Test]
        public void ChargePhoneMethodShouldTrowExeptionWhenPhoneIsNonExistent()
        {
            Shop shop = new Shop(1);

            Smartphone smartphone = new Smartphone("Apple", 100);

            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Apple"));
        }
    }
}