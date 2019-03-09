using SpottedCotuca.Application.Services.Definitions;

namespace SpottedCotuca.Application.Services
{
    public static class Errors
    {
        public static MetaError SpotIdIsInvalid = new MetaError(400, "Invalid Spot Id.");
        public static MetaError SpotMessageIsEmpty = new MetaError(400, "Message cannot be empty.");
        public static MetaError SpotMessageIsMoreThan280Characters = new MetaError(400, "Message cannot be more than 280 characters.");
        public static MetaError SpotStatusIsEmpty = new MetaError(400, "Status cannot be empty.");
        public static MetaError SpotStatusIsNotApprovedOrRejected = new MetaError(400, "Status cannot be different from \"approved\" or \"rejected\".");
        public static MetaError SpotStatusIsNotApprovedRejectedOrPending = new MetaError(400, "Status cannot be different from \"approved\",\"rejected\" or \"pending\".");
        public static MetaError SpotOffsetIsLesserThan0 = new MetaError(400, "Offset cannot be lesser than 0.");
        public static MetaError SpotLimitIsLesserThan1 = new MetaError(400, "Limit cannot be lesser than 1.");
        public static MetaError SpotLimitIsGreaterThanMaxLimit = new MetaError(400, "Limit cannot be greater than 50.");
        public static MetaError SpotCreatingFacebookPostError = new MetaError(500, "Error creating a Facebook post.");
        public static MetaError SpotPublishingTweetError = new MetaError(500, "Error publishing Tweet.");
        public static MetaError SpotCreatingOnDatabaseError = new MetaError(500, "Error creating Spot on database");
        public static MetaError SpotUpdatingOnDatabaseError = new MetaError(500, "Error updating Spot on database.");
        public static MetaError SpotReadingFromDatabaseError = new MetaError(500, "Error reading Spot from database.");
        public static MetaError SpotsReadingFromDatabaseError = new MetaError(500, "Error reading Spots from database.");
        public static MetaError SpotsDeletingFromDatabaseError = new MetaError(500, "Error deleting Spot from database.");
        public static MetaError SpotNotFoundError = new MetaError(404, "Spot not found.");
        public static MetaError SpotsNotFoundError = new MetaError(404, "Spots not found.");
    }
}
