using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class Magazzino
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public ICollection<Materiale> Materiali { get; set; }
}
