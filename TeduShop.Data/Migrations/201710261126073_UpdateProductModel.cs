namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductType");
        }
    }
}
