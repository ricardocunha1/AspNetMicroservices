using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .WithMessage("UserName is required")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("UserName must not exceed 50 chars");

            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .WithMessage("Email should not be empty");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("Total price is required")
                .GreaterThan(0).WithMessage("Total price should be greater than zero");
        }
    }
}
