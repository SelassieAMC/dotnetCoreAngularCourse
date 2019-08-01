using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegas.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make1-ModelA'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make1-ModelA'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make1-ModelB'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make1-ModelB'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make1-ModelC'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make1-ModelC'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make2-ModelA'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make2-ModelA'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make2-ModelB'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make2-ModelB'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make2-ModelC'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make2-ModelC'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make3-ModelA'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make3-ModelA'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make3-ModelB'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make3-ModelB'))");

             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature1',(SELECT Id from Models where Name = 'Make3-ModelC'))");
             migrationBuilder.Sql("Insert into Features (Name,ModelId) values ('Feature2',(SELECT Id from Models where Name = 'Make3-ModelC'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}
