namespace Restaurant.Domain.Entities
{
    public class OrderItem
    {
        #region Properties

        public Guid Id { get; private set; }
        public Guid DishId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; set; }

        #endregion Properties

        #region Ctors

        public OrderItem() { }

        public OrderItem(Guid dishId, int quantity, Money unitPrice)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            if (unitPrice == null)
                throw new DomainException("Unit price cannot be null.");

            DishId = dishId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Id = Guid.NewGuid();
        }

        #endregion Ctors

        #region Behaviors

        public Money GetTotalPrice()
            => Money.Create(Quantity * UnitPrice.Amount, UnitPrice.Currency);

        #endregion Behaviors
    }
}
