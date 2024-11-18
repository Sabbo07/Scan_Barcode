namespace Scan_Barcode.Entities.DTO;

public class OrdineDettaglioDTO
{
    public int Id { get; set; }
    public int? QuantitaRichiesta { get; set; }
    public string NomeMateriale { get; set; }
    public string NumeroOrdine { get; set; }
    public DateTime DataInserimento { get; set; }
}