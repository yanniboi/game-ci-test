using System;
using NUnit.Framework;
using Tests.Builders;
using UI;

namespace Tests.UI
{
    public class ShopperCounterTests
    {
        [Test]
        public void counter_initializes_with_0()
        {
            ShopperCounter shopperCounter = A.ShopperCounter();
            Assert.AreEqual(0, shopperCounter.Count);
        }
        
        [Test]
        public void _0_counter_increases_to_1()
        {
            ShopperCounter shopperCounter = A.ShopperCounter();

            shopperCounter.Increase();
            
            Assert.AreEqual(1, shopperCounter.Count);
        }
        
        [Test]
        public void _1_counter_increases_to_2()
        {
            ShopperCounter shopperCounter = A.ShopperCounter().WithCount(1);

            shopperCounter.Increase();
            
            Assert.AreEqual(2, shopperCounter.Count);
        }
        
        [Test]
        public void _0_counter_increases_x2_to_2()
        {
            ShopperCounter shopperCounter = A.ShopperCounter();

            shopperCounter.Increase();
            shopperCounter.Increase();
            
            Assert.AreEqual(2, shopperCounter.Count);
        }

        [Test]
        public void _1_counter_decreases_to_0()
        {
            ShopperCounter shopperCounter = A.ShopperCounter().WithCount(1);

            shopperCounter.Decrease();
            
            Assert.AreEqual(0, shopperCounter.Count);
        }
        
        [Test]
        public void _2_counter_decreases_to_1()
        {
            ShopperCounter shopperCounter = A.ShopperCounter().WithCount(2);

            shopperCounter.Decrease();
            
            Assert.AreEqual(1, shopperCounter.Count);
        }
        
        [Test]
        public void _2_counter_decreases_x2_to_0()
        {
            ShopperCounter shopperCounter = A.ShopperCounter().WithCount(2);

            shopperCounter.Decrease();
            shopperCounter.Decrease();
            
            Assert.AreEqual(0, shopperCounter.Count);
        }

        [Test]
        public void counter_throws_exception_when_decreasing_below_0()
        {
            ShopperCounter shopperCounter = A.ShopperCounter();
            Assert.Throws<IndexOutOfRangeException>(() => shopperCounter.Decrease());
        }
        
    }
}
