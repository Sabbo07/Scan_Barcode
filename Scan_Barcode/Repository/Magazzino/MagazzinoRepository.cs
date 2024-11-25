
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
        var parameters = new Dictionary<string, object>();
        return _databaseService.ExecuteQuery(query, parameters);
    }
}