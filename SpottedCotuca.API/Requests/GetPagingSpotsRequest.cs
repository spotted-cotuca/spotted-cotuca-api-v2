using FluentValidation;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Requests
{
    public class GetPagingSpotsRequest
    {
        public string Status { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetPagingSpotsRequestValidator : AbstractValidator<GetPagingSpotsRequest>
    {
        public GetPagingSpotsRequestValidator()
        {
            RuleFor(request => request.Status)
                .NotNull()
                .WithMessage("Status cannot be empty.")
                .NotEmpty()
                .WithMessage("Status cannot be empty.")
                .Must(BeAValidStatus)
                .WithMessage("Status cannot be different from \"approved\",\"rejected\" or \"pending\".");

            RuleFor(request => request.Offset)
                .GreaterThan(0)
                .WithMessage("Offset cannot be lesser than 0.");

            RuleFor(request => request.Limit)
                .GreaterThan(1)
                .WithMessage("Limit cannot be lesser than 1.")
                .LessThan(50)
                .WithMessage("Limit cannot be greater than 50");
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
