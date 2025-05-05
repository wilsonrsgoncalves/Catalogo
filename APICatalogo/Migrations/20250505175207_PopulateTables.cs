using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatalogo.Migrations
{
    public partial class PopulateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO categorias (nome, imagem_url)
                VALUES 
                    ('lanches', 'https://example.com/imagens/lanches.jpg'),
                    ('bebidas', 'https://example.com/imagens/bebidas.jpg'),
                    ('acompanhamentos', 'https://example.com/imagens/acompanhamentos.jpg'),
                    ('sobremesas', 'https://example.com/imagens/sobremesas.jpg');
            ");
         
            migrationBuilder.Sql(@"
                INSERT INTO produtos (nome, descricao, preco, imagem_url, estoque, data_cadastro, categoria_id)
                VALUES 
                    -- Lanches (categoria_id = 1)
                    ('hamburguer clássico', 'pão, carne, queijo, alface, tomate e maionese', 15.90, 'https://example.com/imagens/hamburguer.jpg', 50, '2025-05-05 10:00:00', 1),
                    ('cheeseburger duplo', 'pão, duas carnes, queijo cheddar, bacon e molho especial', 22.50, 'https://example.com/imagens/cheeseburger.jpg', 30, '2025-05-05 10:00:00', 1),
                    ('sanduíche de frango', 'pão, frango grelhado, alface, tomate e maionese', 18.90, 'https://example.com/imagens/sanduiche_frango.jpg', 40, '2025-05-05 10:00:00', 1),

                    -- Bebidas (categoria_id = 2)
                    ('refrigerante 350ml', 'refrigerante de cola gelado em lata de 350ml', 5.50, 'https://example.com/imagens/refrigerante.jpg', 100, '2025-05-05 10:00:00', 2),
                    ('suco de laranja', 'suco natural de laranja, 300ml', 7.90, 'https://example.com/imagens/suco_laranja.jpg', 60, '2025-05-05 10:00:00', 2),
                    ('água mineral', 'água mineral com ou sem gás, 500ml', 3.50, 'https://example.com/imagens/agua.jpg', 80, '2025-05-05 10:00:00', 2),

                    -- Acompanhamentos (categoria_id = 3)
                    ('batata frita média', 'porção de batata frita crocante, 150g', 8.90, 'https://example.com/imagens/batata_frita.jpg', 70, '2025-05-05 10:00:00', 3),
                    ('onion rings', 'porção de anéis de cebola empanados, 120g', 10.50, 'https://example.com/imagens/onion_rings.jpg', 50, '2025-05-05 10:00:00', 3),

                    -- Sobremesas (categoria_id = 4)
                    ('milkshake de chocolate', 'milkshake cremoso de chocolate, 400ml', 12.90, 'https://example.com/imagens/milkshake.jpg', 25, '2025-05-05 10:00:00', 4),
                    ('sundae de caramelo', 'sorvete de baunilha com calda de caramelo e castanhas', 9.90, 'https://example.com/imagens/sundae.jpg', 35, '2025-05-05 10:00:00', 4);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.Sql("DELETE FROM produtos;");
            migrationBuilder.Sql("DELETE FROM categorias;");
        }
    }
}