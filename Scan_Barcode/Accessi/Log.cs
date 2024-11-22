namespace Scan_Barcode.Accessi;
using Scan_Barcode.Data;
public class Log
{
    private readonly DatabaseService _databaseService;
    public Log(DatabaseService databaseService)
    {
        _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
    }
    public void accessoeseguito(string username)
    {
        string query = @"
            INSERT INTO Log (Id, Tipo, Data, Utente)
            VALUES (NEWID(), 'Accesso eseguito in App!', GETDATE(), @username)";
            
        // Non ci sono parametri per questa query
        var parameters2 = new Dictionary<string, object>
        {
            { "@username", username }
        };

        // Esegui la query
        _databaseService.ExecuteQuery(query, parameters2);
    }
    public void ordineeseguito(string username, int ordine)
    {
        string query = @"
            INSERT INTO Log (Id, Tipo, Data, Utente)
            VALUES (NEWID(), 'Ordine eseguito con successo ID @ordine', GETDATE(), @username)";
            
        // Non ci sono parametri per questa query
        var parameters2 = new Dictionary<string, object>
        {
            { "@username", username },
            { "@ordine" , ordine }
        };

        // Esegui la query
        _databaseService.ExecuteQuery(query, parameters2);
    }
    
}