namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupMenuf : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MenuGroups", name: "ChildId", newName: "ParentId");
            RenameIndex(table: "dbo.MenuGroups", name: "IX_ChildId", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MenuGroups", name: "IX_ParentId", newName: "IX_ChildId");
            RenameColumn(table: "dbo.MenuGroups", name: "ParentId", newName: "ChildId");
        }
    }
}
