using System.Collections.Generic;
using Scan_Barcode.Data;
using Scan_Barcode.Entities.DTO;

namespace Scan_Barcode.Repository
{
    public class OrdineRepository : IOrdineRepository
    {
        private readonly DatabaseService _databaseService;

        public OrdineRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        public List<Dictionary<string, object>> GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId)
        {
            //Controllo dei parametri
            if (userId <= 0)
            {
                throw new ArgumentException("L'ID utente non è valido.");
            }
            // Controlla il ruolo dell'utent
            
            string query = @"SELECT 
                mo.id AS IDOrdine,
                m.nome AS Nome_Materiale,
                u.nome AS Cliente_Richiedente,
                mdo.qtaRichiesta AS Quantita_Richiesta,
                m.Giacenza as QuantitàTotale,
                mo.nOrdine AS Riferimento_Ordine,
                CAST(mp.scaffale AS VARCHAR(1)) + '-' + 
                CAST(mp.colonna AS VARCHAR(2)) + '-' + 
                CAST(mp.ripiano AS VARCHAR(1)) AS Posizione,
                m.Barcode AS Barcode

                FROM 
                MaterialeOrdineDettaglio mdo
                JOIN 
                MaterialeOrdine mo ON mo.id = mdo.idOrdine
                JOIN 
                Utenti u ON u.IdUtente = mo.idUtente
                JOIN 
                Materiale m ON m.id = mdo.idMateriale
                LEFT JOIN 
                MaterialePosizione mp ON mp.id = mdo.idMaterialePosizione
                WHERE 
                m.idMagazzino = @idMagazzino AND
                mo.stato = 2 AND
                mo.idMandatoSpedizione IS NOT NULL";
            
            var parameters = new Dictionary<string, object>
            {
                { "@IdMagazzino", idMagazzino }
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }


        public List<Dictionary<string, object>> GetOrdiniPerMagazzinoCarico(int idMagazzino, int userId)
        {
            if ((userId <= 0) || (userId == null))
            {
                throw new NullReferenceException();
            }

            string query = @"SELECT 
                mo.id AS IDOrdine,
                m.nome AS Nome_Materiale,
                u.nome AS Cliente_Richiedente,
                mdo.qtaRichiesta AS Quantita_DiRientro,
                mo.nOrdine AS Riferimento_Ordine,
                m.Barcode AS Barcode,
                CAST(mp.scaffale AS VARCHAR(1)) + '-' + 
                CAST(mp.colonna AS VARCHAR(2)) + '-' + 
                CAST(mp.ripiano AS VARCHAR(1)) AS Posizione_di_Rientro
                FROM 
                MaterialeOrdineDettaglio mdo
                JOIN 
                MaterialeOrdine mo ON mo.id = mdo.idOrdine
                JOIN 
                Utenti u ON u.IdUtente = mo.idUtente
                JOIN 
                Materiale m ON m.id = mdo.idMateriale
                LEFT JOIN 
                MaterialePosizione mp ON mp.id = mdo.idMaterialePosizioneRientro
                WHERE 
                m.idMagazzino = @idMagazzino AND
                mo.stato = 3
                AND mdo.rientrato IS NULL
                AND mdo.statoMatRientro IS NULL";

            var parameters = new Dictionary<string, object>
            {
                { "@IdMagazzino", idMagazzino }
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }
        
        
        
        public async Task<IEnumerable<MaterialeOrdineDto>> GetMaterialeOrdineAsync(int idOrdine, string barcode)
        {
            var query = @"
        SELECT nOrdine, destinazione, indirizzo, localita, provincia, regione, cap, qta, qtaRichiesta, Barcode
        FROM MaterialeOrdine mo
        JOIN MaterialeOrdineDettaglio mod ON mod.idOrdine = mo.id
        JOIN Materiale m ON m.id = mod.idMateriale
        WHERE mo.id = @idOrdine AND Barcode = @Barcode";

            var parameters = new Dictionary<string, object>
            {
                { "@idOrdine", idOrdine },
                { "@Barcode", barcode }
            };

            var result = await _databaseService.QueryAsync<MaterialeOrdineDto>(query, parameters);
            return result;
        }
    }
    
    
}