namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departementen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Omschrijving = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById);
            
            CreateTable(
                "dbo.Gebruikers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Voornaam = c.String(nullable: false),
                        Achternaam = c.String(nullable: false),
                        Gsm = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.GebruikersClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DepartementGebruikers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Gebruiker_Id = c.String(nullable: false, maxLength: 128),
                        Departement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departementen", t => t.Departement_Id, cascadeDelete: true)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Gebruiker_Id)
                .Index(t => t.Departement_Id);
            
            CreateTable(
                "dbo.Facturen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactuurJaar = c.Int(nullable: false),
                        FactuurNr = c.String(),
                        FactuurDatum = c.DateTime(nullable: false),
                        Totaal = c.Int(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.Klanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ondernemingsnr = c.String(),
                        NaamBedrijf = c.String(),
                        StraatNr = c.String(),
                        Postcode = c.String(),
                        Adres = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById);
            
            CreateTable(
                "dbo.DepartementKlanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Klant_Id = c.Int(nullable: false),
                        Departement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departementen", t => t.Departement_Id, cascadeDelete: true)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Klant_Id)
                .Index(t => t.Departement_Id);
            
            CreateTable(
                "dbo.GebruikersKlanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Klant_Id = c.Int(nullable: false),
                        Gebruiker_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id, cascadeDelete: true)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Klant_Id)
                .Index(t => t.Gebruiker_Id);
            
            CreateTable(
                "dbo.FactuurDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(),
                        Titel = c.String(),
                        lijnwaarde = c.Int(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.Uurregistraties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDatum = c.DateTime(nullable: false),
                        EindDatum = c.DateTime(nullable: false),
                        TeFactureren = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Factuur_Id = c.Int(),
                        factuurDetails_Id = c.Int(),
                        TypeWerk_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facturen", t => t.Factuur_Id)
                .ForeignKey("dbo.FactuurDetails", t => t.factuurDetails_Id)
                .ForeignKey("dbo.TypeWerk", t => t.TypeWerk_Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Factuur_Id)
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
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById);
            
            CreateTable(
                "dbo.GebruikersLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Gebruikers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GebruikersRollen",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Gebruikers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Rollen", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Rollen",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GebruikersRollen", "RoleId", "dbo.Rollen");
            DropForeignKey("dbo.DepartementKlanten", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.DepartementGebruikers", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.Uurregistraties", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersRollen", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersLogin", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.Klanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Klanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "TypeWerk_Id", "dbo.TypeWerk");
            DropForeignKey("dbo.TypeWerk", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.TypeWerk", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "factuurDetails_Id", "dbo.FactuurDetails");
            DropForeignKey("dbo.Uurregistraties", "Factuur_Id", "dbo.Facturen");
            DropForeignKey("dbo.FactuurDetails", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Facturen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.GebruikersKlanten", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.GebruikersKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.DepartementKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersClaim", "UserId", "dbo.Gebruikers");
            DropIndex("dbo.Rollen", "RoleNameIndex");
            DropIndex("dbo.GebruikersRollen", new[] { "RoleId" });
            DropIndex("dbo.GebruikersRollen", new[] { "UserId" });
            DropIndex("dbo.GebruikersLogin", new[] { "UserId" });
            DropIndex("dbo.TypeWerk", new[] { "EditedById" });
            DropIndex("dbo.TypeWerk", new[] { "CreatedById" });
            DropIndex("dbo.Uurregistraties", new[] { "TypeWerk_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "factuurDetails_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "Factuur_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "EditedById" });
            DropIndex("dbo.Uurregistraties", new[] { "CreatedById" });
            DropIndex("dbo.FactuurDetails", new[] { "Klant_Id" });
            DropIndex("dbo.FactuurDetails", new[] { "EditedById" });
            DropIndex("dbo.FactuurDetails", new[] { "CreatedById" });
            DropIndex("dbo.GebruikersKlanten", new[] { "Gebruiker_Id" });
            DropIndex("dbo.GebruikersKlanten", new[] { "Klant_Id" });
            DropIndex("dbo.GebruikersKlanten", new[] { "EditedById" });
            DropIndex("dbo.GebruikersKlanten", new[] { "CreatedById" });
            DropIndex("dbo.DepartementKlanten", new[] { "Departement_Id" });
            DropIndex("dbo.DepartementKlanten", new[] { "Klant_Id" });
            DropIndex("dbo.DepartementKlanten", new[] { "EditedById" });
            DropIndex("dbo.DepartementKlanten", new[] { "CreatedById" });
            DropIndex("dbo.Klanten", new[] { "EditedById" });
            DropIndex("dbo.Klanten", new[] { "CreatedById" });
            DropIndex("dbo.Facturen", new[] { "Klant_Id" });
            DropIndex("dbo.Facturen", new[] { "EditedById" });
            DropIndex("dbo.Facturen", new[] { "CreatedById" });
            DropIndex("dbo.DepartementGebruikers", new[] { "Departement_Id" });
            DropIndex("dbo.DepartementGebruikers", new[] { "Gebruiker_Id" });
            DropIndex("dbo.DepartementGebruikers", new[] { "EditedById" });
            DropIndex("dbo.DepartementGebruikers", new[] { "CreatedById" });
            DropIndex("dbo.GebruikersClaim", new[] { "UserId" });
            DropIndex("dbo.Gebruikers", "UserNameIndex");
            DropIndex("dbo.Departementen", new[] { "EditedById" });
            DropIndex("dbo.Departementen", new[] { "CreatedById" });
            DropTable("dbo.Rollen");
            DropTable("dbo.GebruikersRollen");
            DropTable("dbo.GebruikersLogin");
            DropTable("dbo.TypeWerk");
            DropTable("dbo.Uurregistraties");
            DropTable("dbo.FactuurDetails");
            DropTable("dbo.GebruikersKlanten");
            DropTable("dbo.DepartementKlanten");
            DropTable("dbo.Klanten");
            DropTable("dbo.Facturen");
            DropTable("dbo.DepartementGebruikers");
            DropTable("dbo.GebruikersClaim");
            DropTable("dbo.Gebruikers");
            DropTable("dbo.Departementen");
        }
    }
}
