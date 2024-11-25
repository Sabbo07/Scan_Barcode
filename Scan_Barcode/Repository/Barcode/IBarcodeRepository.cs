namespace Scan_Barcode.Repository.Barcode;

public interface IBarcodeRepository
{
    Task<Dictionary<string, object>> FindByBarcodeAsync(string barcode);
    Task<bool> UpdateQuantityByBarcodeAsync(string barcode, int newQuantity, int idOrdine,string username);
    Task<int> GetCurrentQuantityByBarcodeAsync(string barcode, int idOrdine);
}