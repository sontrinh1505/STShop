namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRolePermissionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationRolePermissions", new[] { "RoleId" });
            DropPrimaryKey("dbo.ApplicationRolePermissions");
            AddColumn("dbo.ApplicationRolePermissions", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ApplicationRolePermissions", "RoleId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ApplicationRolePermissions", "Id");
            CreateIndex("dbo.ApplicationRolePermissions", "RoleId");
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationRolePermissions", new[] { "RoleId" });
            DropPrimaryKey("dbo.ApplicationRolePermissions");
            AlterColumn("dbo.ApplicationRolePermissions", "RoleId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ApplicationRolePermissions", "Id");
            AddPrimaryKey("dbo.ApplicationRolePermissions", new[] { "PermissonId", "RoleId" });
            CreateIndex("dbo.ApplicationRolePermissions", "RoleId");
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationRoles", "Id", cascadeDelete: true);
        }
    }
}
