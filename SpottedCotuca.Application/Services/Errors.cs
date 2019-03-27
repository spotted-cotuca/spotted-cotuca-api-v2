using SpottedCotuca.Application.Services.Definitions;

namespace SpottedCotuca.Application.Services
{
    public static class Errors
    {
        public static readonly MetaError SpotIdIsInvalid = new MetaError(400, "Invalid Spot Id.");
        public static readonly MetaError SpotMessageIsEmpty = new MetaError(400, "Message cannot be empty.");
        public static readonly MetaError SpotMessageIsMoreThan280Characters = new MetaError(400, "Message cannot be more than 280 characters.");
        public static readonly MetaError SpotStatusIsEmpty = new MetaError(400, "Status cannot be empty.");
        public static readonly MetaError SpotStatusIsNotApprovedOrRejected = new MetaError(400, "Status cannot be different from \"approved\" or \"rejected\".");
        public static readonly MetaError SpotStatusIsNotApprovedRejectedOrPending = new MetaError(400, "Status cannot be different from \"approved\",\"rejected\" or \"pending\".");
        public static readonly MetaError SpotOffsetIsLesserThan0 = new MetaError(400, "Offset cannot be lesser than 0.");
        public static readonly MetaError SpotLimitIsLesserThan1 = new MetaError(400, "Limit cannot be lesser than 1.");
        public static readonly MetaError SpotLimitIsGreaterThanMaxLimit = new MetaError(400, "Limit cannot be greater than 50.");
        public static readonly MetaError SpotCreatingFacebookPostError = new MetaError(500, "Error creating a Facebook post.");
        public static readonly MetaError SpotPublishingTweetError = new MetaError(500, "Error publishing Tweet.");
        public static readonly MetaError SpotCreatingOnDatabaseError = new MetaError(500, "Error creating Spot on database");
        public static readonly MetaError SpotUpdatingOnDatabaseError = new MetaError(500, "Error updating Spot on database.");
        public static readonly MetaError SpotReadingFromDatabaseError = new MetaError(500, "Error reading Spot from database.");
        public static readonly MetaError SpotsReadingFromDatabaseError = new MetaError(500, "Error reading Spots from database.");
        public static readonly MetaError SpotsDeletingFromDatabaseError = new MetaError(500, "Error deleting Spot from database.");
        public static readonly MetaError SpotNotFoundError = new MetaError(404, "Spot not found.");
        public static readonly MetaError SpotsNotFoundError = new MetaError(404, "Spots not found.");

        public static readonly MetaError UserUsernameIsEmpty = new MetaError(400, "Username cannot be empty.");
        public static readonly MetaError UserUsernameLength = new MetaError(400, "Username length must be between 4 and 30 characters.");
        public static readonly MetaError UserPasswordIsEmpty = new MetaError(400, "Password cannot be empty.");
        public static readonly MetaError UserPasswordIsLesserThan8 = new MetaError(400, "Password must have at least 8 characters.");
    }
}
