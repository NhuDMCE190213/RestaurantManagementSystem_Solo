using RestaurantManagement.Domain.BaseEntities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities
{
    public class Table : BaseEntity
    {
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TableStatus TableStatus { get; set; }

        public Table AddTable(int tableNumber, int capacity, Guid employeeId)
        {
            var table = new Table
            {
                TableId = Guid.NewGuid(),
                TableNumber = tableNumber,
                Capacity = capacity,
                TableStatus = TableStatus.Available,

                CreatedBy = employeeId,
                CreatedAt = DateTime.UtcNow
            };
            return table;
        }

        public void UpdateTable(int tableNumber, int capacity, TableStatus tableStatus, Guid employeeId)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            TableStatus = tableStatus;

            UpdatedBy = employeeId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteTable(Guid employeeId)
        {
            TableStatus = TableStatus.Inactive;
            DeletedBy = employeeId;
            DeletedAt = DateTime.UtcNow;
        }   
    }
}
