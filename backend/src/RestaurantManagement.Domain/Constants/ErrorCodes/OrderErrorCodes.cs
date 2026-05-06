namespace RestaurantManagement.Domain.Constants.ErrorCodes
{
    /// <summary>
    /// Error codes for Order feature
    /// </summary>
    public static class OrderErrorCodes
    {
        public const string InvalidOrderItems = "INVALID_ORDER_ITEMS";
        public const string OrderNotFound = "ORDER_NOT_FOUND";
        public const string OrderStatusNotAllowToUpdate = "ORDER_STATUS_NOT_ALLOW_TO_UPDATE";
        public const string OrderAlreadyClosed = "ORDER_ALREADY_CLOSED";
        public const string InvalidOrderStatus = "INVALID_ORDER_STATUS";
        public const string InvalidStatusTransition = "INVALID_STATUS_TRANSITION";
        public const string CannotModifyClosedOrder = "CANNOT_MODIFY_CLOSED_ORDER";
    }
}
