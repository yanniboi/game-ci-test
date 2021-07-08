using UI;

namespace Tests.Builders
{
    public class ShopperCounterBuilder
    {
        private int _count;
    
        public ShopperCounterBuilder(int count)
        {
            _count = count;
        }

        public ShopperCounterBuilder() : this(0) { }

        public ShopperCounterBuilder WithCount(int count)
        {
            _count = count;
            return this;
        }

        public ShopperCounter Build()
        {
            return new ShopperCounter(_count);
        }
    
        public static implicit operator ShopperCounter(ShopperCounterBuilder shopperCounterBuilder)
        {
            return shopperCounterBuilder.Build();
        }
    }
}
