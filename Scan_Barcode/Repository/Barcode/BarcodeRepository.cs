using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public async Task<bool> UpdateQuantityByBarcodeAsync(string barcode, int newQuantity)
        {
            string query = @"UPDATE Materiale
                     SET Giacenza = @NewQuantity
                     WHERE Barcode = @Barcode";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode },
                { "@NewQuantity", newQuantity }
            };

            var rowsAffected =  _databaseService.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0; // Restituisce true se almeno una riga è stata aggiornata
        }

    }
}