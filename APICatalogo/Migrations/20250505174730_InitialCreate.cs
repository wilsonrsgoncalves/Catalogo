using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APICatalogo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    imagem_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    imagem_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    estoque = table.Column<float>(type: "real", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    categoria_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produtos", x => x.id);
                    table.ForeignKey(
                        name: "fk_produtos_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_produtos_categoria_id",
                table: "produtos",
                column: "categoria_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}