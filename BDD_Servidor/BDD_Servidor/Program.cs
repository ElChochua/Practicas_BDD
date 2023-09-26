using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net;

namespace Pracica1_ClienteServidor_Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**Aplicacion Servidor**");
            while (true)
            {
                try
                {
                    IPAddress ipAd = IPAddress.Any;

                    TcpListener myList = new TcpListener(ipAd, 8001);
                    myList.Start();
                    Console.WriteLine("Servidor iniciado en el puerto 8001");
                    Console.WriteLine("Local End Point: " + myList.LocalEndpoint);
                    Console.WriteLine("Esperando Conexion...");
                    Socket socket = myList.AcceptSocket();
                    Console.WriteLine("Conexion recibida desde: " + socket.RemoteEndPoint);
                    byte[] b = new byte[100];
                    int k = socket.Receive(b);
                    Console.WriteLine("Recibiendo...");

                    string cadena = "";
                    for(int i = 0; i < k; i++)
                    {
                        cadena = cadena + Convert.ToChar(b[i]);
                    }
                    Console.WriteLine(cadena);

                    string connectSQL = "Server=localhost;database=BDD_Practica1;" + " Integrated Security=SSPI;";
                    SqlConnection cm = new SqlConnection();
                    cm.ConnectionString = connectSQL;
                    cm.Open();

                    SqlCommand cmd = new SqlCommand(cadena, cm);
                    cmd.ExecuteNonQuery();
                    cm.Close();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    socket.Send(asen.GetBytes("Cadena recibida. comando ejecutado"));
                    Console.WriteLine("\nConfirmacion Enviada");
                    socket.Close();
                    myList.Stop();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error..." + e);
                }
            }
        }
    }
}