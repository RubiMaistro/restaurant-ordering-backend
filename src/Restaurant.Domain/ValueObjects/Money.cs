namespace Restaurant.Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        #region Properties

        public decimal Amount { get; }
        public Currency Currency { get; }

        #endregion Properties

        #region Ctors

        public Money(decimal amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new DomainException("Money amount cannot be negative.");
            }

            Amount = amount;
            Currency = currency;
        }

        #endregion Ctors

        #region Overrides

        public override bool Equals(object? obj)
            => Equals(obj as Money);

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public override string ToString()
            => $"{Amount} {Currency}";

        #endregion Overrides

        #region Behaviors

        public static Money Create(decimal amount, Currency currenc = Currency.HUF) 
            => new Money(amount, currenc);

        public static Money operator +(Money left, Money right)
        {
            if(left.Currency != right.Currency)
            {
                throw new DomainException("Cannot add Money with different currencies.");
            }
            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public bool Equals(Money? other)
            => other is not null &&
            Amount == other.Amount && Currency == other.Currency;

        #endregion Behaviors
        
    }
}
