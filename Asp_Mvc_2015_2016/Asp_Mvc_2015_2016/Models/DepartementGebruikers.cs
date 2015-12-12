using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Asp_Mvc_2015_2016.Models
{
    public class DepartementGebruiker // : _BaseInfo //--> loopt fout, maakt dubbele kolom gebruiker_id aan???
    {
        public int Id { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual Gebruiker Gebruiker { get; set; }
    }
}