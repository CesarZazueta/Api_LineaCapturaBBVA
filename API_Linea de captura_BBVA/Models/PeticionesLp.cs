using System;
using System.Collections.Generic;

namespace API_Linea_de_captura_BBVA.Models;

public partial class PeticionesLp
{
    public int IdLp { get; set; }

    public string? IpCliente { get; set; }

    public string? Navegador { get; set; }

    public DateTime? FechaPeticion { get; set; }

    public int? Establecimiento { get; set; }

    public decimal? TipoPago { get; set; }

    public double? Importe { get; set; }

    public int? Dia { get; set; }

    public int? Mes { get; set; }

    public int? Anio { get; set; }

    public string? ReferenciaPrincipal { get; set; }

    public string? ReferenciaAdicional { get; set; }

    public int? NumeroAleatorio { get; set; }

    public string? Firma { get; set; }

    public string? LineaCaptura { get; set; }
}
