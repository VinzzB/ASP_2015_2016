using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class KlantRepository : GenericRepository<Klant>
    {
        public KlantRepository(FacturatieDBContext context)
            : base(context)
        { }

        public IEnumerable<Klant> GetKlanten(String uid)
        {
            return context.Klanten.Where(k => //alle klanten
                k.Departementen.Any(d =>                       //alle departementen van de klant
                    d.Departement.Gebruikers.Any(g =>          //gebruikers in een departement
                        g.GebruikerId == uid)));               //bevat huidige gebruiker?                
        }

    }
}