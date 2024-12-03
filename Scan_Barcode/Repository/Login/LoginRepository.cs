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

    public async Task<(int? UserId, string Status)> GetUserIdAsync(string username, string password)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@username", username },
            { "@password", password }
        };

        // Verifica se l'utente esiste
        string userQuery = @"
        SELECT COUNT(*)
        FROM Utenti
        WHERE UserId = @username";
        var userExists = await _databaseService.ExecuteScalarAsync(userQuery, parameters);

        if (userExists != null && Convert.ToInt32(userExists) > 0)
        {
            // Verifica se username e password corrispondono
            string authQuery = @"
            SELECT IdUtente
            FROM Utenti 
            WHERE UserId = @username AND Password = @password";
            var result = await _databaseService.ExecuteScalarAsync(authQuery, parameters);

            if (result != null && int.TryParse(result.ToString(), out int userId))
            {
                // Verifica il ruolo
                string roleQuery = @"
                SELECT COUNT(*)
                FROM Utenti
                WHERE UserId = @username AND Password = @password AND Ruolo = '6139'";
                var roleCheck = await _databaseService.ExecuteScalarAsync(roleQuery, parameters);

                if (roleCheck != null && Convert.ToInt32(roleCheck) > 0)
                {
                    var log = new Log(_databaseService);
                    log.accessoeseguito(username);
                    return (userId, "Success");
                }

                return (null, "UnauthorizedRole");
            }

            return (null, "InvalidUser");
        }

        return (null, "UserNotFound");
    }

    
}

