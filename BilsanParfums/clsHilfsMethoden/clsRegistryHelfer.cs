using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class DatenzugriffExecption : Exception
{
    public DatenzugriffExecption(string message) : base(message) { }
    public DatenzugriffExecption(string message, Exception innermessage) : base(message) { }
    
}
 public static  class clsRegistryHelfer
 {
    private static readonly string RegistryPath = @"SOFTWARE\MyApp\Security";
    public static void GenerateAndSaveKeyAndIV()
    {
        // hardcoded
        string Kaystring = "0123456789012345";
        // Aes für Advanced Encryption Standard.
        using (Aes aes = Aes.Create())
        {
            //byte[] Key = aes.Key;  //Die richtige Größe (128, 192 oder 250 Bit) wird automatisch      zugewiesen ;
            //byte[] IV = aes.IV; // Die Größe ist immer 128 Bit;


            // Generiere einen zufälligen Schlüssel und IV
            aes.Key = Encoding.UTF8.GetBytes(Kaystring);
            aes.GenerateIV();

            SaveKeyAndIV(aes.Key, aes.IV);
        }
    }
    public static void SaveKeyAndIV(byte[] key, byte[] iv)
    {
        try
        {
            using (RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                if (keyReg != null)
                {
                    // Speichern der Schlüssel und IV als Binärdaten.

                    keyReg.SetValue("AES_Key_Name", key, RegistryValueKind.Binary);
                    keyReg.SetValue("AES_IV_Name", iv, RegistryValueKind.Binary);
                }
            }
        }
        catch (Exception ex)
        {
            // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
            throw new DatenzugriffExecption($"Fehler beim Speichern von Schlüssel und IV in der Registry: {ex.Message}");
        }
    }

    public static (byte[] Key, byte[] IV) GetKeyAndIV()
    {
        try
        {
            using (RegistryKey keyReg = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (keyReg != null)
                {
                    byte[] key = (byte[])keyReg.GetValue("AES_Key_Name");
                    byte[] iv = (byte[])keyReg.GetValue("AES_IV_Name");

                    if (key != null && iv != null)
                    {
                        return (key, iv);
                    }
                    else
                    {
                        throw new DatenzugriffExecption("Schlüssel oder IV nicht gefunden.");
                    }
                }
                else
                {
                    throw new DatenzugriffExecption("Registry-Schlüssel nicht gefunden.");
                }
            }
        }
        catch (Exception ex)
        {
            // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
            Console.WriteLine($"Fehler beim Abrufen von Schlüssel und IV aus der Registry: {ex.Message}");
            throw; // Weitergeben der Ausnahme
        }
    }
}

