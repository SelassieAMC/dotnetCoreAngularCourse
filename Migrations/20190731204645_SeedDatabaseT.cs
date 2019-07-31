using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegas.Migrations
{
    public partial class SeedDatabaseT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Makes (Name) values ('Make1')");
            migrationBuilder.Sql("Insert into Makes (Name) values ('Make2')");
            migrationBuilder.Sql("Insert into Makes (Name) values ('Make3')");

            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make1-ModelA',(SELECT Id from Makes where Name = 'Make1'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make1-ModelB',(SELECT Id from Makes where Name = 'Make1'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make1-ModelC',(SELECT Id from Makes where Name = 'Make1'))");

            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make2-ModelA',(SELECT Id from Makes where Name = 'Make2'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make2-ModelB',(SELECT Id from Makes where Name = 'Make2'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make2-ModelC',(SELECT Id from Makes where Name = 'Make2'))");

            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make3-ModelA',(SELECT Id from Makes where Name = 'Make3'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make3-ModelB',(SELECT Id from Makes where Name = 'Make3'))");
            migrationBuilder.Sql("Insert into Models(Name, MakeId) values ('Make3-ModelC',(SELECT Id from Makes where Name = 'Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE From Makes");
            migrationBuilder.Sql("DELETE From Models");
        }
    }
}
