using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class MaterialePosizione
{
    public int Id { get; set; }

    public int? IdMateriale { get; set; }

    public string? Scaffale { get; set; }

    public int? Colonna { get; set; }

    public string? Ripiano { get; set; }

    public DateTime? DataInserimento { get; set; }

    public DateTime? DataEliminazione { get; set; }
}
