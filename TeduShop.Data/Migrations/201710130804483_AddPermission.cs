namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationFunctionalities", newName: "ApplicationPermissions");
            DropForeignKey("dbo.ApplicationFunctionalityGroups", "FunctionalityId", "dbo.ApplicationFunctionalities");
            DropForeignKey("dbo.ApplicationFunctionalityGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ApplicationFunctionalityGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationFunctionalityGroups", new[] { "FunctionalityId" });
            CreateTable(
                "dbo.ApplicationPermissionGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.PermissionId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationPermissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.PermissionId);
            
            DropTable("dbo.ApplicationFunctionalityGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationFunctionalityGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        FunctionalityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.FunctionalityId });
            
            DropForeignKey("dbo.ApplicationPermissionGroups", "PermissionId", "dbo.ApplicationPermissions");
            DropForeignKey("dbo.ApplicationPermissionGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ApplicationPermissionGroups", new[] { "PermissionId" });
            DropIndex("dbo.ApplicationPermissionGroups", new[] { "GroupId" });
            DropTable("dbo.ApplicationPermissionGroups");
            CreateIndex("dbo.ApplicationFunctionalityGroups", "FunctionalityId");
            CreateIndex("dbo.ApplicationFunctionalityGroups", "GroupId");
            AddForeignKey("dbo.ApplicationFunctionalityGroups", "GroupId", "dbo.ApplicationGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationFunctionalityGroups", "FunctionalityId", "dbo.ApplicationFunctionalities", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.ApplicationPermissions", newName: "ApplicationFunctionalities");
        }
    }
}
