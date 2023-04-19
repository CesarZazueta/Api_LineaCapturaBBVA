namespace API_Linea_de_captura_BBVA.Controllers
{
    public class VariablesRequest
    {
        public string Establecimiento { get; set; }
        public string Tipo_Pago { get; set; }
        public string Importe { get; set; }

        public string Dia { get; set; }
        public string Mes { get; set; }

        public string Anio { get; set; }

        public string Referencia_Principal { get; set; }

        public string Referencia_Adicional { get; set; }
    }
}
