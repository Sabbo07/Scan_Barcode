namespace Scan_Barcode.Service.Magazzino;

public interface IMagazzinoService

{
    List<Dictionary<string, object>> GetAllMagazzini();
}