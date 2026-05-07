namespace RestaurantManagement.Domain.Constants.ErrorCodes
{
    /// <summary>
    /// Error codes for OrderItem feature
    /// </summary>
    public static class OrderItemErrorCodes
    {
        public const string InvalidMenuItemId = "INVALID_MENU_ITEM_ID";
        public const string InvalidQuantity = "INVALID_QUANTITY";
        public const string InvalidUnitPrice = "INVALID_UNIT_PRICE";
        public const string NotFound = "ORDER_ITEM_NOT_FOUND";
        public const string StatusNotAllowToDelete = "ORDER_ITEM_STATUS_NOT_ALLOW_TO_DELETE";
        public const string StatusNotAllowToUpdate = "ORDER_ITEM_STATUS_NOT_ALLOW_TO_UPDATE";
    }
}
