using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;
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
        private GenericRepository<FactuurRepository> factuurRepository;
        //private GenericRepository<Gebruiker> gebruikerRepository;
        private GebruikerRepository gebruikerRepository;
        private RolesRepository rolesRepository;
        private GenericRepository<DepartementGebruiker> gebruikerDepartementRepository;
        private GenericRepository<DepartementKlant> departementKlantRepository;
        private UurRegistratieRepository uurRegistratieRepository;
        private FactuurDetailsRepository factuurDetailsRepository;
        private GenericRepository<TypeWerk> typeWerkRepository;

        public GenericRepository<TypeWerk> TypeWerkRepository
        {
            get {
                if (this.typeWerkRepository == null)
                    this.typeWerkRepository = new GenericRepository<TypeWerk>(context);
                return typeWerkRepository; 
            }
        }

        public FactuurDetailsRepository FactuurDetailsRepository
        {
            get {
                if (this.factuurDetailsRepository == null)
                    this.factuurDetailsRepository = new FactuurDetailsRepository(context);
                return factuurDetailsRepository;
            }            
        }

        public UurRegistratieRepository UurRegistratieRepository
        {
            get {
                if (this.uurRegistratieRepository == null)
                    uurRegistratieRepository = new UurRegistratieRepository(context);
                return uurRegistratieRepository; 
            }
        }

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
     //   private bool disposed = false;

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

        public GenericRepository<Factuur> FactuurRepository
        {
            get
            {
                if (this.factuurRepository == null)
                {
                    this.factuurRepository = new GenericRepository<Factuur>(context);
                }
                return factuurRepository;
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

        public GebruikerRepository GebruikerRepository
        {
            get
            {
                if (this.gebruikerRepository == null)
                {
                    this.gebruikerRepository = new GebruikerRepository(context);
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
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
            if (gebruikerRepository != null)
            {
                gebruikerRepository.Dispose();
                gebruikerRepository = null;
            }
        }
    }
}