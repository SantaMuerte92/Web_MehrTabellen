using System.ComponentModel.DataAnnotations;

namespace Web_MehrTabellen.Model
{
    public class Artikels
    {
        public int AID { get; set; }

        [Required(ErrorMessage = "Bezeichnung ist erforderlich")]
        [StringLength(200, ErrorMessage = "Bezeichnung darf maximal {1} Zeichen haben.")]
        public string Bezeichnung { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "Preis muss größer als 0 sein")]
        public decimal Preis { get; set; }

        [Required(ErrorMessage = "Kategorie ist erforderlich")]
        public int KID { get; set; }
    }
}
