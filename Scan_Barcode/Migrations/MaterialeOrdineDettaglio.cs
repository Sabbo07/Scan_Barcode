using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class MaterialeOrdineDettaglio
{
    public int Id { get; set; }

    public int? IdOrdine { get; set; }

    public int? IdMateriale { get; set; }

    public int? IdKit { get; set; }

    public int? Qta { get; set; }

    public DateTime? DataInserimento { get; set; }

    public int? QtaRichiesta { get; set; }

    public int? IdMaterialePosizione { get; set; }

    public bool? Rientrato { get; set; }

    public int? StatoMatRientro { get; set; }

    public int? IdMaterialePosizioneRientro { get; set; }

    public DateTime? DataRientro { get; set; }

    public int? IdUtenteRientro { get; set; }
    
    public MaterialeOrdine Ordine { get; set; } // Navigazione verso l'ordine
    public Materiale Materiale { get; set; } // Navigazione verso il materiale
}
