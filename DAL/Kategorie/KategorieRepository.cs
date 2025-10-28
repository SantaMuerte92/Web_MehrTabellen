using Dapper;
using Microsoft.Data.SqlClient;
using Web_MehrTabellen.Model;

namespace Web_MehrTabellen.DAL
{
    public class KategorieRepository : IKategorieRepository
    {

        private readonly string connString;

        public KategorieRepository(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool DeleteKategorie(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                DELETE FROM [dbo].[Kategorie]
                                WHERE KID = @KID;"; // Bedingung zum Löschen der Person

                var parameters = new
                {
                    KID = id // Der Parameter für die PID
                };

                connection.Open(); // Bei Dapper nicht zwingend notwendig, wird aber oft gemacht
                var rowsAffected = connection.Execute(sqlQuery, parameters); // Anzahl der betroffenen Zeilen zurückgeben

                return rowsAffected > 0; // Gibt true zurück, wenn mindestens eine Zeile gelöscht wurde
            }
        }

        public List<Kategorien> GetAllKategorien()
        {
            using (var connection = new SqlConnection(connString))
            {
                return connection.Query<Kategorien>("Select * from Kategorie").ToList();
            }
        }

        public Kategorien GetKategorieById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                SELECT * 
                                FROM [dbo].[Kategorie]
                                WHERE KID = @KID;"; // Bedingung zum Abrufen der Person

                var parameters = new
                {
                    KID = id // Der Parameter für die PID
                };

                connection.Open();
                var katego = connection.QuerySingleOrDefault<Kategorien>(sqlQuery, parameters);
                return katego; // Gibt null zurück, wenn keine Person gefunden wurde
            }
        }

        public Kategorien InsertKategorie(Kategorien kategorie)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                INSERT INTO [dbo].[Kategiorie] (Bezeichnung)
                OUTPUT INSERTED.*
                VALUES (@Bezeichnung);";

                var parameters = new
                {
                    Bezeichnung = kategorie.Bezeichnung,
                };
                connection.Open();
                var newKategorie = connection.QuerySingle<Kategorien>(sqlQuery, parameters);
                return newKategorie;
            }
        }

        public Kategorien UpdateKategorie(Kategorien kategorie)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sqlQuery = @"
                                UPDATE [dbo].[Kategorie]
                                SET 
                                    Bezeichnung = @Bezeichnung,
                                 
                                                                   OUTPUT INSERTED.*
                                WHERE KID = @KID;"; // Bedingung zum Aktualisieren

                var parameters = new
                {
                    KID = kategorie.KID, // Stellen Sie sicher, dass PID nicht null ist
                    Bezeichnung = kategorie.Bezeichnung,

                };

                connection.Open();
                var updatedKategorie = connection.QuerySingle<Kategorien>(sqlQuery, parameters);
                return updatedKategorie;
            }
        }
    }
}
