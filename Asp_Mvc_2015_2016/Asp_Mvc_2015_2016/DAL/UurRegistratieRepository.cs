using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class UurRegistratieRepository : GenericRepository<UurRegistratie>
    {
        public UurRegistratieRepository(FacturatieDBContext context)
            : base(context)
        {
            
        }
    }
}