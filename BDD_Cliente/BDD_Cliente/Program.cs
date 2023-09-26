using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace Practica1_ClienteServidor_Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**Aplicacion Cliente**");
            while (true)
            {
                try
                {
                    TcpClient tcpclnt = new TcpClient();
                    Console.WriteLine("Conectando...");
                    tcpclnt.Connect("127.0.0.1", 8001);
                    Console.WriteLine("Conectando al servidor");
                    Console.WriteLine("Introduzca la cadena a ejecutar");
                    String str = Console.ReadLine();
                    Stream stm = tcpclnt.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Transmitiendo cadena...");
                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);
                    string acuse = "";
                    for(int i=0;i<k ; i++)
                    {
                        acuse = acuse + Convert.ToChar(bb[i]);
                        Console.WriteLine(acuse);
                        tcpclnt.Close();
                    }
                }catch(Exception e)
                {
                    Console.WriteLine("Error..." + e.StackTrace);
                }
            }
        }
    }
}