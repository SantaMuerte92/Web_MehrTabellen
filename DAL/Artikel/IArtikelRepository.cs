using Web_MehrTabellen.Model;


namespace Web_MehrTabellen.DAL.Artikel


{
    public interface IArtikelRepository
    {
      

            List<Artikels> GetAllArtikel();

            Artikels GetArtikelById(int id);

            Artikels InsertArtikel(Artikels artikel);

            Artikels UpdateArtikel(Artikels artikel);

            bool DeleteArtikel(int id);



        }
    }

