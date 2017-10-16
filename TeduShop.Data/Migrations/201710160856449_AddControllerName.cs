namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddControllerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationPermissions", "ControllerName", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationPermissions", "ControllerName");
        }
    }
}
