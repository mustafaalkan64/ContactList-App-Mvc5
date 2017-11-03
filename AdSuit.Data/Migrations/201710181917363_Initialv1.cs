namespace AdSuit.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialv1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeHistories", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeHistories", "UpdateDate", c => c.DateTime(nullable: false));
        }
    }
}
