using Scan_Barcode.Entities.DTO;

namespace Scan_Barcode.Service.Ordine;

public interface IOrdineService
{
    Task<IEnumerable<MaterialeOrdineDto>> GetMaterialeOrdineAsync(int idOrdine, string barcode);
}