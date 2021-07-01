using FluentValidation;

namespace Ordering.Application.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Please specify a valid customer id.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
        }
    }

    public sealed class OrderItemValidator : AbstractValidator<CreateOrderCommand.OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("The description needs to be informed.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The quantity needs to be informed.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price needs to be informed.");
        }
    }
}
