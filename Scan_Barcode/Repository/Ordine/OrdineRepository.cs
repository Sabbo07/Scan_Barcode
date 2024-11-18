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

        
        public List<Dictionary<string, object>> GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId)
        {
            if ((userId <= 0) || (userId == null))
            {
                throw new NullReferenceException();
            }
            
            string query = @"
    select mo.id AS IDOrdine,
           m.nome AS Nome_Materiale ,
			u.Nome AS Cliente_Richiedente,
			mdo.qtaRichiesta AS Quantità_Richiesta ,
			mo.nOrdine AS Riferimento_Ordine,
        CAST(mp.Scaffale AS VARCHAR(1)) + '-' + 
        CAST(mp.Colonna AS VARCHAR(2)) + '-' + 
        CAST(mp.Ripiano AS VARCHAR(1)) AS Posizione
  from MaterialeOrdineDettaglio mdo
  join MaterialeOrdine mo ON mo.id = mdo.idOrdine
  join Utenti u ON u.IdUtente =mo.idUtente
  join Materiale m ON m.id = mdo.idMateriale
  jOIN MaterialePosizione mp ON mp.IdMateriale = mdo.IdMaterialePosizione
  where m.idMagazzino=@IdMagazzino AND mo.stato = 2 AND mo.idMandatoSpedizione IS NULL";
            
            var parameters = new Dictionary<string, object>
            {
                { "@IdMagazzino", idMagazzino }
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }
    }
    
}