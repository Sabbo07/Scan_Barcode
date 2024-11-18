using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class Log
{
    public Guid Id { get; set; }

    public string? Tipo { get; set; }

    public DateTime? Data { get; set; }

    public string? Utente { get; set; }

    public string? Info { get; set; }

    public decimal? Longitudine { get; set; }

    public decimal? Latitudine { get; set; }

    public string? Json { get; set; }
}
