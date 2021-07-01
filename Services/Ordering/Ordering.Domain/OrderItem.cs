using SharedKernel;

namespace Ordering.Domain
{
    public sealed class OrderItem : Entity
    {
        public OrderItem(string description, int quantity, decimal price)
        {
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public string Description { get; }
        public int Quantity { get; }
        public decimal Price { get; }
    }
}
