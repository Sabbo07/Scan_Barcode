namespace Scan_Barcode.Accessi;
using Scan_Barcode.Data;
public class Log
{
    private readonly DatabaseService _databaseService;
    public Log(DatabaseService databaseService)
    {
        _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
    }
    // questo è salva l'accesso eseguito fatto dal magazziniere
    public void accessoeseguito(string username)
    {
        string query = @"
            INSERT INTO Log (Id, Tipo, Data, Utente)
            VALUES (NEWID(), 'Accesso eseguito in App!', GETDATE(), @username)";
            
        
        var parameters2 = new Dictionary<string, object>
        {
            { "@username", username }
        };
        
        _databaseService.ExecuteQuery(query, parameters2);
    }
    // questo invece scrive in db che l'ordine è stato eseguito
    public void ordineeseguito(string username, int ordine)
    {
        string query = @"
        INSERT INTO Log (Id, Tipo, Data, Utente)
        VALUES (NEWID(), 'Ordine eseguito con successo ID = ' + CAST(@ordine AS NVARCHAR), GETDATE(), @username)";
        
        var parameters = new Dictionary<string, object>
        {
            { "@username", username },
            { "@ordine", ordine }
        };
        
        _databaseService.ExecuteNonQuery(query, parameters);
    }
    //Qui salviamo l'accesso non andato a buon fine, in qualunque caso, pure un accesso di
    //una persona che non avrebbe a che fare con l'app 
    public void accessorifiutato(string username)
    {
        string query = @"
            INSERT INTO Log (Id, Tipo, Data, Utente)
            VALUES (NEWID(), 'Accesso errato!', GETDATE(), @username)";
        
        var parameters2 = new Dictionary<string, object>
        {
            { "@username", username }
        };
        
        _databaseService.ExecuteQuery(query, parameters2);
    }
}