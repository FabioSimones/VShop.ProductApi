using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)" + 
                "Values('Caderno', 7.55, 'Caderno', 10, 'caderno1.jpg', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)" +
                "Values('Lápis', 2.55, 'Lápis', 10, 'lapis1.jpg', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)" +
                "Values('Clips', 5.55, 'Clips para papel', 50, 'clips1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Products");
        }
    }
}
