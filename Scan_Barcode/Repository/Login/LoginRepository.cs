using Scan_Barcode.Data;

namespace Scan_Barcode.Repository.Login;

public class LoginRepository
{
    private readonly DatabaseService _databaseService;

    public LoginRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    



}