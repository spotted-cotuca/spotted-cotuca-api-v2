using FluentValidation;

namespace SpottedCotuca.Application.Contracts.Requests
{
    public class PostSpotRequest
    {
        public string Message { get; set; }
    }

    public class PostSpotRequestValidator : AbstractValidator<PostSpotRequest>
    {
        public PostSpotRequestValidator()
        {
            RuleFor(request => request.Message)
                .NotEmpty()
                .WithMessage("Message cannot be empty.")
                .Length(0, 280)
                .WithMessage("Message cannot be more than 280 characters.");
        }
    }
}
