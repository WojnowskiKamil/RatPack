namespace Rat_Pack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatabaseModel01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Package_Id = c.Guid(),
                        CreatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Package_Id)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.Int(nullable: false),
                        Description = c.String(),
                        CityStart = c.String(),
                        CityEnd = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Lenght = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdditionalInfo = c.String(),
                        Truck_Id = c.Guid(),
                        CreatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Trucks", t => t.Truck_Id)
                .Index(t => t.Truck_Id)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.Trucks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CarBrand = c.String(),
                        CarName = c.String(),
                        CarType = c.Int(nullable: false),
                        HomeCity = c.String(),
                        Lenght = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Load = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdditionalInfo = c.String(),
                        Location = c.String(),
                        CreatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .Index(t => t.CreatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Packages", "Truck_Id", "dbo.Trucks");
            DropForeignKey("dbo.Trucks", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Packages", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trucks", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Packages", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Packages", new[] { "Truck_Id" });
            DropIndex("dbo.Messages", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Messages", new[] { "Package_Id" });
            DropTable("dbo.Trucks");
            DropTable("dbo.Packages");
            DropTable("dbo.Messages");
        }
    }
}
