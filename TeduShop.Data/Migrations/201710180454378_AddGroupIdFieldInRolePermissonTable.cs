namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupIdFieldInRolePermissonTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationRolePermissions", "GroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationRolePermissions", "GroupId");
        }
    }
}
