using Dapper;
using Microsoft.Data.SqlClient;
using Web_MehrTabellen.Model;


namespace Web_MehrTabellen.DAL.Artikel
{
    public class ArtikelRepository : IArtikelRepository
    {
                private readonly string connString;

        public ArtikelRepository(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool DeleteArtikel(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                DELETE FROM [dbo].[Artikel]
                                WHERE AID = @AID;"; // Bedingung zum Löschen der Person

                var parameters = new
                {
                    AID = id // Der Parameter für die PID
                };

                connection.Open(); // Bei Dapper nicht zwingend notwendig, wird aber oft gemacht
                var rowsAffected = connection.Execute(sqlQuery, parameters); // Anzahl der betroffenen Zeilen zurückgeben

                return rowsAffected > 0; // Gibt true zurück, wenn mindestens eine Zeile gelöscht wurde
            }
        }

        public List<Artikels> GetAllArtikel()
        {
            using (var connection = new SqlConnection(connString))
            {
                return connection.Query<Artikels>("Select * from Artikel").ToList();
            }
        }

        public Artikels GetArtikelById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                SELECT * 
                                FROM [dbo].[Artikel]
                                WHERE AID = @AID;"; // Bedingung zum Abrufen der Person

                var parameters = new
                {
                    AID = id // Der Parameter für die PID
                };

                connection.Open();
                var person = connection.QuerySingleOrDefault<Artikels>(sqlQuery, parameters);
                return person; // Gibt null zurück, wenn keine Person gefunden wurde
            }
        }

        public Artikels InsertArtikel(Artikels artikel)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                INSERT INTO [dbo].[Artikel] (Bezeichnung, Preis, KID)
                OUTPUT INSERTED.*
                VALUES (@Bezeichnung, @Preis, @KID);";

                var parameters = new
                {
                    Bezeichnung = artikel.Bezeichnung,
                    Preis = artikel.Preis,
                    KID = artikel.KID,
                                   };
                connection.Open();
                var newArtikel = connection.QuerySingle<Artikels>(sqlQuery, parameters);
                return newArtikel;
            }
        }

        public Artikels UpdateArtikel(Artikels artikel)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                UPDATE [dbo].[Artikel]
                                SET 
                                    Bezeichnung = @Bezeichnung,
                                    Preis = @Preis,
                                    KID = @KID,
                                                                   OUTPUT INSERTED.*
                                WHERE AID = @AID;"; // Bedingung zum Aktualisieren

                var parameters = new
                {
                    AID = artikel.AID, // Stellen Sie sicher, dass PID nicht null ist
                    Bezeichnung = artikel.Bezeichnung,
                    Preis = artikel.Preis,
                    KID = artikel.KID,
                };

                connection.Open();
                var updatedArtikel = connection.QuerySingle<Artikels>(sqlQuery, parameters);
                return updatedArtikel;
            }
        }

    }
}
