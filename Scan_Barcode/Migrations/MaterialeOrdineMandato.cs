using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class MaterialeOrdineMandato
{
    public int Id { get; set; }

    public string? Identificativo { get; set; }

    public DateTime? DataInserimento { get; set; }

    public int? IdUtente { get; set; }
}
