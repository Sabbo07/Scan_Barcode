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
        
        public async Task<bool> UpdateQuantityByBarcodeAsync(string barcode, int newQuantity, int idOrdine,string username)
        {
            string query = @"UPDATE Materiale
                     SET Giacenza = @NewQuantity
                     WHERE Barcode = @Barcode AND IdOrdine = @IdOrdine";

            var parameters = new Dictionary<string, object>
            {
                { "@Barcode", barcode },
                { "@NewQuantity", newQuantity },
                { "@IdOrdine", idOrdine }
            };
            string query2 =@"UPDATE MaterialeOrdine
                                SET stato = 3
                                Where id = @IdOrdine";
            var parameters2 = new Dictionary<string, object>
            {
                { "@IdOrdine", idOrdine }
            };
            var log = new Log(_databaseService);
            log.ordineeseguito(username,idOrdine);
            var rowsAffected =  _databaseService.ExecuteNonQuery(query2, parameters2);
            return rowsAffected > 0; // Restituisce true se almeno una riga è stata aggiornata
        }

    }
}