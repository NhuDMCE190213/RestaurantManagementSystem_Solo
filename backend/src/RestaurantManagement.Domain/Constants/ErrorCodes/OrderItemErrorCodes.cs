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
        public const string OrderItemNotFound = "ORDER_ITEM_NOT_FOUND";
        public const string MenuItemNotFound = "MENU_ITEM_NOT_FOUND";
    }
}
