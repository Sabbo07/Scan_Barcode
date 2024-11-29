namespace Scan_Barcode.Service;

public interface IBarcodeService
{
    Task<Dictionary<string, object>> GetProductByBarcodeAsync(string barcode);
    Task<bool> UpdateProductQuantityAsyncScarico(string barcode, int newQuantity, int idOrdine,string username);
    Task<string> GetUsernameByUserIdAsync(int userId);
}