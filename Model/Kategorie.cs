using System.ComponentModel.DataAnnotations;


namespace Web_MehrTabellen.Model
{
    public class Kategorien
    {
        public int KID { get; set; }

        [Required(ErrorMessage = "Bezeichnung ist erforderlich")]
        [StringLength(100, ErrorMessage = "Bezeichnung darf maximal {1} Zeichen haben.")]
        public string Bezeichnung { get; set; } = string.Empty;
    }
}

