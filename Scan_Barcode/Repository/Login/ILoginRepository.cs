namespace Scan_Barcode.Repository.Login;

public interface ILoginRepository
{
    Task<(int? UserId, string Status)> GetUserIdAsync(string username, string password);
}