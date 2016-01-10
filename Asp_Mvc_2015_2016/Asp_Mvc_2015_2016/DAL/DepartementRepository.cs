using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class DepartementRepository : GenericRepository<Departement>
    {
        public DepartementRepository(FacturatieDBContext context)
            : base(context)
        { }

        public List<Departement> getDepartementenByGebruiker(Gebruiker gebruiker)
        {
            return DbSet.Where(a => a.Gebruikers.Equals(gebruiker)).ToList();
        }
    }
}