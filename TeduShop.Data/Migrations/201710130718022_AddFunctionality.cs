namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFunctionality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationFunctionalities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationFunctionalityGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        FunctionalityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.FunctionalityId })
                .ForeignKey("dbo.ApplicationFunctionalities", t => t.FunctionalityId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.FunctionalityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationFunctionalityGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationFunctionalityGroups", "FunctionalityId", "dbo.ApplicationFunctionalities");
            DropIndex("dbo.ApplicationFunctionalityGroups", new[] { "FunctionalityId" });
            DropIndex("dbo.ApplicationFunctionalityGroups", new[] { "GroupId" });
            DropTable("dbo.ApplicationFunctionalityGroups");
            DropTable("dbo.ApplicationFunctionalities");
        }
    }
}
