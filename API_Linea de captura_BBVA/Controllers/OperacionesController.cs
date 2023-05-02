using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Net;
using System;
using API_Linea_de_captura_BBVA.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace API_Linea_de_captura_BBVA.Controllers
{

    [ApiController]
    public class OperacionesController : ControllerBase
    {

        [HttpGet]
        [Route("api/Linea_Captura_BBVA")]
        public string GetLineaCaptura ()
        {
            //        string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString()
            var ipHost = HttpContext.Connection.RemoteIpAddress.ToString();
            string Navegador = Request.Headers["User-Agent"];
            DateTime thisDay = DateTime.Now;
            return ipHost;
            
        }


        [HttpPost]
        [Route("api/Linea_Captura_BBVA")]
        public string PostLineaCaptura(VariablesRequest request)
        {
            //validamos acceso
            var llave = "bWK0gJc1wn6X2e0$&2uY";
            
            //Obtiene la llave harcodeada en el json
            //ReferenciaPrincipal + referencia adicional + importe + anio + mes + dia +numerosAleatorios
            var Firma_Api = request.Referencia_Principal + request.Referencia_Adicional + request.Importe + request.Anio + request.Mes + request.Dia + request.Numero_Aleatorio;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(llave); //Remplaza cada caracter por su valor ASCII
            byte[] messageBytes = encoding.GetBytes(Firma_Api);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);
            byte[] bytes = cryptographer.ComputeHash(messageBytes);
            string hash = "";
    
            for (int i = 0; i < bytes.Length; i++)
            {
                hash += bytes[i].ToString("X2"); // hex format
            }


            if (hash != request.Firma)
            {
                return ("la peticion no es valida, favor de contactar a su departamento de sistemas");
            }

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
                        case "a":
                            caracter = "1";
                            break;
                        case "b":
                            caracter = "2";
                            break;
                        case "c":
                            caracter = "3";
                            break;
                        case "d":
                            caracter = "4";
                            break;
                        case "e":
                            caracter = "5";
                            break;
                        case "f":
                            caracter = "6";
                            break;
                        case "g":
                            caracter = "7";
                            break;
                        case "h":
                            caracter = "8";
                            break;
                        case "i":
                            caracter = "9";
                            break;
                        case "j":
                            caracter = "1";
                            break;
                        case "k":
                            caracter = "2";
                            break;
                        case "l":
                            caracter = "3";
                            break;
                        case "m":
                            caracter = "4";
                            break;
                        case "n":
                            caracter = "5";
                            break;
                        case "o":
                            caracter = "6";
                            break;
                        case "p":
                            caracter = "7";
                            break;
                        case "q":
                            caracter = "8";
                            break;
                        case "r":
                            caracter = "9";
                            break;
                        case "s":
                            caracter = "2";
                            break;
                        case "t":
                            caracter = "3";
                            break;
                        case "u":
                            caracter = "4";
                            break;
                        case "v":
                            caracter = "5";
                            break;
                        case "w":
                            caracter = "6";
                            break;
                        case "x":
                            caracter = "7";
                            break;
                        case "y":
                            caracter = "8";
                            break;
                        case "z":
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


            //Modificamos los tipos de datos para que la base de datos pueda leer
            var ipHost = HttpContext.Connection.RemoteIpAddress.ToString(); //Obtenemos la ip de la peticion
            string Navegador = Request.Headers["User-Agent"]; //Obtenemos el navegador
            DateTime f_peticion = DateTime.Now; //obtenemos la fecha de hoy con su horario
            //pasamos los datos de la peticion  a variables con su tipo de dato de la base de datos
            int establecimiento = Int32.Parse(request.Establecimiento);
            int tipopago = Int32.Parse(request.Tipo_Pago);
            int dia = Int32.Parse(request.Dia);
            int mes = Int32.Parse(request.Mes);
            int anio = Int32.Parse(request.Anio);
            string referenciaP = request.Referencia_Principal;
            string referenciaA = request.Referencia_Adicional;
            int numeroA = Int32.Parse(request.Numero_Aleatorio);

            string Importearray = request.Importe;
            string entero = "";
            string decimales = "";

            for (int i = 0; i < request.Importe.Length; i++)
            {
                caracter = Importearray.Substring(i, 1);
                if((request.Importe.Length - i) > 2)
                {
                    entero = entero + caracter;
                }
                else
                {
                    decimales = decimales + caracter;
                }
            }

            string importeX = entero + "." + decimales;
            double Importe = double.Parse(importeX);
    
            //Establecemos la coneccion con mysql
            using (var context = new LpBbvaContext())
            {
                var s = new PeticionesLp();
                s.IpCliente = ipHost;
                s.Navegador = Navegador;
                s.FechaPeticion = f_peticion;
                s.Establecimiento = establecimiento;
                s.TipoPago = tipopago;
                s.Importe = Importe;
                s.Dia = dia;
                s.Mes = mes;
                s.Anio = anio;
                s.ReferenciaPrincipal = referenciaP;
                s.ReferenciaAdicional = referenciaA;
                s.NumeroAleatorio = numeroA;
                s.Firma = hash;
                s.LineaCaptura = resultado;
                context.PeticionesLps.Add(s);
                context.SaveChanges();
            }
            return resultado;
        }
    }
}
