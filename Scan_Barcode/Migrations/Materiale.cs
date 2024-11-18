using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class Materiale
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMagazzino { get; set; }
    public Magazzino? Magazzino { get; set; } // Proprietà di navigazione

    public string? Identificativo { get; set; }

    public string? Nome { get; set; }

    public int? Giacenza { get; set; }

    public DateTime? DataInserimento { get; set; }

    public DateTime? DataEliminazione { get; set; }

    public string? Note { get; set; }

    public string? Foto { get; set; }

    public bool? Ritiro { get; set; }

    public string? Barcode { get; set; }
    
    public ICollection<MaterialeOrdineDettaglio> MaterialeOrdineDettagli { get; set; } // Navigazione verso i dettagli dell'ordine

}
