using Scan_Barcode.Migrations;

namespace Scan_Barcode.Repository;

public interface IOrdineRepository
{
    List<Dictionary<string, object>> GetOrdiniPerMagazzino(int idMagazzino);
    int InserisciOrdine(int idOrdine, int idMateriale, int qtaRichiesta);
    List<Dictionary<string, object>> GetOrdiniPerMagazzino2(int idMagazzino);
}