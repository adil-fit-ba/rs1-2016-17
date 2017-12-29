namespace CondorExtreme3.Migrations.Configuration2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nesto = c.String(),
                        PostalCode = c.String(),
                        CountryID = c.Int(nullable: false),
                        Guid = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Guid = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.RegisteredVisitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CityID = c.Int(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Guid = c.String(),
                        VirtualPointsTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.SalesOrderVirtualPoints",
                c => new
                    {
                        SalesOrderNumber = c.Int(nullable: false, identity: true),
                        VirtualPointsPackID = c.Int(nullable: false),
                        VisitorID = c.Int(nullable: false),
                        Guid = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SalesOrderNumber)
                .ForeignKey("dbo.RegisteredVisitors", t => t.VisitorID)
                .ForeignKey("dbo.VirtualPointsPacks", t => t.VirtualPointsPackID)
                .Index(t => t.VirtualPointsPackID)
                .Index(t => t.VisitorID);
            
            CreateTable(
                "dbo.VirtualPointsPacks",
                c => new
                    {
                        VirtualPointsPackID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Guid = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VirtualPointsPackID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        VisitorID = c.Int(nullable: false),
                        ProjectionId = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        ReservationCompleted = c.Boolean(nullable: false),
                        Guid = c.String(),
                        ConnectionString = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.RegisteredVisitors", t => t.VisitorID)
                .Index(t => t.VisitorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "VisitorID", "dbo.RegisteredVisitors");
            DropForeignKey("dbo.SalesOrderVirtualPoints", "VirtualPointsPackID", "dbo.VirtualPointsPacks");
            DropForeignKey("dbo.SalesOrderVirtualPoints", "VisitorID", "dbo.RegisteredVisitors");
            DropForeignKey("dbo.RegisteredVisitors", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryID", "dbo.Countries");
            DropIndex("dbo.Reservations", new[] { "VisitorID" });
            DropIndex("dbo.SalesOrderVirtualPoints", new[] { "VisitorID" });
            DropIndex("dbo.SalesOrderVirtualPoints", new[] { "VirtualPointsPackID" });
            DropIndex("dbo.RegisteredVisitors", new[] { "CityID" });
            DropIndex("dbo.Cities", new[] { "CountryID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.VirtualPointsPacks");
            DropTable("dbo.SalesOrderVirtualPoints");
            DropTable("dbo.RegisteredVisitors");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
