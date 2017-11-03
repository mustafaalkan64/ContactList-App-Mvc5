namespace AdSuit.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactType = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Tags = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        UpdateDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactType = c.String(),
                        Value = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeHistories_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        EmployeeHistories_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedEmployeeId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.EmployeeTags", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeProperties", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeTags", new[] { "TagId" });
            DropIndex("dbo.EmployeeTags", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeProperties", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeHistories");
            DropTable("dbo.Tags");
            DropTable("dbo.EmployeeTags");
            DropTable("dbo.EmployeeProperties");
            DropTable("dbo.Employees");
            DropTable("dbo.ContactTypes");
        }
    }
}
