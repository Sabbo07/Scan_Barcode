using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class MaterialePosizioneScarico
{
    public int Id { get; set; }

    public int? IdMaterialePosizione { get; set; }

    public int? Qta { get; set; }

    public DateTime? DataInserimento { get; set; }

    public int? IdUtente { get; set; }

    public string? Note { get; set; }
}
