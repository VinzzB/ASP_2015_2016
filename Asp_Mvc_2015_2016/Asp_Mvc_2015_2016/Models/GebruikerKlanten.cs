using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace Asp_Mvc_2015_2016.Models
{
    [Table("GebruikersKlanten")]
    public class GebruikerKlanten // : _BaseInfo //--> loopt fout, maakt dubbele kolom gebruiker_id aan???
    {
        public int Id { get; set; }
        public virtual Gebruiker Gebruiker { get; set; }
        public virtual Klant Klant { get; set; }
    }
}