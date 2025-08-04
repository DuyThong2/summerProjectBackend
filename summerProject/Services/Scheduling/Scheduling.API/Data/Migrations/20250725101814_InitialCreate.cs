using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduling.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    StartMonth = table.Column<int>(type: "int", nullable: false),
                    AutoRepeatMonthly = table.Column<bool>(type: "bit", nullable: false),
                    BaseTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleCollections_ScheduleTemplates_BaseTemplateId",
                        column: x => x.BaseTemplateId,
                        principalTable: "ScheduleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTemplateDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTemplateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTemplateDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleTemplateDetails_ScheduleTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "ScheduleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdHocMeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdHocMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdHocMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdHocMeals_ScheduleCollections_ScheduleCollectionId",
                        column: x => x.ScheduleCollectionId,
                        principalTable: "ScheduleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyScheduleInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppliedTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyScheduleInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyScheduleInstances_ScheduleCollections_ScheduleCollectionId",
                        column: x => x.ScheduleCollectionId,
                        principalTable: "ScheduleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonthlyScheduleInstances_ScheduleTemplates_AppliedTemplateId",
                        column: x => x.AppliedTemplateId,
                        principalTable: "ScheduleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RecurringMealRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaysOfWeek = table.Column<int>(type: "int", nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    Interval = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringMealRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringMealRules_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecurringMealRules_ScheduleCollections_ScheduleCollectionId",
                        column: x => x.ScheduleCollectionId,
                        principalTable: "ScheduleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyScheduleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthlyScheduleInstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyScheduleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyScheduleItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonthlyScheduleItems_MonthlyScheduleInstances_MonthlyScheduleInstanceId",
                        column: x => x.MonthlyScheduleInstanceId,
                        principalTable: "MonthlyScheduleInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleOverrides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementMealId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleOverrides_Meals_ReplacementMealId",
                        column: x => x.ReplacementMealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ScheduleOverrides_RecurringMealRules_TargetRuleId",
                        column: x => x.TargetRuleId,
                        principalTable: "RecurringMealRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ScheduleOverrides_ScheduleCollections_ScheduleCollectionId",
                        column: x => x.ScheduleCollectionId,
                        principalTable: "ScheduleCollections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdHocMeals_MealId",
                table: "AdHocMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_AdHocMeals_ScheduleCollectionId_Date",
                table: "AdHocMeals",
                columns: new[] { "ScheduleCollectionId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleInstances_AppliedTemplateId",
                table: "MonthlyScheduleInstances",
                column: "AppliedTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleInstances_ScheduleCollectionId_Year_Month",
                table: "MonthlyScheduleInstances",
                columns: new[] { "ScheduleCollectionId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleItems_Date_TimeSlot",
                table: "MonthlyScheduleItems",
                columns: new[] { "Date", "TimeSlot" });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleItems_MealId",
                table: "MonthlyScheduleItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleItems_MonthlyScheduleInstanceId_Date",
                table: "MonthlyScheduleItems",
                columns: new[] { "MonthlyScheduleInstanceId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyScheduleItems_Source_SourceId",
                table: "MonthlyScheduleItems",
                columns: new[] { "Source", "SourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringMealRules_MealId",
                table: "RecurringMealRules",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringMealRules_ScheduleCollectionId",
                table: "RecurringMealRules",
                column: "ScheduleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleCollections_BaseTemplateId",
                table: "ScheduleCollections",
                column: "BaseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleCollections_UserId_StartYear_StartMonth",
                table: "ScheduleCollections",
                columns: new[] { "UserId", "StartYear", "StartMonth" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOverrides_ReplacementMealId",
                table: "ScheduleOverrides",
                column: "ReplacementMealId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOverrides_ScheduleCollectionId_Date_TimeSlot",
                table: "ScheduleOverrides",
                columns: new[] { "ScheduleCollectionId", "Date", "TimeSlot" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOverrides_TargetRuleId",
                table: "ScheduleOverrides",
                column: "TargetRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTemplateDetails_MealId",
                table: "ScheduleTemplateDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTemplateDetails_TemplateId",
                table: "ScheduleTemplateDetails",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdHocMeals");

            migrationBuilder.DropTable(
                name: "MonthlyScheduleItems");

            migrationBuilder.DropTable(
                name: "ScheduleOverrides");

            migrationBuilder.DropTable(
                name: "ScheduleTemplateDetails");

            migrationBuilder.DropTable(
                name: "MonthlyScheduleInstances");

            migrationBuilder.DropTable(
                name: "RecurringMealRules");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "ScheduleCollections");

            migrationBuilder.DropTable(
                name: "ScheduleTemplates");
        }
    }
}
