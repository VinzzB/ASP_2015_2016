using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("DepartementKlanten")]
    public class DepartementKlanten : _BaseInfo
    {        
        public virtual Departement Departement { get; set; }
        public virtual Klant Klant { get; set; }
    }
}