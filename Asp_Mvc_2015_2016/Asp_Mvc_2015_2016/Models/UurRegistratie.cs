using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Uurregistraties")]
    public class UurRegistratie : _BaseInfo
    {
        public int Id { get; set; }
        public FactuurDetails factuurDetails { get; set; }
        //detailgegevens
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public TypeWerk TypeWerk { get; set; }
        public bool TeFactureren { get; set; }
    }
}