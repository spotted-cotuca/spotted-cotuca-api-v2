using FluentValidation;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.Application.Contracts.Requests.Spot
{
    public class SpotPostRequest
    {
        public string Message { get; set; }
    }

    public class SpotPostRequestValidator : AbstractValidator<SpotPostRequest>
    {
        public SpotPostRequestValidator()
        {
            RuleFor(request => request.Message)
                .NotEmpty()
                .WithCustomError(Errors.SpotMessageIsEmpty)
                .Length(0, 280)
                .WithCustomError(Errors.SpotMessageIsMoreThan280Characters);
        }
    }
}
