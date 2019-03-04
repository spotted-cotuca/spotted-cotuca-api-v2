using FluentValidation;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.Application.Contracts.Requests
{
    public class SpotsGetRequest
    {
        public string Status { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class SpotsGetRequestValidator : AbstractValidator<SpotsGetRequest>
    {
        public SpotsGetRequestValidator()
        {
            RuleFor(request => request.Status)
                .NotNull()
                .WithCustomError(Errors.SpotStatusIsEmpty)
                .NotEmpty()
                .WithCustomError(Errors.SpotStatusIsEmpty)
                .Must(BeAValidStatus)
                .WithCustomError(Errors.SpotStatusIsNotApprovedRejectedOrPending);

            RuleFor(request => request.Offset)
                .GreaterThan(0)
                .WithCustomError(Errors.SpotOffsetIsLesserThan0);

            RuleFor(request => request.Limit)
                .GreaterThan(1)
                .WithCustomError(Errors.SpotLimitIsLesserThan1)
                .LessThan(50)
                .WithCustomError(Errors.SpotLimitIsGreaterThanMaxLimit);
        }

        private bool BeAValidStatus(string status)
        {
            if (status == null)
                return false;

            if (status.ToLower() == Status.Approved.ToString().ToLower() ||
                status.ToLower() == Status.Rejected.ToString().ToLower() ||
                status.ToLower() == Status.Pending.ToString().ToLower())
            {
                return true;
            }

            return false;
        }
    }
}
