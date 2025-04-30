using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populando Categorias
            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "categoriaid", "nome", "imagemurl" },
                values: new object[,]
            {
                { 1, "Bebidas", "bebidas.jpg" },
                { 2, "Lanches", "lanches.jpg" },
                { 3, "Sobremesas", "sobremesas.jpg" },
                { 4, "Salgados", "salgados.jpg" },
                { 5, "Saudáveis", "saudaveis.jpg" }
            });

            // Populando Produtos
            migrationBuilder.InsertData(
                table: "produtos",
                columns: new[] { "nome", "descricao", "preco", "imagemurl", "estoque", "datacadastro", "categoriaid" },
                values: new object[,]
                {
                // Bebidas
                { "Coca-Cola Lata", "Refrigerante 350ml gelado", 5.00m, "coca.jpg", 100f, DateTime.UtcNow, 1 },
                { "Suco Natural", "Suco de laranja 300ml", 6.50m, "suco.jpg", 50f, DateTime.UtcNow, 1 },
                { "Água sem gás", "Garrafa 500ml", 3.00m, "agua.jpg", 200f, DateTime.UtcNow, 1 },
                { "Energético Red", "Bebida energética 250ml", 9.00m, "energetico.jpg", 40f, DateTime.UtcNow, 1 },
                { "Café expresso", "Café quente 100ml", 4.00m, "cafe.jpg", 75f, DateTime.UtcNow, 1 },

                // Lanches
                { "X-Burguer", "Pão, carne e queijo", 12.00m, "xburguer.jpg", 60f, DateTime.UtcNow, 2 },
                { "Cheeseburguer", "Com cheddar e molho especial", 14.00m, "cheeseburguer.jpg", 55f, DateTime.UtcNow, 2 },
                { "X-Bacon", "Hambúrguer com bacon", 15.50m, "xbacon.jpg", 45f, DateTime.UtcNow, 2 },
                { "X-Frango", "Hambúrguer de frango", 13.00m, "xfrango.jpg", 50f, DateTime.UtcNow, 2 },
                { "Veggie Burguer", "Hambúrguer vegetariano", 14.00m, "veggie.jpg", 30f, DateTime.UtcNow, 2 },

                });
        }

        /// <inheritdoc />  
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "produtos",
                keyColumn: "id",
                keyValues: Enumerable.Range(1, 25).Cast<object>().ToArray()
            );
            migrationBuilder.DeleteData(
                table: "produtos",
                keyColumn: "id",
                keyValues: Enumerable.Range(1, 25).Cast<object>().ToArray()
            );

            migrationBuilder.DeleteData(
                table: "categorias",
                keyColumn: "id",
                keyValues: Enumerable.Range(1, 5).Cast<object>().ToArray()
            );
        }
    }
}
