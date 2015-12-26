using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class UnitOfWork : IDisposable
    {
        private FacturatieDBContext context = new FacturatieDBContext();
        private GenericRepository<Departement> departementRepository;
        //private GenericRepository<Gebruiker> gebruikerRepository;
        //...
        private bool disposed = false;

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

        //public GenericRepository<Gebruiker> GebruikerRepository
        //{
        //    get
        //    {
        //        if (this.gebruikerRepository == null)
        //        {
        //            this.gebruikerRepository = new GenericRepository<Gebruiker>(context);
        //        }
        //        return gebruikerRepository;
        //    }
        //}

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