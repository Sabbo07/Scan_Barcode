namespace Scan_Barcode.Repository.Magazzino;

public interface IMagazzinoRepository
{
    List<Dictionary<string, object>> GetMagazzini();
}