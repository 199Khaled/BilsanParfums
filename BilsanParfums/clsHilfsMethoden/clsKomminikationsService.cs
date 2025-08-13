using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace clsHilfsMethoden
{
    public class clsKomminikationsService
    {
        private static TcpListener server;
        private static bool running = false;

        // Methode zum Starten des Servers
        public static string StartServer(bool freigabe)
        {
            string result = string.Empty;

            if (freigabe)
            {
                // Der Server wird nur gestartet, wenn 'freigabe' wahr ist
                try
                {
                    server = new TcpListener(IPAddress.Any, 5000); // Port 12345 oder anderer Port
                    server.Start();
                    running = true;

                    Task.Run(() =>
                    {
                        if(running && server != null && server.Server.IsBound)
                        {
                            if (server.Pending())
                            {
                                using (TcpClient client = server.AcceptTcpClient())
                                {
                                    NetworkStream stream = client.GetStream();
                                    byte[] buffer = new byte[1024];
                                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                                    if (bytesRead > 0)
                                    {
                                        string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                        result = request;

                                        // Weitere Nachrichtenverarbeitung hier
                                        string response = "Nachricht empfangen.";
                                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                                        stream.Write(responseBytes, 0, responseBytes.Length);
                                    }
                                }
                            }
                            else
                            {
                                Thread.Sleep(100); // CPU-Belastung minimieren
                            }
                        }

                    });


                }
                catch (Exception ex)
                {
                    return $"Fehler beim Starten des Servers: {ex.Message}";
                }

            }
            else
            {
                return "Freigabe nicht aktiviert.";
            }
            return result;
        }

        // Methode zum Stoppen des Servers
        public static void StopServer()
        {
            try
            {
                if (server != null)
                {
                    server.Stop();
                }

                server = null; // Server-Objekt löschen
                running = false; // Server-Zustand zurücksetzen
            }
            catch
            {
                // Ignoriere Fehler beim Stoppen, da der Server ohnehin zurückgesetzt wird
            }
        }

        public static async Task<string> StartServerAsync()
        {
            string result = string.Empty;
            //if (!freigabe)
            //    return "Freigabe nicht aktiviert.";

            try
            {
                if (server != null)
                {
                    server.Stop(); // Sicherstellen, dass vorherige Instanz gestoppt wurde
                }

                server = new TcpListener(IPAddress.Any, 5000);  // server: Erstellt einen neuen Server, der auf Port 5000 lauscht und Verbindungen von allen IP-Adressen (IPAddress.Any) akzeptiert.
                server.Start(); // server.Start(): Startet den Server, damit er auf Verbindungen wartet.
                running = true; //running = true: Die Schleife bleibt aktiv, solange running auf true gesetzt ist.

                while (running)
                /*
                 * server != null: Stellt sicher, dass der Server initialisiert wurde.
                   server.Server.IsBound: Prüft, ob der Server erfolgreich an den Port gebunden ist.
                     server.Pending(): Überprüft, ob eingehende Verbindungen in der Warteschlange sind.
                 */
                {
                    if (server != null && server.Server.IsBound && server.Pending())
                    {
                        using (TcpClient client = await server.AcceptTcpClientAsync()) //AcceptTcpClientAsync(): Wartet asynchron auf eine eingehende Verbindung und akzeptiert diese.
                                                                                       //   Die Verbindung wird als TcpClient-Objekt bereitgestellt.
                        {
                            NetworkStream stream = client.GetStream(); //GetStream(): Ruft den Netzwerk-Datenstrom ab, der für das Lesen und Schreiben von Daten verwendet wird.
                            byte[] buffer = new byte[1024];  //buffer: Ein Puffer, in den empfangene Daten gespeichert werden.
                            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); //ReadAsync(): Liest Daten asynchron vom Netzwerk-Stream in den Puffer.
                           // bytesRead: Die Anzahl der tatsächlich gelesenen Bytes.
                            if (bytesRead > 0) 
                            {
                                result = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                                // Antwort an den Client senden
                                string response = "Nachricht empfangen, Vielen Dank für Ihren Einkauf!.";
                                byte[] responseBytes = Encoding.UTF8.GetBytes(response);  
                                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);  // //WriteAsync(): Sendet die Nachricht asynchron über den Netzwerk-Stream. 

                                return result;
                            }
                        }
                    }

                    await Task.Delay(100); // CPU-Entlastung
                }
            }
            catch (Exception ex)
            {
                StopServer(); // Stelle sicher, dass der Server zurückgesetzt wird
                return $"Fehler beim Starten des Servers: {ex.Message}";
            }
            return result;
        }
    }
}
