namespace SpottedCotuca.Common.Security
{
    public enum Permission
    {
        //Spot
        ReadApprovedSpot,
        ReadPendingSpot,
        ReadRejectedSpot,
        WriteSpot,
        UpdateSpot,
        DeleteSpot,
        
        //User
        ReadUser,
        WriteUser,
        UpdateUser,
        DeleteUser
    }
}