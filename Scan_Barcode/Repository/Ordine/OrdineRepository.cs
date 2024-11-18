using System.Collections.Generic;
using Scan_Barcode.Data;

namespace Scan_Barcode.Repository
{
    public class OrdineRepository : IOrdineRepository
    {
        private readonly DatabaseService _databaseService;

        public OrdineRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Metodo per ottenere gli ordini di un magazzino specifico
        public List<Dictionary<string, object>> GetOrdiniPerMagazzino(int idMagazzino)
        {
            string query = @"
                SELECT *
                FROM MaterialeOrdineDettaglio mod
                JOIN MaterialeOrdine mo ON mod.IdOrdine = mo.Id
                JOIN Materiale m ON mod.IdMateriale = m.Id
                WHERE m.IdMagazzino = @IdMagazzino";
            
            var parameters = new Dictionary<string, object>
            {
                { "@IdMagazzino", idMagazzino }
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }

        // Metodo per inserire un nuovo ordine
        public int InserisciOrdine(int idOrdine, int idMateriale, int qtaRichiesta)
        {
            string query = @"
                INSERT INTO MaterialeOrdineDettaglio (IdOrdine, IdMateriale, QtaRichiesta, DataInserimento)
                VALUES (@IdOrdine, @IdMateriale, @QtaRichiesta, GETDATE())";
            
            var parameters = new Dictionary<string, object>
            {
                { "@IdOrdine", idOrdine },
                { "@IdMateriale", idMateriale },
                { "@QtaRichiesta", qtaRichiesta }
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }
        public List<Dictionary<string, object>> GetOrdiniPerMagazzino2(int idMagazzino)
        {
            string query = @"
    select m.nome AS Nome_Materiale ,
			u.Nome AS Cliente_Richiedente,
			mdo.qtaRichiesta AS Quantità_Richiesta ,
			mo.nOrdine AS Codice_Ordine,
        CAST(mp.Scaffale AS VARCHAR(1)) + '-' + 
        CAST(mp.Colonna AS VARCHAR(2)) + '-' + 
        CAST(mp.Ripiano AS VARCHAR(1)) AS Posizione
  from MaterialeOrdineDettaglio mdo
  join MaterialeOrdine mo ON mo.id = mdo.idOrdine
  join Utenti u ON u.IdUtente =mo.idUtente
  join Materiale m ON m.id = mdo.idMateriale
  jOIN MaterialePosizione mp ON mp.IdMateriale = m.Id
  where m.idMagazzino=@IdMagazzino";
            
            var parameters = new Dictionary<string, object>
            {
                { "@IdMagazzino", idMagazzino }
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }
    }
    
}