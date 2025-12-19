using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    public class Order
    {
        #region Properties

        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        #endregion Properties

        #region Data
        
        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        #endregion Data

        #region Ctors
        public Order() { }
        
        public Order(Guid id)
        {
            Id = id;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        #endregion Ctors

        #region Behaviors

        public void AddItem(OrderItem item)
        {
            EnsureModifiable(item);
            _items.Add(item);
        }

        public void RemoveItem(Guid orderItemId)
        {

            var item = _items.FirstOrDefault(i => i.Id == orderItemId);
            if (item == null)
                throw new DomainException("Order item not found.");

            EnsureModifiable(item);
            _items.Remove(item);
        }

        public Money GetTotalAmount()
        {
            Money total = Money.Create(0);

            foreach (var item in _items)
                total += item.GetTotalPrice();
            
            return total;
        }

        public void Submit()
        {
            if (Status != OrderStatus.Created)
                throw new DomainException("Only orders in Created status can be submitted.");

            if (!_items.Any())
                throw new DomainException("Cannot submit an empty order.");

            Status = OrderStatus.Submitted;
        }

        public void MarkAsPaid()
        {
            if (Status != OrderStatus.Submitted)
                throw new DomainException("Order must be submitted before payment.");

            if (!_items.Any())
                throw new DomainException("Cannot mark an empty order as Paid.");

            Status = OrderStatus.Paid;
        }

        public void Complete()
        {
            if (Status != OrderStatus.Paid)
                throw new DomainException("Order must be paid before completion.");

            Status = OrderStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
                throw new DomainException("Completed order cannot be cancelled.");

            Status = OrderStatus.Cancelled;
        }

        #endregion Behaviors

        #region Helpers

        private void EnsureModifiable(OrderItem item)
        {
            if (Status == OrderStatus.Completed)
                throw new DomainException("Orders in Completed status cannot be modified.");

            if (item == null)
                throw new DomainException("Order item cannot be null.");
        }

        #endregion Helpers
    }
}
