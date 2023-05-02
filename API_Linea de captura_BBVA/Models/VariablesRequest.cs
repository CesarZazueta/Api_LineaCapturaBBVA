using System;
using System.Web;

namespace API_Linea_de_captura_BBVA.Models
{
    public class VariablesRequest
    {
        internal readonly object ipHost;

        public string Establecimiento { get; set; }
        public string Tipo_Pago { get; set; }
        public string Importe { get; set; }

        public string Dia { get; set; }
        public string Mes { get; set; }

        public string Anio { get; set; }

        public string Referencia_Principal { get; set; }

        public string Referencia_Adicional { get; set; }

        public string Numero_Aleatorio { get; set; }

        public string Firma { get; set; }
    }
}
