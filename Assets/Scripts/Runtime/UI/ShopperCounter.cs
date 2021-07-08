using System;

namespace UI
{
    public class ShopperCounter
    {
        public ShopperCounter(int count = 0)
        {
            Count = count;
        }

        public int Count { get; protected set; }

        public void Increase()
        {
            Count++;
        }

        public void Decrease()
        {
            if (Count <= 0)
            {
                throw new IndexOutOfRangeException("Counter cannot be less than zero.");
            }

            Count--;
        }
    }
}