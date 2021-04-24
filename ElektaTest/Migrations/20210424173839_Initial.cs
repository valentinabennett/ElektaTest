using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektaTest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EequipmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Canceled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.PatientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");
        }
    }
}
