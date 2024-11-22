using Scan_Barcode.Data;
using Scan_Barcode.Accessi;
namespace Scan_Barcode.Repository.Login;

public class LoginRepository : ILoginRepository
{
    private readonly DatabaseService _databaseService;

    public LoginRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<int?> GetUserIdAsync(string username, string password)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@username", username },
            { "@password", password }
        };
        
        string query = @"
                SELECT Id 
                FROM Utenti 
                WHERE UserId = @username AND Password = @password";

        // Chiamata al metodo appropriato in DatabaseService
        var result = await _databaseService.ExecuteScalarAsync(query, parameters);

        if (result != null && int.TryParse(result.ToString(), out int userId))
        {
            var log = new Log(_databaseService);
            log.accessoeseguito(username);

            return userId;
            
        }
        
        return null; // Nessun utente trovato
    }
    
}

