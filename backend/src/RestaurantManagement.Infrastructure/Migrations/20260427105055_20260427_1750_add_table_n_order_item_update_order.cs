using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20260427_1750_add_table_n_order_item_update_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "Orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax",
                table: "Orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "Orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount",
                table: "Orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "order_status",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AddColumn<Guid>(
                name: "table_id",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    order_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    order_item_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.order_item_id);
                    table.CheckConstraint("ck_order_items_quantity_non_negative", "quantity >= 0");
                    table.CheckConstraint("ck_order_items_total_price_consistent", "total_price = quantity * unit_price");
                    table.CheckConstraint("ck_order_items_total_price_non_negative", "total_price >= 0");
                    table.CheckConstraint("ck_order_items_unit_price_non_negative", "unit_price >= 0");
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    table_id = table.Column<Guid>(type: "uuid", nullable: false),
                    table_number = table.Column<int>(type: "integer", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    table_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Available"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tables", x => x.table_id);
                    table.CheckConstraint("CK_Table_Capacity", "Capacity > 0");
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_created_at",
                table: "Orders",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_orders_customer_id",
                table: "Orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_status",
                table: "Orders",
                column: "order_status");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_status_created_at",
                table: "Orders",
                columns: new[] { "order_status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_orders_table_id",
                table: "Orders",
                column: "table_id");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orders_discount_non_negative",
                table: "Orders",
                sql: "discount >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orders_sub_total_non_negative",
                table: "Orders",
                sql: "sub_total >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orders_tax_non_negative",
                table: "Orders",
                sql: "tax >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orders_total_amount_consistent",
                table: "Orders",
                sql: "total_amount = sub_total + tax - discount");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orders_total_amount_non_negative",
                table: "Orders",
                sql: "total_amount >= 0");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_created_at",
                table: "OrderItems",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_menu_item_id",
                table: "OrderItems",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "OrderItems",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_item_status",
                table: "OrderItems",
                column: "order_item_status");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_item_status_created_at",
                table: "OrderItems",
                columns: new[] { "order_item_status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_tables_capacity",
                table: "Tables",
                column: "capacity");

            migrationBuilder.CreateIndex(
                name: "ix_tables_created_at",
                table: "Tables",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_tables_table_number",
                table: "Tables",
                column: "table_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tables_table_number_created_at",
                table: "Tables",
                columns: new[] { "table_number", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_tables_table_status",
                table: "Tables",
                column: "table_status");

            migrationBuilder.CreateIndex(
                name: "ix_tables_table_status_capacity",
                table: "Tables",
                columns: new[] { "table_status", "capacity" });

            migrationBuilder.AddForeignKey(
                name: "fk_orders_tables_table_id",
                table: "Orders",
                column: "table_id",
                principalTable: "Tables",
                principalColumn: "table_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_tables_table_id",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropIndex(
                name: "ix_orders_created_at",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_customer_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_order_status",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_order_status_created_at",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_table_id",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orders_discount_non_negative",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orders_sub_total_non_negative",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orders_tax_non_negative",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orders_total_amount_consistent",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orders_total_amount_non_negative",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "order_status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "table_id",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.AlterColumn<double>(
                name: "total_amount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "tax",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "sub_total",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "discount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
