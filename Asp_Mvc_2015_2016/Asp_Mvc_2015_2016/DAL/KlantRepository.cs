﻿using Asp_Mvc_2015_2016.Models;
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
        {
            
        }
    }
}