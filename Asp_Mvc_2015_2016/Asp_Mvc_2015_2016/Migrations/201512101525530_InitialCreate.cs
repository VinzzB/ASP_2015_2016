namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adressen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StraatNr = c.String(),
                        Postcode = c.String(),
                        Plaats = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departementen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Omschrijving = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gebruikers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Voornaam = c.String(),
                        Achternaam = c.String(),
                        Email = c.String(),
                        Tel = c.String(),
                        Gsm = c.String(),
                        Rol_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rollen", t => t.Rol_Id)
                .Index(t => t.Rol_Id);
            
            CreateTable(
                "dbo.Klanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ondernemingsnr = c.String(),
                        NaamBedrijf = c.String(),
                        Adres_Id = c.Int(),
                        Departement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adressen", t => t.Adres_Id)
                .ForeignKey("dbo.Departementen", t => t.Departement_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.Departement_Id);
            
            CreateTable(
                "dbo.Rollen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolBenaming = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facturen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactuurJaar = c.Int(nullable: false),
                        FactuurNr = c.String(),
                        FactuurDatum = c.DateTime(nullable: false),
                        Totaal = c.Int(nullable: false),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.FactuurDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(),
                        Titel = c.String(),
                        lijnwaarde = c.Int(nullable: false),
                        Factuur_Id = c.Int(),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facturen", t => t.Factuur_Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .Index(t => t.Factuur_Id)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.Uurregistraties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDatum = c.DateTime(nullable: false),
                        EindDatum = c.DateTime(nullable: false),
                        TeFactureren = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        EditedBy_Id = c.Int(),
                        factuurDetails_Id = c.Int(),
                        TypeWerk_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Gebruikers", t => t.EditedBy_Id)
                .ForeignKey("dbo.FactuurDetails", t => t.factuurDetails_Id)
                .ForeignKey("dbo.TypeWerk", t => t.TypeWerk_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.EditedBy_Id)
                .Index(t => t.factuurDetails_Id)
                .Index(t => t.TypeWerk_Id);
            
            CreateTable(
                "dbo.TypeWerk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WerkType = c.String(),
                        GeldigVanaf = c.DateTime(nullable: false),
                        TariefPerUur = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KlantFacturaties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeldigVanaf = c.DateTime(nullable: false),
                        TariefWaarde = c.Int(nullable: false),
                        TypeWerk_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeWerk", t => t.TypeWerk_Id)
                .Index(t => t.TypeWerk_Id);
            
            CreateTable(
                "dbo.GebruikerDepartements",
                c => new
                    {
                        Gebruiker_Id = c.Int(nullable: false),
                        Departement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Gebruiker_Id, t.Departement_Id })
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departementen", t => t.Departement_Id, cascadeDelete: true)
                .Index(t => t.Gebruiker_Id)
                .Index(t => t.Departement_Id);
            
            CreateTable(
                "dbo.KlantGebruikers",
                c => new
                    {
                        Klant_Id = c.Int(nullable: false),
                        Gebruiker_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Klant_Id, t.Gebruiker_Id })
                .ForeignKey("dbo.Klanten", t => t.Klant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id, cascadeDelete: true)
                .Index(t => t.Klant_Id)
                .Index(t => t.Gebruiker_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantFacturaties", "TypeWerk_Id", "dbo.TypeWerk");
            DropForeignKey("dbo.Uurregistraties", "TypeWerk_Id", "dbo.TypeWerk");
            DropForeignKey("dbo.Uurregistraties", "factuurDetails_Id", "dbo.FactuurDetails");
            DropForeignKey("dbo.Uurregistraties", "EditedBy_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "CreatedBy_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.FactuurDetails", "Factuur_Id", "dbo.Facturen");
            DropForeignKey("dbo.Facturen", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Klanten", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.Gebruikers", "Rol_Id", "dbo.Rollen");
            DropForeignKey("dbo.KlantGebruikers", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.KlantGebruikers", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Klanten", "Adres_Id", "dbo.Adressen");
            DropForeignKey("dbo.GebruikerDepartements", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.GebruikerDepartements", "Gebruiker_Id", "dbo.Gebruikers");
            DropIndex("dbo.KlantGebruikers", new[] { "Gebruiker_Id" });
            DropIndex("dbo.KlantGebruikers", new[] { "Klant_Id" });
            DropIndex("dbo.GebruikerDepartements", new[] { "Departement_Id" });
            DropIndex("dbo.GebruikerDepartements", new[] { "Gebruiker_Id" });
            DropIndex("dbo.KlantFacturaties", new[] { "TypeWerk_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "TypeWerk_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "factuurDetails_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "EditedBy_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "CreatedBy_Id" });
            DropIndex("dbo.FactuurDetails", new[] { "Klant_Id" });
            DropIndex("dbo.FactuurDetails", new[] { "Factuur_Id" });
            DropIndex("dbo.Facturen", new[] { "Klant_Id" });
            DropIndex("dbo.Klanten", new[] { "Departement_Id" });
            DropIndex("dbo.Klanten", new[] { "Adres_Id" });
            DropIndex("dbo.Gebruikers", new[] { "Rol_Id" });
            DropTable("dbo.KlantGebruikers");
            DropTable("dbo.GebruikerDepartements");
            DropTable("dbo.KlantFacturaties");
            DropTable("dbo.TypeWerk");
            DropTable("dbo.Uurregistraties");
            DropTable("dbo.FactuurDetails");
            DropTable("dbo.Facturen");
            DropTable("dbo.Rollen");
            DropTable("dbo.Klanten");
            DropTable("dbo.Gebruikers");
            DropTable("dbo.Departementen");
            DropTable("dbo.Adressen");
        }
    }
}
