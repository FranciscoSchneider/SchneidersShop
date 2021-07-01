using FluentValidation;

namespace Ordering.Application.InvoiceOrder
{
    public sealed class InvoiceOrderCommandValidator : AbstractValidator<InvoiceOrderCommand>
    {
        public InvoiceOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Please specify a valid order id.");
        }
    }
}
