using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Database_Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _model = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    _plate = table.Column<string>(type: "text", nullable: true),
                    Plate = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    IsLeased = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bike", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cnh_Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cnh_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Num_Days = table.Column<int>(type: "integer", nullable: false),
                    Daily_Value = table.Column<double>(type: "double precision", nullable: false),
                    Fine_Percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _name = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _name = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date_Order = table.Column<DateOnly>(type: "date", nullable: false),
                    Value_Order = table.Column<double>(type: "double precision", nullable: false),
                    StatusOrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Status_Order_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "Status_Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CNH = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    Path_CNH_Image = table.Column<string>(type: "text", nullable: true),
                    Is_Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Is_Delivering = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Neighborhood = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    UserTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CnhTypeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Cnh_Type_CnhTypeId",
                        column: x => x.CnhTypeId,
                        principalTable: "Cnh_Type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_Type_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "User_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Notification_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accepted_Orders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Accepted_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accepted_Orders", x => new { x.UserId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Accepted_Orders_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accepted_Orders_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivered_Orders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Delivered_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivered_Orders", x => new { x.UserId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Delivered_Orders_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivered_Orders_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Creation_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Predition_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Initial_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Total_Value = table.Column<double>(type: "double precision", nullable: false),
                    Num_More_Dailys = table.Column<int>(type: "integer", nullable: false),
                    Is_Active = table.Column<bool>(type: "boolean", nullable: false),
                    BikeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rental_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificated_User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notification_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificated_User", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_Notificated_User_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificated_User_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cnh_Type",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11dcfe4c-c66a-47de-8b88-81f830919085"), "A" },
                    { new Guid("63ad311d-c292-4374-8916-eb083be610b8"), "AB" },
                    { new Guid("9b39cb59-7300-4634-a23b-5cac5017dbb6"), "B" },
                    { new Guid("f4958544-c796-43f4-a3cb-568ef6272a8d"), "NA" }
                });

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "Id", "Daily_Value", "Description", "Fine_Percentage", "Name", "Num_Days" },
                values: new object[,]
                {
                    { new Guid("4183ed75-08df-4272-8c91-44fd63c43dae"), 22.0, "Plano de locação de 30 dias", 60, "Plano 30 dias", 30 },
                    { new Guid("71cada4b-0fb0-40be-92f0-fe9b1534816c"), 30.0, "Plano de locação de 7 dias", 20, "Plano 7 dias", 7 },
                    { new Guid("f492d76b-e54b-4d86-8772-bcc095cc8167"), 28.0, "Plano de locação de 15 dias", 40, "Plano 15 dias", 15 }
                });

            migrationBuilder.InsertData(
                table: "Status_Order",
                columns: new[] { "Id", "Name", "_name" },
                values: new object[,]
                {
                    { new Guid("2cb64fd3-7080-41ea-9574-af6b2a16f8e9"), "DISPONÍVEL", "DISPONÍVEL" },
                    { new Guid("a0996df0-919c-4873-8f22-20cf5d680da0"), "ACEITO", "ACEITO" },
                    { new Guid("d1979c80-6c15-4268-9e68-010f53570498"), "ENTREGUE", "ENTREGUE" }
                });

            migrationBuilder.InsertData(
                table: "User_Type",
                columns: new[] { "Id", "Name", "_name" },
                values: new object[,]
                {
                    { new Guid("79fd21e3-3127-4dda-ba1b-17d20a9d1d3e"), "CONSUMIDOR", "CONSUMIDOR" },
                    { new Guid("bbd4ed2e-5f92-414c-9f7b-e7395c464898"), "ENTREGADOR", "ENTREGADOR" },
                    { new Guid("f6a2372a-b146-45f9-be70-a0be13736dd8"), "ADMINISTRADOR", "ADMINISTRADOR" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "BirthDate", "City", "CNH", "CnhTypeId", "CNPJ", "Is_Active", "Login", "Name", "Neighborhood", "Password", "Path_CNH_Image", "State", "UserTypeId", "ZipCode" },
                values: new object[] { new Guid("5d277ec2-3b50-4920-9bab-422693f6017b"), null, null, null, "***", null, "***", true, "admin", "administrator", null, "123", null, null, new Guid("f6a2372a-b146-45f9-be70-a0be13736dd8"), null });

            migrationBuilder.CreateIndex(
                name: "IX_Accepted_Orders_OrderId",
                table: "Accepted_Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "Plate",
                table: "Bike",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivered_Orders_OrderId",
                table: "Delivered_Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificated_User_NotificationId",
                table: "Notificated_User",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OrderId",
                table: "Notification",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusOrderId",
                table: "Order",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_BikeId",
                table: "Rental",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_PlanId",
                table: "Rental",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_UserId",
                table: "Rental",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CnhTypeId",
                table: "User",
                column: "CnhTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accepted_Orders");

            migrationBuilder.DropTable(
                name: "Delivered_Orders");

            migrationBuilder.DropTable(
                name: "Notificated_User");

            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Bike");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Cnh_Type");

            migrationBuilder.DropTable(
                name: "User_Type");

            migrationBuilder.DropTable(
                name: "Status_Order");
        }
    }
}
