using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NearEarthObjectsWebService.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SetupOrbitingBodies",
                columns: table => new
                {
                    OrbitingBodyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1"),
                    CreatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetupOrbitingBodies", x => x.OrbitingBodyId);
                });

            migrationBuilder.CreateTable(
                name: "Asteroids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NasaJplUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AbsoluteMagnitude = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    EstimatedDiameter = table.Column<decimal>(type: "decimal(11,10)", nullable: false),
                    CloseApproachDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RelativeVelocity = table.Column<decimal>(type: "decimal(11,10)", nullable: false),
                    MissDistance = table.Column<decimal>(type: "decimal(11,10)", nullable: false),
                    OrbitingBodyId = table.Column<int>(type: "int", nullable: false),
                    IsPotentiallyHazardous = table.Column<bool>(type: "bit", nullable: false),
                    IsSentryObject = table.Column<bool>(type: "bit", nullable: false),
                    SentryDataUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsSoftDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    CreatedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asteroids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asteroids_SetupOrbitingBody",
                        column: x => x.OrbitingBodyId,
                        principalTable: "SetupOrbitingBodies",
                        principalColumn: "OrbitingBodyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asteroids_OrbitingBodyId",
                table: "Asteroids",
                column: "OrbitingBodyId");

            migrationBuilder.Sql(@"
                INSERT INTO dbo.SetupOrbitingBodies (OrbitingBodyId, Name, IsEnabled, CreatedBy, CreatedDateTime)
                VALUES (1, 'Earth', 1, 'EF Core Migration', GETUTCDATE()),
                (2, 'Mercury', 1, 'EF Core Migration', GETUTCDATE()),
                (3, 'Venus', 1, 'EF Core Migration', GETUTCDATE()),
                (4, 'Mars', 1, 'EF Core Migration', GETUTCDATE())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asteroids");

            migrationBuilder.DropTable(
                name: "SetupOrbitingBodies");
        }
    }
}
