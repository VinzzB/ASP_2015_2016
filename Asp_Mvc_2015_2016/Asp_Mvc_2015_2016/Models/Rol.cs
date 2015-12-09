using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc_2015_2016.Models
{
    [Table("Rollen")]
    public class Rol
    {
        public int Id { get; set; }
        public string RolBenaming { get; set; }
    }
}