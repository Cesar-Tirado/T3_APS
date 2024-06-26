using ProyectoTest.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using System.Text;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Web.Caching;
using System.Web.Razor.Parser.SyntaxTree;
using System.Net.Mime;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Image = iTextSharp.text.Image;

namespace ProyectoTest.Logica
{
    public class CorreoLogica
    {
        private static CorreoLogica _instancia = null;

        public CorreoLogica()
        {

        }

        public static CorreoLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CorreoLogica();
                }

                return _instancia;
            }
        }



        public bool EnviarCorreo(Compra oCompra)
        {
            bool sendMail = false; 

            try
            {
                AppSettingsReader DatosConexion = new AppSettingsReader();

                string mail = DatosConexion.GetValue("mail", typeof(string)).ToString();
                string pass = DatosConexion.GetValue("pass", typeof(string)).ToString();
                string mailCopy = DatosConexion.GetValue("mailCopy", typeof(string)).ToString();
                string idCompraFormateado = oCompra.IdCompra.ToString("D8");

                decimal precioTotal = oCompra.Total;
                string DireccionTienda = "";
                string DireccionCliente = "";
                if (oCompra.Tipo == "Recojo")
                {
                    DireccionTienda = oCompra.Direccion;
                    DireccionCliente = "";
                    oCompra.Tipo = "Recojo en Tienda";

                } else if ( oCompra.Tipo == "Delivery")
                {
                    DireccionTienda = "Calle Ernesto Plascencia 300, San Isidro";
                    DireccionCliente = oCompra.Direccion;
                }  


                StringBuilder query = new StringBuilder();
                string tablaHTML = "<table><tr><th class='columna-derecha' style='padding-right: 100px'>Detalle de tu pedido:</th><th class='columna-derecha'></th></tr>";

                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    string precioExtraFormat = dc.PrecioExtra.ToString("0.00");

                    tablaHTML += "<tr>";
                    tablaHTML += "<td class='columna-izquierda'><b>" + dc.Cantidad + " x " + " " + dc.Nombre + "</b></td>";
                    tablaHTML += "<td class='columna-derecha' style='width:100px;'><b>" + "S/. " + precioExtraFormat + "</b></td>";
                    tablaHTML += "</tr>";
                    tablaHTML += "<tr>";
                    tablaHTML += "<td class='columna-izquierda' style='padding-left:30px;'>" + dc.Adicionales + "</td>";
                    tablaHTML += "<td class='columna-izquierda' style='padding-left:30px;'>" + dc.ObservacionesDC + "</td>";
                    tablaHTML += "<td class='columna-derecha'></td>";
                    tablaHTML += "</tr>";                   
                }
                tablaHTML += "</ table >";

                MailMessage Correos = new MailMessage();
                SmtpClient envio = new SmtpClient();

                Correos.To.Clear();
                Correos.Attachments.Clear();
                Correos.Bcc.Add(mailCopy);
                Correos.Subject = "Pedido RestoManage";
                Correos.Body = @"
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;            
                            }        
                            .container {
                                max-width: 600px;
                                text-align: center;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }
                            h1, h5 {
                                color: #333333;
                                padding: 0px;
                                margin: 5px;
                            }
                            p {
                                color: #666666;
                            }

                            .container2 {
                                width: 600px;
                                margin: 0 auto;
                            }

                            .columna-izquierda {
                                text-align: left;
                            }
        
                            .columna-derecha {
                                text-align: right!important;
                            }

                            table {
                                font-size: 12px;
                                margin: 0px;
                                width: 600px;
                                text-align: left;
                                border-collapse: collapse;
                            }

                            th {
                                font-size: 14px;
                                font-weight:bold;
                                padding: 8px;
                                background: #d3d3d3;
                            }

                            td {
                                padding: 8px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <p>¡Hola " + oCompra.Nombre + @", tu pedido ha sido recibido!</p>
                            <h5>Le notificaremos el estado de su pedido" + @" </h5>
                            <h5>Su orden tiene el ID: N° " + idCompraFormateado + @" </h5>
                            <h5>Su pedido se programó para : " + oCompra.HoraRecojo + @" </h5><br>
                            <span style='font-size:14px; font-weight:bold; color:black'> Su pedido: " + oCompra.Tipo + @"  </span><hr>

                            <div class='container2'>
                                <table>
                                    <tr>
                                        <th class='columna-izquierda'>Datos del Local</td>
                                        <th class='columna-derecha'>Datos del Cliente</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>RestoManage</td>
                                        <td class='columna-derecha'> " + oCompra.Nombre + @"</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>" + DireccionTienda + @"</td>
                                        <td class='columna-derecha'>" + DireccionCliente + @"</td>
                                    </tr>   
                                    <tr>
                                        <td class='columna-izquierda'>Lima - Perú</td>
                                        <td class='columna-derecha'> " + oCompra.DocumentoFacturacion + @"</td>
                                    </tr>  
                                    <tr>
                                        <td class='columna-izquierda'>014213916</td>
                                        <td class='columna-derecha'> " + oCompra.Telefono + @"</td>
                                    </tr> 
                                    <tr>
                                        <td class='columna-izquierda'> " + mail + @"</td>
                                        <td class='columna-derecha'> " + oCompra.Correo + @" </td>
                                    </tr>                            
                                </table>
                                <hr> 
                            </div>

                            <div class='container2'>
                               " + tablaHTML + @"                             
                            </div>

                            <div class='container2'>
                                <table>
                                    <tr>
                                        <th class='columna-izquierda'>Total Parcial: </td>
                                        <th class='columna-derecha'> S/." + oCompra.Total + @"</td>
                                    </tr>
                                    <tr>
                                        <th class='columna-izquierda'>Costo de envío: </td>
                                        <th class='columna-derecha'> S/.0.00  </td>
                                    </tr>
                                    <tr>
                                        <th class='columna-izquierda' style='font-size:24px;'>Total: </td>
                                        <th class='columna-derecha' style='font-size:24px;'> S/. " + oCompra.Total + @"</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda' style='padding-top: 20px;'>Método de Pago: " + oCompra.FormaPago + @" </td>
                                        <td class='columna-derecha' style='padding-top: 20px;'>Método de entrega: " + oCompra.Tipo + @" </td>
                                    </tr>
                                </table>            
                            </div>        
                        </div>
                    </body>
                    </html>";

                Correos.IsBodyHtml = true;
                Correos.To.Add(oCompra.Correo); // Destino

                Correos.From = new MailAddress(mail);
                envio.Credentials = new NetworkCredential(mail, pass);

                envio.Host = "smtp.gmail.com";
                envio.Port = 587;
                envio.EnableSsl = true;
                envio.Send(Correos);
                sendMail = true;
                Console.WriteLine("Correo Enviado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " +ex.Message + ex.ToString());
            }
            return sendMail;
        }

        public bool PedidoAceptado(int idCompra, string nombre, string correo)
        {
            bool sendMail = false;

            try
            {
                AppSettingsReader DatosConexion = new AppSettingsReader();

                string mail = DatosConexion.GetValue("mail", typeof(string)).ToString();
                string pass = DatosConexion.GetValue("pass", typeof(string)).ToString();
                string mailCopy = DatosConexion.GetValue("mailCopy", typeof(string)).ToString();




                StringBuilder query = new StringBuilder();
                string tablaHTML = "<table><tr><th class='columna-derecha' style='padding-right: 100px'>Detalle de tu pedido:</th><th class='columna-derecha'></th></tr>";


                tablaHTML += "</ table >";

                MailMessage Correos = new MailMessage();
                SmtpClient envio = new SmtpClient();

                Correos.To.Clear();
                Correos.Attachments.Clear();
                Correos.Bcc.Add(mailCopy);
                Correos.Subject = "Pedido Aceptado";
                Correos.Body = @"
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;            
                            }        
                            .container {
                                max-width: 600px;
                                text-align: center;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }
                            h1, h5 {
                                color: #333333;
                                padding: 0px;
                                margin: 5px;
                            }
                            p {
                                color: #666666;
                            }

                            .container2 {
                                width: 600px;
                                margin: 0 auto;
                            }

                            .columna-izquierda {
                                text-align: left;
                            }
        
                            .columna-derecha {
                                text-align: right!important;
                            }

                            table {
                                font-size: 12px;
                                margin: 0px;
                                width: 600px;
                                text-align: left;
                                border-collapse: collapse;
                            }

                            th {
                                font-size: 14px;
                                font-weight:bold;
                                padding: 8px;
                                background: #d3d3d3;
                            }

                            td {
                                padding: 8px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <p>¡Hola " + nombre + @", Muchas gracias por tu compra tu pedido ya está en camino :) !</p>
                            <h5>Su número de pedido es: N° " + idCompra + @" </h5>
                            

                            
                        </div>
                    </body>
                    </html>";

                Correos.IsBodyHtml = true;
                Correos.To.Add(correo); // Destino

                Correos.From = new MailAddress(mail);
                envio.Credentials = new NetworkCredential(mail, pass);

                envio.Host = "smtp.gmail.com";
                envio.Port = 587;
                envio.EnableSsl = true;
                envio.Send(Correos);
                sendMail = true;
                Console.WriteLine("Correo Enviado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message + ex.ToString());
            }
            return sendMail;
        }

        public bool PedidoRechazado(int idCompra, string nombre, string correo)
        {
            bool sendMail = false;

            try
            {
                AppSettingsReader DatosConexion = new AppSettingsReader();

                string mail = DatosConexion.GetValue("mail", typeof(string)).ToString();
                string pass = DatosConexion.GetValue("pass", typeof(string)).ToString();
                string mailCopy = DatosConexion.GetValue("mailCopy", typeof(string)).ToString();
                

       
 
                StringBuilder query = new StringBuilder();
                string tablaHTML = "<table><tr><th class='columna-derecha' style='padding-right: 100px'>Detalle de tu pedido:</th><th class='columna-derecha'></th></tr>";

                
                tablaHTML += "</ table >";

                MailMessage Correos = new MailMessage();
                SmtpClient envio = new SmtpClient();

                Correos.To.Clear();
                Correos.Attachments.Clear();
                Correos.Bcc.Add(mailCopy);
                Correos.Subject = "Pedido Rechazado";
                Correos.Body = @"
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;            
                            }        
                            .container {
                                max-width: 600px;
                                text-align: center;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }
                            h1, h5 {
                                color: #333333;
                                padding: 0px;
                                margin: 5px;
                            }
                            p {
                                color: #666666;
                            }

                            .container2 {
                                width: 600px;
                                margin: 0 auto;
                            }

                            .columna-izquierda {
                                text-align: left;
                            }
        
                            .columna-derecha {
                                text-align: right!important;
                            }

                            table {
                                font-size: 12px;
                                margin: 0px;
                                width: 600px;
                                text-align: left;
                                border-collapse: collapse;
                            }

                            th {
                                font-size: 14px;
                                font-weight:bold;
                                padding: 8px;
                                background: #d3d3d3;
                            }

                            td {
                                padding: 8px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <p>¡Hola " + nombre + @", lo sentimos tu pedido ha sido rechazado por el restaurante!</p>
                            <h5>Su número de pedido es: N° " + idCompra + @" </h5>
                            

                            
                        </div>
                    </body>
                    </html>";

                Correos.IsBodyHtml = true;
                Correos.To.Add(correo); // Destino

                Correos.From = new MailAddress(mail);
                envio.Credentials = new NetworkCredential(mail, pass);

                envio.Host = "smtp.gmail.com";
                envio.Port = 587;
                envio.EnableSsl = true;
                envio.Send(Correos);
                sendMail = true;
                Console.WriteLine("Correo Enviado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message + ex.ToString());
            }
            return sendMail;
        }

        public bool EnviarVale(int IdCliente, string Email, string Telefono, string Nombre, string Apellidos, string Distrito)
        {
            bool sendMail = false;

            try
            {
                AppSettingsReader DatosConexion = new AppSettingsReader();
                string mail = DatosConexion.GetValue("mail", typeof(string)).ToString();
                string pass = DatosConexion.GetValue("pass", typeof(string)).ToString();

                // Crear un nuevo documento PDF
                Document doc = new Document();

                // Ruta para guardar el archivo PDF C:\Descuentos

                //string pdfPath = @"C:\Users\cesar\OneDrive\Documentos\PDFS\descuento"+IdCliente+".pdf";
                string pdfPath = @"C:\Descuentos\descuento"+IdCliente+".pdf";

                // Inicializar un objeto FileStream para escribir el PDF
                FileStream fs = new FileStream(pdfPath, FileMode.Create);

                // Crear un escritor PDF
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                // Abrir el documento para editar
                doc.Open();
                BaseColor colorRojo = new BaseColor(176, 23, 31); 
                BaseColor colorNegro = BaseColor.BLACK;                
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font customFont = new Font(bf, 12, Font.ITALIC, BaseColor.BLACK);


                // Agregar más contenido, como imágenes, tablas, etc.

                string imagePath = @"C:\Descuentos\Billete_LHG.png";


                Image img = Image.GetInstance(imagePath);
                img.ScaleToFit(500f, 500f); // Ajusta el tamaño de la imagen
                doc.Add(img);
                Random random = new Random();
                int numeroAleatorio = random.Next(1000, 10000);
                string codigo = "LHG" + numeroAleatorio + IdCliente;
                // Agregar un párrafo con un estilo de fuente personalizado
                Paragraph paragraph = new Paragraph(codigo, customFont);
                doc.Add(paragraph);

                // Cerrar el documento PDF
                doc.Close();

                // Crear el mensaje de correo
                MailMessage Correos = new MailMessage();
                SmtpClient envio = new SmtpClient();

                Correos.To.Clear();
                Correos.Attachments.Clear();
                Correos.Subject = "Regalo Grill & Drinks";

                // Adjuntar el PDF
                Attachment pdfAttachment = new Attachment(pdfPath);
                Correos.Attachments.Add(pdfAttachment);

                Correos.From = new MailAddress(mail);
                Correos.To.Add(Email);

                // Construir el cuerpo HTML del correo con los datos del cliente
                string htmlBody = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;            
                            }}        
                            .container {{
                                max-width: 600px;
                                text-align: center;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }}
                            h1, h5 {{
                                color: #333333;
                                padding: 0px;
                                margin: 5px;
                            }}
                            p {{
                                color: #666666;
                            }}

                            .container2 {{
                                width: 600px;
                                margin: 0 auto;
                            }}

                            .columna-izquierda {{
                                text-align: left;
                            }}
        
                            .columna-derecha {{
                                text-align: right!important;
                            }}

                            table {{
                                font-size: 12px;
                                margin: 0px;
                                width: 600px;
                                text-align: left;
                                border-collapse: collapse;
                            }}

                            th {{
                                font-size: 14px;
                                font-weight:bold;
                                padding: 8px;
                                background: #d3d3d3;
                            }}

                            td {{
                                padding: 8px;
                            }}
                        </style>
                    </head>
                    <body>
                        
                        <!-- Agregar más contenido si es necesario -->
                        <div style='text-align: center; margin-top: 20px;'>
                            <a href='https://207.244.224.249/' style='text-decoration: none;'>
                                <button style='
                                    display: inline-block;
                                    font-size: 14px;
                                    padding: 10px 20px;
                                    margin: 10px;
                                    background-color: #ff0000; /* Puedes cambiar el color del fondo */
                                    color: #ffffff; /* Puedes cambiar el color del texto */
                                    border: none;
                                    border-radius: 5px;
                                    cursor: pointer;
                                '>Compra Aquí</button>
                            </a>
                        </div>


                        <div class='container'>
                            
                            <p>¡Hola {Nombre} {Apellidos}!</p>
                            <h4>Felicitaciones le adjuntamos su billete </h4>
                            <div class='container2'>
                                <table>
                                    <tr>
                                        <th class='columna-izquierda'>Sus Datos</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>Nombre: {Nombre}</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>Apellidos: {Apellidos}</td>
                                    </tr>   
                                    <tr>
                                        <td class='columna-izquierda'>Teléfono: {Telefono}</td>
                                    </tr>  
                                    <tr>
                                        <td class='columna-izquierda'>Correo: {Email} </td>
                                    </tr> 
                                    <tr>
                                        <td class='columna-izquierda'>Código de consumo: {codigo}</td>
                                    </tr>                            
                                </table>
                            </div>
                            
                            <div class='container2'>
                                <table>
                                    <tr>
                                        <th class='columna-izquierda'>Observaciones</td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>-Valido en nuestros locales 
                                             en Lima de Real Plaza Puruchuco, Mall Aventura Santa
                                            Anita y Centro Comercial Mega Plaza en Independencia.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class='columna-izquierda'>-Valido de Lunes a Viernes.</td>
                                    </tr>   
                                    <tr>
                                        <td class='columna-izquierda'>-Valido hasta 01-06-2024</td>
                                    </tr>  
                                    <tr>
                                        <td class='columna-izquierda'>-Se aplica a todos los consumos en el 
                                            local mayores a S/100.00.</td>
                                    </tr> 
                                    <tr>
                                        <td class='columna-izquierda'>-Válido para ser usado una sola vez.</td>
                                    </tr>                            
                                </table>
                                <hr> 
                                <span>Canjear su billete al momento de cancelar su consumo.</span>
                                <span>Verificar su vigencia antes del consumo.</span>
                            </div>

                            <div style='text-align: center; margin-top: 20px;'>
                            <a href='https://207.244.224.249/' style='text-decoration: none;'>
                                <button style='
                                    display: inline-block;
                                    font-size: 14px;
                                    padding: 10px 20px;
                                    margin: 10px;
                                    background-color: #ff0000; /* Puedes cambiar el color del fondo */
                                    color: #ffffff; /* Puedes cambiar el color del texto */
                                    border: none;
                                    border-radius: 5px;
                                    cursor: pointer;
                                '> Web</button>
                            </a>
                        </div>

                        </div>
                    </body>
                </html>
                ";

                Correos.IsBodyHtml = true;
                Correos.Body = htmlBody;

                envio.Credentials = new NetworkCredential(mail, pass);
                envio.Host = "smtp.gmail.com";
                envio.Port = 587;
                envio.EnableSsl = true;

                // Enviar el correo
                envio.Send(Correos);

                // Eliminar el archivo PDF después de enviarlo
                fs.Close();
                //File.Delete(pdfPath);

                sendMail = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }

            return sendMail;
        }


       
    }
}