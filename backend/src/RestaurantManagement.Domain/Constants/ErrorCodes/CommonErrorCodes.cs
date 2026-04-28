namespace RestaurantManagement.Domain.Constants.ErrorCodes
{
    /// <summary>
    /// Common error codes used across features
    /// </summary>
    public static class CommonErrorCodes
    {
        public const string NotFound = "NOT_FOUND";
        public const string Conflict = "CONFLICT";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string DatabaseError = "DATABASE_ERROR";
        public const string ValidationError = "VALIDATION_ERROR";
    }
}
