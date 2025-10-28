using Web_MehrTabellen.Model;

namespace Web_MehrTabellen.DAL
{
    public interface IKategorieRepository
 
    {


        List<Kategorien> GetAllKategorien();

        Kategorien GetKategorieById(int id);

        Kategorien InsertKategorie(Kategorien kategorie);

        Kategorien UpdateKategorie(Kategorien kategorie);

        bool DeleteKategorie(int id);



    }
}
