namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CakeImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Card", "CakeImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Card", "CakeImage");
        }
    }
}
