using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private FacturatieDBContext context; // = new FacturatieDBContext();
        private GenericRepository<Departement> departementRepository;
        private GenericRepository<Klant> klantRepository;
        //private GenericRepository<Gebruiker> gebruikerRepository;
        private GebruikerRepository gebruikerRepository;
        private RolesRepository rolesRepository;
        private GenericRepository<DepartementGebruiker> gebruikerDepartementRepository;
        private GenericRepository<DepartementKlant> departementKlantRepository;

        public GenericRepository<DepartementKlant> DepartementKlantRepository
        {
            get {
                if (this.departementKlantRepository == null) {
                    this.departementKlantRepository = new GenericRepository<DepartementKlant>(context);
                }
                return departementKlantRepository; 
            }            
        }

        public GenericRepository<DepartementGebruiker> GebruikerDepartementRepository
        {
            get {
                if (this.gebruikerDepartementRepository == null)
                {
                    this.gebruikerDepartementRepository = new GenericRepository<DepartementGebruiker>(context);
                }
                return gebruikerDepartementRepository; 
            }
        }
        public RolesRepository RolesRepository
        {
            get {
                if (this.rolesRepository == null){
                    this.rolesRepository = new RolesRepository(context);
                }
                return rolesRepository; 
            }
        }
        //...
        private bool disposed = false;

        public UnitOfWork() {/* EMPTY CTOR */ }
        public UnitOfWork(FacturatieDBContext context)
        {
            this.context = context;
        }

        public GenericRepository<Departement> DepartementRepository
        {
            get
            {
                if (this.departementRepository == null)
                {
                    this.departementRepository = new GenericRepository<Departement>(context);
                }
                return departementRepository;
            }
        }

        public GenericRepository<Klant> KlantRepository
        {
            get
            {
                if (this.klantRepository == null)
                {
                    this.klantRepository = new GenericRepository<Klant>(context);
                }
                return klantRepository;
            }
        }

        public DAL.GebruikerRepository GebruikerRepository
        {
            get
            {
                if (this.gebruikerRepository == null)
                {
                    this.gebruikerRepository = new DAL.GebruikerRepository(context);
                }
                return gebruikerRepository;
            }
        }

        //...

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                context.Dispose();
            }
            this.disposed = true;
        }


    }
}