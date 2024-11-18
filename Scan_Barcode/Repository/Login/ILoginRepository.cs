namespace Scan_Barcode.Repository.Login;

public interface ILoginRepository
{
    Task<int?> GetUserIdAsync(string username, string password);
}