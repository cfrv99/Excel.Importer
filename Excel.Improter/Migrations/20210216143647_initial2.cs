using Microsoft.EntityFrameworkCore.Migrations;

namespace Excel.Improter.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasilatItkileri",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "Kesfiyyat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "MoteberliyiTesdiqlenmemishEhtiyyat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "QaliqEhtiyyat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "SenayeMenimsenilmeSahesi",
                table: "ExcelDatas");

            migrationBuilder.RenameColumn(
                name: "YenidenQiymetlendirme",
                table: "ExcelDatas",
                newName: "HasilatQaliqlari");

            migrationBuilder.RenameColumn(
                name: "TexnikiSerhedDeyishmesi",
                table: "ExcelDatas",
                newName: "FaydaliQazintiNovu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasilatQaliqlari",
                table: "ExcelDatas",
                newName: "YenidenQiymetlendirme");

            migrationBuilder.RenameColumn(
                name: "FaydaliQazintiNovu",
                table: "ExcelDatas",
                newName: "TexnikiSerhedDeyishmesi");

            migrationBuilder.AddColumn<string>(
                name: "HasilatItkileri",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kesfiyyat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoteberliyiTesdiqlenmemishEhtiyyat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QaliqEhtiyyat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenayeMenimsenilmeSahesi",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
