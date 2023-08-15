using System;
using System.Collections;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace MT_ENVIO_CORREO
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string ruta = @"C:\Users\Usuario\Desktop\Prueba\tmp\Descarga_CFDI\Descarga\DctRelacionados multiples";
                //string ruta_mover = @"C:\Users\Usuario\Desktop\Prueba\tmp\Descarga_CFDI\Descarga\Procesados";

                string ruta = @"C:\Users\Usuario\Desktop\Prueba\tmp\Descarga_CFDI\Descarga\Se encontro 2022";
                string ruta_mover = @"C:\Users\Usuario\Desktop\Prueba\tmp\Descarga_CFDI\Descarga\Procesa2022";

                string correo_oringe = "carlos_egr23@hotmail.com";
                string constraseña = "Charles9424";
                string correo_destino = "cfdi_arr@value.com.mx";
                //string correo_destino = "carlos.gomez@value.com.mx";

                int file_count = 0;
                file_count = 59;

                do
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(ruta);
                    FileInfo[] filt = directoryInfo.GetFiles();
                    
                    int i = 0;
                    DirectoryInfo di = new DirectoryInfo(ruta);
                    FileInfo[] files = di.GetFiles("*.xml");
                    ArrayList Archivos_mover = new ArrayList();
                  

                    MailMessage oMailMessage = new MailMessage(correo_oringe, correo_destino, "Carga Masiva", "");

                    do
                    {
                        string nombre_archivo = Path.GetFileNameWithoutExtension(files[i].FullName);
                        string Archivo_xml = ruta + "\\" + nombre_archivo + ".xml";
                        string Archivo_pdf = ruta + "\\" + nombre_archivo + ".pdf";
                        Archivos_mover.Add("\\" + nombre_archivo + ".xml");
                        Archivos_mover.Add("\\" + nombre_archivo + ".pdf");

                        i++;
                    } while (i <= 9);
                    int j = 0;
                    int h =0;
                    do
                    {
                        oMailMessage.Attachments.Add(new Attachment(ruta + Archivos_mover[h].ToString()));
                        h++;
                    } while (h < Archivos_mover.Count);

                    string strop2 = "Stop";
                    //SmtpClient smtpClient = new SmtpClient("webmail.value.com.mx");
                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    //smtpClient.Port = 465;
                    smtpClient.Port = 587;
                    smtpClient.Timeout = 100000;
                    smtpClient.Credentials = new System.Net.NetworkCredential(correo_oringe, constraseña);
                    smtpClient.Send(oMailMessage);
                    smtpClient.Dispose();

                    oMailMessage.Dispose();


                    do
                    {
                        File.Move(ruta + Archivos_mover[j].ToString(), ruta_mover+ Archivos_mover[j].ToString());

                        j++;
                    }while(j<Archivos_mover.Count);

                    string stop="stop";
                    file_count--;
                    Thread.Sleep(100);
                } while (0<= file_count);
            }
            catch (Exception ex)
            {
              string error = ex.ToString();
            }
           
        }
    }
}
