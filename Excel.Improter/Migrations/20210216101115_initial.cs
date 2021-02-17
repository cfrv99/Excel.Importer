using Microsoft.EntityFrameworkCore.Migrations;

namespace Excel.Improter.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YataginInzibatiErazisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YataginKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YataginAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaheninKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaheninAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DMAANomresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DMAABitmeTarix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DMAAQeydiyyatTarixi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenayeMenimsenilmeSahesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhtiyyatinKategoryasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IstisimarVeziyyeti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlkinEhtiyyat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlUzreHasilatCemi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasilatItkileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kesfiyyat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YenidenQiymetlendirme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoteberliyiTesdiqlenmemishEhtiyyat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TexnikiSerhedDeyishmesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QaliqEhtiyyat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AyrilanSahe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VOEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MilliGealojiKeshfiyyat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kordinat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedaxilVeziyyeti = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelDatas");
        }
    }
}
