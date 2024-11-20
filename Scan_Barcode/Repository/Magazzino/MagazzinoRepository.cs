
using Scan_Barcode.Data;
using Scan_Barcode.Repository.Magazzino;

public class MagazzinoRepository : IMagazzinoRepository
{
    private readonly DatabaseService _databaseService;

    public MagazzinoRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    // Metodo per ottenere tutti i magazzini
    public List<Dictionary<string, object>> GetMagazzini()
    {
        // Query SQL
        string query = @"SELECT 
                Id AS IDMagazzino,
                Nome AS Nome_Magazzino
            FROM 
                Magazzino";

        // Non ci sono parametri per questa query
        var parameters = new Dictionary<string, object>();

        // Esegui la query e ritorna i risultati
        return _databaseService.ExecuteQuery(query, parameters);
    }
}