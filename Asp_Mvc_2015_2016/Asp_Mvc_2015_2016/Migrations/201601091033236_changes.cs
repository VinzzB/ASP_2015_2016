namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DepartementGebruikers", name: "Departement_Id", newName: "DepartementId");
            RenameIndex(table: "dbo.DepartementGebruikers", name: "IX_Departement_Id", newName: "IX_DepartementId");
            AddColumn("dbo.DepartementGebruikers", "GebruikerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartementGebruikers", "GebruikerId");
            RenameIndex(table: "dbo.DepartementGebruikers", name: "IX_DepartementId", newName: "IX_Departement_Id");
            RenameColumn(table: "dbo.DepartementGebruikers", name: "DepartementId", newName: "Departement_Id");
        }
    }
}
