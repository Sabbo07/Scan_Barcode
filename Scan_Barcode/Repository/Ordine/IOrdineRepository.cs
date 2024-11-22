using Scan_Barcode.Entities.DTO;
using Scan_Barcode.Migrations;

namespace Scan_Barcode.Repository;

public interface IOrdineRepository
{
    List<Dictionary<string, object>> GetOrdiniPerMagazzinoScarico(int idMagazzino, int userId);
    List<Dictionary<string, object>> GetOrdiniPerMagazzinoCarico(int idMagazzino, int userId);
    Task<IEnumerable<MaterialeOrdineDto>> GetMaterialeOrdineAsync(int idOrdine, string barcode);
}