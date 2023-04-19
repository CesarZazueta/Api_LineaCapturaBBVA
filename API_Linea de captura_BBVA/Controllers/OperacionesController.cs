using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json.Serialization;

namespace API_Linea_de_captura_BBVA.Controllers
{
    
    [ApiController]
    public class OperacionesController : ControllerBase
    {
        [HttpPost]
        [Route("api/Linea_Captura_BBVA")]
        public string GetLineaCaptura([FromBody]VariablesRequest request)
        
        {

            //Variables strin a cambiarla a entero
            string DC = "2";
            int imp = Int32.Parse(request.Importe); //Importe de string a entero
            int dia_I = Int32.Parse(request.Dia);
            int mes_I = Int32.Parse(request.Mes);
            int anio_I = Int32.Parse(request.Anio);



            //Obtener el digito Verificador
            int suma_Digito_Verificador = 0;
            int selector = 7;
            int int_Digito_Verificador;
            string Digito_Verificador;

            while (imp > 0)
            {

                suma_Digito_Verificador += ((imp % 10) * selector);

                switch (selector)
                {
                    case 7: selector = 3;
                        break;
                    case 3:
                        selector = 1;
                        break;
                    case 1:
                        selector = 7;
                        break;
                }


                imp  = imp/10;
            }

            int_Digito_Verificador = suma_Digito_Verificador%10;
            Digito_Verificador = int_Digito_Verificador.ToString();
            //string X = suma_Digito_Verificador.ToString(); //Importe de entero a string

            //Ahora sacamos la fecha juliana
            int Fecha_Juliana_I = ((dia_I - 1) + ((mes_I-1) * 31) + ((anio_I-2013)*372));
            string Fecha_Juliana;
            if(Fecha_Juliana_I < 1000)
            {
                Fecha_Juliana = Fecha_Juliana_I.ToString();
                Fecha_Juliana = "0" + Fecha_Juliana;
            }
            else
            {
                Fecha_Juliana_I = (Fecha_Juliana_I % 10000);
                Fecha_Juliana = Fecha_Juliana_I.ToString();
            }

            //Obtenemos los digitos verificadores.
            string Referencia_Completa;
            string caracter;
            int suma_Digitos = 0;
            bool IsNumero;
            int Numero;
            string Mostrar_Suma_Digitos;

            Referencia_Completa = request.Establecimiento + request.Tipo_Pago + request.Referencia_Principal + request.Referencia_Adicional + Fecha_Juliana + Digito_Verificador + DC;

            selector = 11;
            for (int i = Referencia_Completa.Length; i > 0;i--)
            {
                caracter = Referencia_Completa.Substring(i-1, 1);
                IsNumero = int.TryParse(caracter,out Numero);
                if(IsNumero == false)
                {
                    switch (caracter)
                    {
                        case "A":
                            caracter = "1";
                            break;
                        case "B":
                            caracter = "2";
                            break;
                        case "C":
                            caracter = "3";
                            break;
                        case "D":
                            caracter = "4";
                            break;
                        case "E":
                            caracter = "5";
                            break;
                        case "F":
                            caracter = "6";
                            break;
                        case "G":
                            caracter = "7";
                            break;
                        case "H":
                            caracter = "8";
                            break;
                        case "I":
                            caracter = "9";
                            break;
                        case "J":
                            caracter = "1";
                            break;
                        case "K":
                            caracter = "2";
                            break;
                        case "L":
                            caracter = "3";
                            break;
                        case "M":
                            caracter = "4";
                            break;
                        case "N":
                            caracter = "5";
                            break;
                        case "O":
                            caracter = "6";
                            break;
                        case "P":
                            caracter = "7";
                            break;
                        case "Q":
                            caracter = "8";
                            break;
                        case "R":
                            caracter = "9";
                            break;
                        case "S":
                            caracter = "2";
                            break;
                        case "T":
                            caracter = "3";
                            break;
                        case "U":
                            caracter = "4";
                            break;
                        case "V":
                            caracter = "5";
                            break;
                        case "W":
                            caracter = "6";
                            break;
                        case "X":
                            caracter = "7";
                            break;
                        case "Y":
                            caracter = "8";
                            break;
                        case "Z":
                            caracter = "9";
                            break;

                    }


                }

                Numero = Int32.Parse(caracter);
                suma_Digitos = suma_Digitos + (Numero * selector);

                switch (selector)
                {
                    case 11:
                        selector = 13;
                        break;
                    case 13:
                        selector = 17;
                        break;
                    case 17:
                        selector = 19;
                        break;
                    case 19:
                        selector = 23;
                        break;
                    case 23:
                        selector = 11;
                        break;
                }

            }

            Mostrar_Suma_Digitos = suma_Digitos.ToString();
            int digitos_verificadores_I = suma_Digitos % 97;
            digitos_verificadores_I = digitos_verificadores_I+1;
            string Digitos_Verificadores = digitos_verificadores_I.ToString();


            string resultado;
            //resultado = request.Establecimiento + " " +request.Tipo_Pago + " " + request.Referencia_Principal + " " + request.Referencia_Adicional + " " + Fecha_Juliana + " "  + Digito_Verificador + " " + DC + " " + request.Importe + " " + Mostrar_Suma_Digitos + " " + Digitos_Verificadores;
            resultado = request.Establecimiento + request.Tipo_Pago + request.Referencia_Principal + request.Referencia_Adicional + Fecha_Juliana + Digito_Verificador + DC + Digitos_Verificadores;

            return resultado;
        }
    }
}
