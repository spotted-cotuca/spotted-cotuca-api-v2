using FluentValidation;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.Application.Contracts.Requests.Spot
{
    public class SpotPutRequest
    {
        public string Status { get; set; }
    }

    public class SpotPutRequestValidator : AbstractValidator<SpotPutRequest>
    {
        public SpotPutRequestValidator()
        {
            RuleFor(request => request.Status)
                .NotEmpty()
                .WithCustomError(Errors.SpotStatusIsEmpty)
                .Must(BeAValidStatus)
                .WithCustomError(Errors.SpotStatusIsNotApprovedOrRejected);
        }

        private bool BeAValidStatus(string status)
        {
            if (status.ToLower() == Status.Approved.ToString().ToLower() || 
                status.ToLower() == Status.Rejected.ToString().ToLower())
            {
                return true;
            }

            return false;
        }
    }
}
