namespace Scan_Barcode.Entities.DTO;

public class MaterialeOrdineDto
{
    public string nOrdine { get; set; }
    public string Destinazione { get; set; }
    public string Indirizzo { get; set; }
    public string Localita { get; set; }
    public string Provincia { get; set; }
    public string Regione { get; set; }
    public string Cap { get; set; }
    public int Qta { get; set; }
    public int QtaRichiesta { get; set; }
    public string Barcode { get; set; }
}