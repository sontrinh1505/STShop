namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStruture : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            CreateTable(
                "dbo.ApplicationRolePermissions",
                c => new
                    {
                        PermissonId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PermissonId, t.RoleId })
                .ForeignKey("dbo.ApplicationPermissions", t => t.PermissonId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissonId)
                .Index(t => t.RoleId);
            
            DropTable("dbo.ApplicationRoleGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId });
            
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRolePermissions", "PermissonId", "dbo.ApplicationPermissions");
            DropIndex("dbo.ApplicationRolePermissions", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRolePermissions", new[] { "PermissonId" });
            DropTable("dbo.ApplicationRolePermissions");
            CreateIndex("dbo.ApplicationRoleGroups", "RoleId");
            CreateIndex("dbo.ApplicationRoleGroups", "GroupId");
            AddForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups", "ID", cascadeDelete: true);
        }
    }
}
