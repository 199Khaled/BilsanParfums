using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HilfsMethoden
{
    public class clsOrdnerFürBilderErstellen
    {
        public static string GenerateGuide()
        {
            // Generate a new Guid
            Guid guid = Guid.NewGuid();
            //Convert Guid to string()

            return guid.ToString();
        }

        public static bool CreateFolderIFDoesNotExist(string Foldername)
        {
            if (string.IsNullOrEmpty(Foldername))
                return false;

           //wir überprüfen, if Ordner existiert.
           if(!Directory.Exists(Foldername))
            {
                try
                {
                    //wenn der Order nicht existiert
                    Directory.CreateDirectory(Foldername);
                    return true;
                }
                catch(IOException io)
                {
                    Console.WriteLine(io.Message);
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGuid(string Filename)
        {
            // Wir leiten die FileErweiterung ab
            // und liefern wir die mit der Guid wieder zurück.
            FileInfo File = new FileInfo(Filename);
            return GenerateGuide() + File.Extension;
        }
        public static bool CopyImageToProjectImageFolder(ref string QuellDateiPfad)
        {
            //Diese Methode kopiert das Bild zum Bilder Ordner nach Umbennen des Bilds mit der Guid und 
            // mit derselben Erweiterung ,danach wird der SourceDateiName mit dem neuen Namen aktualisiert.

            string ZielOrder = "C:\\Users\\Khaled\\Desktop\\Klinik Program\\PersonenBilder\\";

            if (!CreateFolderIFDoesNotExist(ZielOrder))
                return false;

            string ZielDateiPfad = ZielOrder + ReplaceFileNameWithGuid(QuellDateiPfad);
            try
            {
                File.Copy(QuellDateiPfad, ZielDateiPfad, true);
            }
            catch(IOException io)
            {
                Console.WriteLine(io.Message);
                return false;
            }

            QuellDateiPfad = ZielDateiPfad;
            return true;
        }
    }
}
