using System;
using System.Collections.Generic;

namespace Scan_Barcode.Migrations;

public partial class Utenti
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? UserId { get; set; }

    public string? Password { get; set; }

    public int? Ruolo { get; set; }

    public int? IdUtente { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMercato { get; set; }

    public int? IdCanale { get; set; }

    public int? IdRegione { get; set; }

    public int? IdAgenzia { get; set; }

    public int? IdEvento { get; set; }

    public int? IdFiliale { get; set; }

    public bool? Ispettore { get; set; }

    public string? IdEmail { get; set; }

    public bool? App { get; set; }

    public string? Promemoria { get; set; }

    public bool? Privacy { get; set; }

    public Guid? Guid { get; set; }

    public int? IdElearning { get; set; }

    public DateTime? UltimoAccesso { get; set; }

    public DateTime? UpdatePassword { get; set; }
}
