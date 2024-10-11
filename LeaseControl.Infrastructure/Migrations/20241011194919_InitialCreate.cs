using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliverymens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CNH = table.Column<string>(type: "text", nullable: false),
                    TypeCnh = table.Column<string>(type: "text", nullable: false),
                    ImageCnh = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverymens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Plate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanLease",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DailyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    DurationDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanLease", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leasess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryManId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanLeaseId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValueTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leasess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leasess_Deliverymens_DeliveryManId",
                        column: x => x.DeliveryManId,
                        principalTable: "Deliverymens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leasess_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leasess_PlanLease_PlanLeaseId",
                        column: x => x.PlanLeaseId,
                        principalTable: "PlanLease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlanLease",
                columns: new[] { "Id", "DailyRate", "DurationDays", "Name" },
                values: new object[,]
                {
                    { 1, 30m, 7, "7 Dias" },
                    { 2, 28m, 15, "15 Dias" },
                    { 3, 22m, 30, "30 Dias" },
                    { 4, 20m, 45, "45 Dias" },
                    { 5, 18m, 50, "50 Dias" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymens_CNH",
                table: "Deliverymens",
                column: "CNH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymens_CNPJ",
                table: "Deliverymens",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leasess_DeliveryManId",
                table: "Leasess",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_Leasess_MotorcycleId",
                table: "Leasess",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Leasess_PlanLeaseId",
                table: "Leasess",
                column: "PlanLeaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Plate",
                table: "Motorcycles",
                column: "Plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leasess");

            migrationBuilder.DropTable(
                name: "Deliverymens");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "PlanLease");
        }
    }
}
