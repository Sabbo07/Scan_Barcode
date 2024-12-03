namespace Scan_Barcode.Repository.Barcode;

public interface IBarcodeRepository
{
    Task<Dictionary<string, object>> FindByBarcodeAsync(string barcode);
    Task<int> GetCurrentQuantityByBarcodeAsync(string barcode, int idOrdine);
    Task<bool> UpdateQuantityByBarcodeAsyncScarico(string barcode, int quantityChange, int idOrdine, string username);
    Task<bool> UpdateQuantityByBarcodeAsyncCarico(string barcode, int quantityChange, int idOrdine, string username);
}