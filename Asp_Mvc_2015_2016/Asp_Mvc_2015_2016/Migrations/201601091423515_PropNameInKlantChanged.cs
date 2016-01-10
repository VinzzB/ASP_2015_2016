namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropNameInKlantChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klanten", "Plaats", c => c.String());
            DropColumn("dbo.Klanten", "Adres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Klanten", "Adres", c => c.String());
            DropColumn("dbo.Klanten", "Plaats");
        }
    }
}
