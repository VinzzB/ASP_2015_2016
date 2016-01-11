namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DepartementGebruikers", new[] { "Gebruiker_Id" });
            DropColumn("dbo.DepartementGebruikers", "GebruikerId");
            RenameColumn(table: "dbo.DepartementGebruikers", name: "Gebruiker_Id", newName: "GebruikerId");
            AlterColumn("dbo.DepartementGebruikers", "GebruikerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DepartementGebruikers", "GebruikerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DepartementGebruikers", new[] { "GebruikerId" });
            AlterColumn("dbo.DepartementGebruikers", "GebruikerId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.DepartementGebruikers", name: "GebruikerId", newName: "Gebruiker_Id");
            AddColumn("dbo.DepartementGebruikers", "GebruikerId", c => c.Int(nullable: false));
            CreateIndex("dbo.DepartementGebruikers", "Gebruiker_Id");
        }
    }
}
