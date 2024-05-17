using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLMEducation.Data.Migrations
{
    public partial class surv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp_ = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestQMistralId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestLlamaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestQGemmaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MistralSurvey1 = table.Column<int>(type: "int", nullable: false),
                    MistralSurvey2 = table.Column<int>(type: "int", nullable: false),
                    MistralSurvey3 = table.Column<int>(type: "int", nullable: false),
                    MistralSurvey4 = table.Column<int>(type: "int", nullable: false),
                    LLamaSurvey1 = table.Column<int>(type: "int", nullable: false),
                    LLamaSurvey2 = table.Column<int>(type: "int", nullable: false),
                    LLamaSurvey3 = table.Column<int>(type: "int", nullable: false),
                    LLamaSurvey4 = table.Column<int>(type: "int", nullable: false),
                    GemmaSurvey1 = table.Column<int>(type: "int", nullable: false),
                    GemmaSurvey2 = table.Column<int>(type: "int", nullable: false),
                    GemmaSurvey3 = table.Column<int>(type: "int", nullable: false),
                    GemmaSurvey4 = table.Column<int>(type: "int", nullable: false),
                    LastSurvey1 = table.Column<int>(type: "int", nullable: false),
                    LastSurvey2 = table.Column<int>(type: "int", nullable: false),
                    LastSurvey3 = table.Column<int>(type: "int", nullable: false),
                    LastSurvey4 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyData");
        }
    }
}
