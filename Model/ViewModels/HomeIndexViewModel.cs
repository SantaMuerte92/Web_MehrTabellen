using System.Collections.Generic;
using Web_MehrTabellen.Model;

namespace Web_MehrTabellen.Model.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Artikels> Artikel { get; set; } = new();
        public List<Kategorien> Kategorien { get; set; } = new();
        public int SelectedKID { get; set; }
    }
}
