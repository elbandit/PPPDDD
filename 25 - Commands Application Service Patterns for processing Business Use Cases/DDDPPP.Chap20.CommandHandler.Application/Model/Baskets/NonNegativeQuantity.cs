namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class NonNegativeQuantity
    {
        private int _value;

        private NonNegativeQuantity() : this (0)
        { }

        public NonNegativeQuantity(int value)
        {
            // Check.IsGreaterThan(-1, value, () => { throw new ArgumentOutOfRangeException(); });
            _value = value;
        }
        
        public NonNegativeQuantity Add(NonNegativeQuantity quantity)
        {
            return new NonNegativeQuantity(_value + quantity._value);
        }

        public int value
        {
            get { return _value; }
        }

        public bool is_zero()
        {
            return _value == 0;
        }
    }
}