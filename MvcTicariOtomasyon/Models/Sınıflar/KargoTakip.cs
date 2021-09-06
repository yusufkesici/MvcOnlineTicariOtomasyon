using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Sınıflar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TakipKodu { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Aciklama { get; set; }
        public DateTime TarihZaman { get; set; }

    }
}