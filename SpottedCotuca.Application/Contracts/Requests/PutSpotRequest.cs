using FluentValidation;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Application.Contracts.Requests
{
    public class PutSpotRequest
    {
        public string Status { get; set; }
    }

    public class PutSpotRequestValidator : AbstractValidator<PutSpotRequest>
    {
        public PutSpotRequestValidator()
        {
            RuleFor(request => request.Status)
                .NotEmpty()
                .WithMessage("Status cannot be empty.")
                .Must(BeAValidStatus)
                .WithMessage("Status cannot be different from \"approved\" or \"rejected\".");
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
