using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class MaterialeOrdine
{
    public int Id { get; set; }

    public string? NOrdine { get; set; }

    public int? IdAdesione { get; set; }

    public DateOnly? DataConsegnaRichiesta { get; set; }

    public int? IdPuntoVendita { get; set; }

    public int? IdCollaboratore { get; set; }

    public string? Destinazione { get; set; }

    public string? Cellulare { get; set; }

    public string? Email { get; set; }

    public string? Indirizzo { get; set; }

    public string? Localita { get; set; }

    public string? Provincia { get; set; }

    public string? Regione { get; set; }

    public string? Cap { get; set; }

    public string? Note { get; set; }

    public DateTime? DataInserimento { get; set; }

    public int? IdUtente { get; set; }

    public int? Stato { get; set; }

    public int? IdMandatoSpedizione { get; set; }

    public DateTime? DataAggStato { get; set; }
    
    public ICollection<MaterialeOrdineDettaglio> DettagliOrdine { get; set; } // Navigazione verso i dettagli dell'ordine

}
