namespace Asp_Mvc_2015_2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GebruikerDepartements", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikerDepartements", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.KlantGebruikers", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.KlantGebruikers", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.Gebruikers", "Rol_Id", "dbo.Rollen");
            DropForeignKey("dbo.KlantFacturaties", "TypeWerk_Id", "dbo.TypeWerk");
            DropForeignKey("dbo.Adressen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Adressen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersClaim", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Klanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Klanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id1", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id1", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersLogin", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersRollen", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.TypeWerk", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.TypeWerk", "EditedById", "dbo.Gebruikers");
            DropIndex("dbo.Gebruikers", new[] { "Rol_Id" });
            DropIndex("dbo.Klanten", new[] { "Departement_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Uurregistraties", new[] { "EditedBy_Id" });
            DropIndex("dbo.KlantFacturaties", new[] { "TypeWerk_Id" });
            DropIndex("dbo.GebruikerDepartements", new[] { "Gebruiker_Id" });
            DropIndex("dbo.GebruikerDepartements", new[] { "Departement_Id" });
            DropIndex("dbo.KlantGebruikers", new[] { "Klant_Id" });
            DropIndex("dbo.KlantGebruikers", new[] { "Gebruiker_Id" });
            RenameColumn(table: "dbo.Uurregistraties", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Uurregistraties", name: "EditedBy_Id", newName: "EditedById");
            DropPrimaryKey("dbo.Gebruikers");
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
                        Departement_Id = c.Int(),
                        Gebruiker_Id = c.String(maxLength: 128),
                        Gebruiker_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Departementen", t => t.Departement_Id)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id1)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Departement_Id)
                .Index(t => t.Gebruiker_Id)
                .Index(t => t.Gebruiker_Id1);
            
            CreateTable(
                "dbo.DepartementKlanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Departement_Id = c.Int(),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Departement_Id)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.GebruikersKlanten",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(),
                        EditedById = c.String(maxLength: 128),
                        EditedOn = c.DateTime(),
                        Gebruiker_Id = c.String(maxLength: 128),
                        Klant_Id = c.Int(),
                        Gebruiker_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruikers", t => t.CreatedById)
                .ForeignKey("dbo.Gebruikers", t => t.EditedById)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .ForeignKey("dbo.Gebruikers", t => t.Gebruiker_Id1)
                .Index(t => t.CreatedById)
                .Index(t => t.EditedById)
                .Index(t => t.Gebruiker_Id)
                .Index(t => t.Klant_Id)
                .Index(t => t.Gebruiker_Id1);
            
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
            
            AddColumn("dbo.Adressen", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Adressen", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Adressen", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Adressen", "EditedOn", c => c.DateTime());
            AddColumn("dbo.Departementen", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Departementen", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Departementen", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Departementen", "EditedOn", c => c.DateTime());
            AddColumn("dbo.Gebruikers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gebruikers", "PasswordHash", c => c.String());
            AddColumn("dbo.Gebruikers", "SecurityStamp", c => c.String());
            AddColumn("dbo.Gebruikers", "PhoneNumber", c => c.String());
            AddColumn("dbo.Gebruikers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gebruikers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gebruikers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Gebruikers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gebruikers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Gebruikers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Klanten", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Klanten", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Klanten", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Klanten", "EditedOn", c => c.DateTime());
            AddColumn("dbo.Facturen", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Facturen", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Facturen", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Facturen", "EditedOn", c => c.DateTime());
            AddColumn("dbo.FactuurDetails", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.FactuurDetails", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.FactuurDetails", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.FactuurDetails", "EditedOn", c => c.DateTime());
            AddColumn("dbo.TypeWerk", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.TypeWerk", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.TypeWerk", "EditedById", c => c.String(maxLength: 128));
            AddColumn("dbo.TypeWerk", "EditedOn", c => c.DateTime());
            AlterColumn("dbo.Gebruikers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Gebruikers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Uurregistraties", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Uurregistraties", "EditedOn", c => c.DateTime());
            AlterColumn("dbo.Uurregistraties", "CreatedById", c => c.String(maxLength: 128));
            AlterColumn("dbo.Uurregistraties", "EditedById", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Gebruikers", "Id");
            CreateIndex("dbo.Adressen", "CreatedById");
            CreateIndex("dbo.Adressen", "EditedById");
            CreateIndex("dbo.Gebruikers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Departementen", "CreatedById");
            CreateIndex("dbo.Departementen", "EditedById");
            CreateIndex("dbo.Klanten", "CreatedById");
            CreateIndex("dbo.Klanten", "EditedById");
            CreateIndex("dbo.Facturen", "CreatedById");
            CreateIndex("dbo.Facturen", "EditedById");
            CreateIndex("dbo.FactuurDetails", "CreatedById");
            CreateIndex("dbo.FactuurDetails", "EditedById");
            CreateIndex("dbo.Uurregistraties", "CreatedById");
            CreateIndex("dbo.Uurregistraties", "EditedById");
            CreateIndex("dbo.TypeWerk", "CreatedById");
            CreateIndex("dbo.TypeWerk", "EditedById");
            AddForeignKey("dbo.Adressen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Adressen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Klanten", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Klanten", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Departementen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Departementen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Facturen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Facturen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.FactuurDetails", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.FactuurDetails", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.TypeWerk", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.TypeWerk", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Uurregistraties", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Uurregistraties", "EditedById", "dbo.Gebruikers", "Id");
            DropColumn("dbo.Gebruikers", "Login");
            DropColumn("dbo.Gebruikers", "Tel");
            DropColumn("dbo.Gebruikers", "Rol_Id");
            DropColumn("dbo.Klanten", "Departement_Id");
            DropTable("dbo.Rollen");
            DropTable("dbo.KlantFacturaties");
            DropTable("dbo.GebruikerDepartements");
            DropTable("dbo.KlantGebruikers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KlantGebruikers",
                c => new
                    {
                        Klant_Id = c.Int(nullable: false),
                        Gebruiker_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Klant_Id, t.Gebruiker_Id });
            
            CreateTable(
                "dbo.GebruikerDepartements",
                c => new
                    {
                        Gebruiker_Id = c.Int(nullable: false),
                        Departement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Gebruiker_Id, t.Departement_Id });
            
            CreateTable(
                "dbo.KlantFacturaties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeldigVanaf = c.DateTime(nullable: false),
                        TariefWaarde = c.Int(nullable: false),
                        TypeWerk_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rollen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolBenaming = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Klanten", "Departement_Id", c => c.Int());
            AddColumn("dbo.Gebruikers", "Rol_Id", c => c.Int());
            AddColumn("dbo.Gebruikers", "Tel", c => c.String());
            AddColumn("dbo.Gebruikers", "Login", c => c.String());
            DropForeignKey("dbo.Uurregistraties", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Uurregistraties", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersRollen", "RoleId", "dbo.Rollen");
            DropForeignKey("dbo.TypeWerk", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.TypeWerk", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.FactuurDetails", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Facturen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersRollen", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersLogin", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id1", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Departementen", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id1", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Klanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Klanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementKlanten", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.DepartementGebruikers", "Departement_Id", "dbo.Departementen");
            DropForeignKey("dbo.DepartementGebruikers", "CreatedById", "dbo.Gebruikers");
            DropForeignKey("dbo.GebruikersClaim", "UserId", "dbo.Gebruikers");
            DropForeignKey("dbo.Adressen", "EditedById", "dbo.Gebruikers");
            DropForeignKey("dbo.Adressen", "CreatedById", "dbo.Gebruikers");
            DropIndex("dbo.Rollen", "RoleNameIndex");
            DropIndex("dbo.TypeWerk", new[] { "EditedById" });
            DropIndex("dbo.TypeWerk", new[] { "CreatedById" });
            DropIndex("dbo.Uurregistraties", new[] { "EditedById" });
            DropIndex("dbo.Uurregistraties", new[] { "CreatedById" });
            DropIndex("dbo.FactuurDetails", new[] { "EditedById" });
            DropIndex("dbo.FactuurDetails", new[] { "CreatedById" });
            DropIndex("dbo.Facturen", new[] { "EditedById" });
            DropIndex("dbo.Facturen", new[] { "CreatedById" });
            DropIndex("dbo.GebruikersRollen", new[] { "RoleId" });
            DropIndex("dbo.GebruikersRollen", new[] { "UserId" });
            DropIndex("dbo.GebruikersLogin", new[] { "UserId" });
            DropIndex("dbo.GebruikersKlanten", new[] { "Gebruiker_Id1" });
            DropIndex("dbo.GebruikersKlanten", new[] { "Klant_Id" });
            DropIndex("dbo.GebruikersKlanten", new[] { "Gebruiker_Id" });
            DropIndex("dbo.GebruikersKlanten", new[] { "EditedById" });
            DropIndex("dbo.GebruikersKlanten", new[] { "CreatedById" });
            DropIndex("dbo.Klanten", new[] { "EditedById" });
            DropIndex("dbo.Klanten", new[] { "CreatedById" });
            DropIndex("dbo.DepartementKlanten", new[] { "Klant_Id" });
            DropIndex("dbo.DepartementKlanten", new[] { "Departement_Id" });
            DropIndex("dbo.DepartementKlanten", new[] { "EditedById" });
            DropIndex("dbo.DepartementKlanten", new[] { "CreatedById" });
            DropIndex("dbo.Departementen", new[] { "EditedById" });
            DropIndex("dbo.Departementen", new[] { "CreatedById" });
            DropIndex("dbo.DepartementGebruikers", new[] { "Gebruiker_Id1" });
            DropIndex("dbo.DepartementGebruikers", new[] { "Gebruiker_Id" });
            DropIndex("dbo.DepartementGebruikers", new[] { "Departement_Id" });
            DropIndex("dbo.DepartementGebruikers", new[] { "EditedById" });
            DropIndex("dbo.DepartementGebruikers", new[] { "CreatedById" });
            DropIndex("dbo.GebruikersClaim", new[] { "UserId" });
            DropIndex("dbo.Gebruikers", "UserNameIndex");
            DropIndex("dbo.Adressen", new[] { "EditedById" });
            DropIndex("dbo.Adressen", new[] { "CreatedById" });
            DropPrimaryKey("dbo.Gebruikers");
            AlterColumn("dbo.Uurregistraties", "EditedById", c => c.Int());
            AlterColumn("dbo.Uurregistraties", "CreatedById", c => c.Int());
            AlterColumn("dbo.Uurregistraties", "EditedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Uurregistraties", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Gebruikers", "Email", c => c.String());
            AlterColumn("dbo.Gebruikers", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.TypeWerk", "EditedOn");
            DropColumn("dbo.TypeWerk", "EditedById");
            DropColumn("dbo.TypeWerk", "CreatedOn");
            DropColumn("dbo.TypeWerk", "CreatedById");
            DropColumn("dbo.FactuurDetails", "EditedOn");
            DropColumn("dbo.FactuurDetails", "EditedById");
            DropColumn("dbo.FactuurDetails", "CreatedOn");
            DropColumn("dbo.FactuurDetails", "CreatedById");
            DropColumn("dbo.Facturen", "EditedOn");
            DropColumn("dbo.Facturen", "EditedById");
            DropColumn("dbo.Facturen", "CreatedOn");
            DropColumn("dbo.Facturen", "CreatedById");
            DropColumn("dbo.Klanten", "EditedOn");
            DropColumn("dbo.Klanten", "EditedById");
            DropColumn("dbo.Klanten", "CreatedOn");
            DropColumn("dbo.Klanten", "CreatedById");
            DropColumn("dbo.Gebruikers", "UserName");
            DropColumn("dbo.Gebruikers", "AccessFailedCount");
            DropColumn("dbo.Gebruikers", "LockoutEnabled");
            DropColumn("dbo.Gebruikers", "LockoutEndDateUtc");
            DropColumn("dbo.Gebruikers", "TwoFactorEnabled");
            DropColumn("dbo.Gebruikers", "PhoneNumberConfirmed");
            DropColumn("dbo.Gebruikers", "PhoneNumber");
            DropColumn("dbo.Gebruikers", "SecurityStamp");
            DropColumn("dbo.Gebruikers", "PasswordHash");
            DropColumn("dbo.Gebruikers", "EmailConfirmed");
            DropColumn("dbo.Departementen", "EditedOn");
            DropColumn("dbo.Departementen", "EditedById");
            DropColumn("dbo.Departementen", "CreatedOn");
            DropColumn("dbo.Departementen", "CreatedById");
            DropColumn("dbo.Adressen", "EditedOn");
            DropColumn("dbo.Adressen", "EditedById");
            DropColumn("dbo.Adressen", "CreatedOn");
            DropColumn("dbo.Adressen", "CreatedById");
            DropTable("dbo.Rollen");
            DropTable("dbo.GebruikersRollen");
            DropTable("dbo.GebruikersLogin");
            DropTable("dbo.GebruikersKlanten");
            DropTable("dbo.DepartementKlanten");
            DropTable("dbo.DepartementGebruikers");
            DropTable("dbo.GebruikersClaim");
            AddPrimaryKey("dbo.Gebruikers", "Id");
            RenameColumn(table: "dbo.Uurregistraties", name: "EditedById", newName: "EditedBy_Id");
            RenameColumn(table: "dbo.Uurregistraties", name: "CreatedById", newName: "CreatedBy_Id");
            CreateIndex("dbo.KlantGebruikers", "Gebruiker_Id");
            CreateIndex("dbo.KlantGebruikers", "Klant_Id");
            CreateIndex("dbo.GebruikerDepartements", "Departement_Id");
            CreateIndex("dbo.GebruikerDepartements", "Gebruiker_Id");
            CreateIndex("dbo.KlantFacturaties", "TypeWerk_Id");
            CreateIndex("dbo.Uurregistraties", "EditedBy_Id");
            CreateIndex("dbo.Uurregistraties", "CreatedBy_Id");
            CreateIndex("dbo.Klanten", "Departement_Id");
            CreateIndex("dbo.Gebruikers", "Rol_Id");
            AddForeignKey("dbo.TypeWerk", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.TypeWerk", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Uurregistraties", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Uurregistraties", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.FactuurDetails", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.FactuurDetails", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Facturen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Facturen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.GebruikersRollen", "UserId", "dbo.Gebruikers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GebruikersLogin", "UserId", "dbo.Gebruikers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id1", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.GebruikersKlanten", "Gebruiker_Id", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.GebruikersKlanten", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.GebruikersKlanten", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Departementen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Departementen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id1", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementGebruikers", "Gebruiker_Id", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementGebruikers", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Klanten", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Klanten", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementKlanten", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementKlanten", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.DepartementGebruikers", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.GebruikersClaim", "UserId", "dbo.Gebruikers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Adressen", "EditedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.Adressen", "CreatedById", "dbo.Gebruikers", "Id");
            AddForeignKey("dbo.KlantFacturaties", "TypeWerk_Id", "dbo.TypeWerk", "Id");
            AddForeignKey("dbo.Gebruikers", "Rol_Id", "dbo.Rollen", "Id");
            AddForeignKey("dbo.KlantGebruikers", "Gebruiker_Id", "dbo.Gebruikers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KlantGebruikers", "Klant_Id", "dbo.Klanten", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GebruikerDepartements", "Departement_Id", "dbo.Departementen", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GebruikerDepartements", "Gebruiker_Id", "dbo.Gebruikers", "Id", cascadeDelete: true);
        }
    }
}
