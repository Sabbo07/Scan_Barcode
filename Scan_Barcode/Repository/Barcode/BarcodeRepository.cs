using System.Collections.Generic;
using System.Threading.Tasks;
using Scan_Barcode.Accessi;
using Scan_Barcode.Data;

namespace Scan_Barcode.Repository.Barcode
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly DatabaseService _databaseService;

        public BarcodeRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Dictionary<string, object>> FindByBarcodeAsync(string barcode)
        {
            string query = @"SELECT 
                                Id, Nome, Giacenza
                             FROM Materiale
                             WHERE Barcode = @Barcode";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode }
            };
            
            var result =  _databaseService.ExecuteQuery(query, parameters);

            // Restituisce il primo risultato (o null se non trovato)
            return result.Count > 0 ? result[0] : null;
        }
        
        public async Task<bool> UpdateQuantityByBarcodeAsyncScarico(string barcode, int quantityChange, int idOrdine, string username)
        {
            //Qui è sbagliata la query, o meglio la chiamata
            string query = @"
                    UPDATE m
                    SET m.Giacenza = m.Giacenza - @QuantityChange
                    FROM Materiale m
                    INNER JOIN MaterialeOrdineDettaglio mod
                    ON mod.idMateriale = m.id
                    INNER JOIN MaterialeOrdine mo 
                    ON mo.Id = mod.IdOrdine
                    WHERE m.barcode = @Barcode AND mod.idOrdine = @IdOrdine
                   AND mo.stato = 2 AND
                    mo.idMandatoSpedizione IS NOT NULL
                    AND m.Giacenza + @QuantityChange >= 0";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode },
                { "@QuantityChange", quantityChange }, // Può essere positivo o negativo
                { "@IdOrdine", idOrdine }
            };

            string query2 = @"
                UPDATE MaterialeOrdine
                SET stato = 3
                WHERE id = @IdOrdine";

            var parameters2 = new Dictionary<string, object>
            {
                { "@IdOrdine", idOrdine }
            };

            var log = new Log(_databaseService);
            log.ordineeseguito(username, idOrdine);

            var rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            _databaseService.ExecuteNonQuery(query2, parameters2);

            return rowsAffected > 0; // Restituisce true se almeno una riga è stata aggiornata
        }
        public async Task<bool> UpdateQuantityByBarcodeAsyncCarico(string barcode, int quantityChange, int idOrdine, string username)
        {
            string query = @"
    UPDATE m
    SET m.Giacenza = m.Giacenza + @QuantityChange
    FROM Materiale m
    INNER JOIN MaterialeOrdineDettaglio mod
        ON mod.idMateriale = m.id
    INNER JOIN MaterialeOrdine mo 
        ON mo.Id = mod.IdOrdine
    WHERE m.barcode = @Barcode AND mod.idOrdine = @IdOrdine
    AND m.Giacenza + @QuantityChange >= 0 AND
    mo.stato = 3 AND
                    mod.rientrato IS NULL";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode },
                { "@QuantityChange", quantityChange }, // Può essere positivo o negativo
                { "@IdOrdine", idOrdine }
            };
            

            var log = new Log(_databaseService);
            log.ordineeseguito(username, idOrdine);

            var rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);

            return rowsAffected > 0; // Restituisce true se almeno una riga è stata aggiornata
        }
        public async Task<int> GetCurrentQuantityByBarcodeAsync(string barcode, int idOrdine)
        {
            string query = @"
        SELECT m.Giacenza
        FROM Materiale m
        INNER JOIN MaterialeOrdineDettaglio mod
            ON mod.idMateriale = m.id
        WHERE m.barcode = @Barcode AND mod.idOrdine = @IdOrdine";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode },
                { "@IdOrdine", idOrdine }
            };

            var result = await _databaseService.ExecuteScalarAsync(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

    }
}