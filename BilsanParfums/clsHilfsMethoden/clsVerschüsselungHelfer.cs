

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Dynamic;
using Microsoft.Win32;

public static class clsVerschlüsselungHelfer
{


    private static readonly string EncryptionKey = "your-encryption-key";

    public static string Encrypt(string plainText)
    {
        byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 16));
        byte[] iv = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 16));

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 16));
        byte[] iv = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 16));
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(buffer))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}

    //####################################################################################

    //using System;
    //using System.Security.Cryptography;
    //using System.Text;
    //using Microsoft.Win32;
    //using System.IO;
    //using System.Globalization;

    //public static class clsVerschlüsselungHelfer
    //{

    //    private static readonly string RegistryPath = @"SOFTWARE\MyApp\Security";
    //    private static readonly string Keystring = "0123456789012345";  // Hardkordierter string.
    //    public static void GenerateAndSaveKeyAndIV()
    //    {

    //        // Aes für Advanced Encryption Standard.
    //        using (Aes aes = Aes.Create())
    //        {
    //            //byte[] Key = aes.Key;  //Die richtige Größe (128, 192 oder 250 Bit) wird automatisch      zugewiesen ;
    //            //byte[] IV = aes.IV; // Die Größe ist immer 128 Bit;

    //            aes.Key = Encoding.UTF8.GetBytes(Keystring);
    //            // Generiere einen zufälligen IV
    //            aes.GenerateIV();

    //            SaveKeyAndIV(aes.Key, aes.IV);
    //        }
    //    }
    //    public static void SaveKeyAndIV(byte[] key, byte[] iv)
    //    {
    //        try
    //        {
    //            using (RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(RegistryPath))
    //            {
    //                if (keyReg != null)
    //                {
    //                    // Speichern der Schlüssel und IV als Binärdaten.

    //                    keyReg.SetValue("AES_Key_Name", key, RegistryValueKind.Binary);
    //                    keyReg.SetValue("AES_IV_Name", iv, RegistryValueKind.Binary);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
    //            throw new Exception ($"Fehler beim Speichern von Schlüssel und IV in der Registry: {ex.Message}");
    //        }
    //    }

    //    public static (byte[] Key, byte[] IV) GetKeyAndIV()
    //    {
    //        try
    //        {
    //            using (RegistryKey keyReg = Registry.CurrentUser.OpenSubKey(RegistryPath))
    //            {
    //                if (keyReg != null)
    //                {
    //                    byte[] key = (byte[])keyReg.GetValue("AES_Key_Name");
    //                    byte[] iv = (byte[])keyReg.GetValue("AES_IV_Name");

    //                    if (key != null && iv != null)
    //                    {
    //                        return (key, iv);
    //                    }
    //                    else
    //                    {
    //                        throw new Exception("Schlüssel oder IV nicht gefunden.");
    //                    }
    //                }
    //                else
    //                {
    //                    throw new Exception("Registry-Schlüssel nicht gefunden.");
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
    //            Console.WriteLine($"Fehler beim Abrufen von Schlüssel und IV aus der Registry: {ex.Message}");
    //            throw; // Weitergeben der Ausnahme
    //        }
    //    }

    //    //private static readonly (byte[] Key, byte[] IV) KeyAndIV = GetKeyAndIV();

    //    public static string EncryptString(string plainText)
    //    {
    //       // GenerateAndSaveKeyAndIV();

    //        (byte[] Key, byte[] IV) KeyAndIV = GetKeyAndIV();

    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            aesAlg.Key = KeyAndIV.Key;
    //            aesAlg.IV = KeyAndIV.IV;

    //            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

    //            using (MemoryStream msEncrypt = new MemoryStream())
    //            {
    //                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //                {
    //                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                    {
    //                        swEncrypt.Write(plainText);
    //                    }
    //                    return Convert.ToBase64String(msEncrypt.ToArray());
    //                }
    //            }
    //        }
    //    }

    //    public static string DecryptString(string cipherText)
    //    {
    //        (byte[] Key, byte[] IV) KeyAndIV = GetKeyAndIV();

    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            aesAlg.Key = KeyAndIV.Key;
    //            aesAlg.IV = KeyAndIV.IV;

    //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

    //            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
    //            {
    //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {
    //                    using (MemoryStream resultStream = new MemoryStream())
    //                    {
    //                        csDecrypt.CopyTo(resultStream);
    //                        return Encoding.UTF8.GetString(resultStream.ToArray());
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}



    // ##############################################################################

    //    private static readonly byte[] _salt = Encoding.ASCII.GetBytes("your-salt-key");

    //    public static string Encrypt(string plainText, string password)
    //    {
    //        byte[] encrypted;

    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            var key = new Rfc2898DeriveBytes(password, _salt);

    //            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
    //            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

    //            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

    //            using (MemoryStream msEncrypt = new MemoryStream())
    //            {
    //                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //                {
    //                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                    {
    //                        swEncrypt.Write(plainText);
    //                    }
    //                    encrypted = msEncrypt.ToArray();
    //                }
    //            }
    //        }

    //        return Convert.ToBase64String(encrypted);
    //    }

    //    public static string Decrypt(string cipherText, string password)
    //    {
    //        string plaintext = null;

    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            var key = new Rfc2898DeriveBytes(password, _salt);

    //            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
    //            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

    //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

    //            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
    //            {
    //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {
    //                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
    //                    {
    //                        plaintext = srDecrypt.ReadToEnd();
    //                    }
    //                }
    //            }
    //        }

    //        return plaintext;
    //    }
    //}

    //private static readonly string RegistryPath = @"SOFTWARE\MyApp\Security";

    //public static void GenerateAndSaveKeyAndIV()
    //{
    //    // hardcoded
    //    string Kaystring = "0123456789012345";
    //    // Aes für Advanced Encryption Standard.
    //    using (Aes aes = Aes.Create())
    //    {
    //        //byte[] Key = aes.Key;  //Die richtige Größe (128, 192 oder 250 Bit) wird automatisch      zugewiesen ;
    //        //byte[] IV = aes.IV; // Die Größe ist immer 128 Bit;


    //        // Generiere einen zufälligen Schlüssel und IV
    //        aes.Key = Encoding.UTF8.GetBytes(Kaystring);
    //        aes.GenerateIV();

    //        SaveKeyAndIV(aes.Key, aes.IV);
    //    }
    //}
    //public static void SaveKeyAndIV(byte[] key, byte[] iv)
    //{
    //    try
    //    {
    //        using (RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(RegistryPath))
    //        {
    //            if (keyReg != null)
    //            {
    //                // Speichern der Schlüssel und IV als Binärdaten.

    //                keyReg.SetValue("AES_Key_Name", key, RegistryValueKind.Binary);
    //                keyReg.SetValue("AES_IV_Name", iv, RegistryValueKind.Binary);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
    //        Console.WriteLine($"Fehler beim Speichern von Schlüssel und IV in der Registry: {ex.Message}");
    //    }
    //}

    //public static (byte[] Key, byte[] IV) GetKeyAndIV()
    //{
    //    try
    //    {
    //        using (RegistryKey keyReg = Registry.CurrentUser.OpenSubKey(RegistryPath))
    //        {
    //            if (keyReg != null)
    //            {
    //                byte[] key = (byte[])keyReg.GetValue("AES_Key_Name");
    //                byte[] iv = (byte[])keyReg.GetValue("AES_IV_Name");

    //                if (key != null && iv != null)
    //                {
    //                    return (key, iv);
    //                }
    //                else
    //                {
    //                    throw new Exception("Schlüssel oder IV nicht gefunden.");
    //                }
    //            }
    //            else
    //            {
    //                throw new Exception("Registry-Schlüssel nicht gefunden.");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Hier sollten Sie Logging oder eine andere Fehlerbehandlung einfügen
    //        Console.WriteLine($"Fehler beim Abrufen von Schlüssel und IV aus der Registry: {ex.Message}");
    //        throw; // Weitergeben der Ausnahme
    //    }
    //}

    //public static string EncryptString(string plainText)
    //{
    //    GenerateAndSaveKeyAndIV();

    //    var keyAndIV = GetKeyAndIV();

    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = keyAndIV.Key;
    //        aesAlg.IV = keyAndIV.IV;
    //        aesAlg.Padding = PaddingMode.PKCS7;

    //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

    //        using (MemoryStream msEncrypt = new MemoryStream())
    //        {
    //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                {
    //                    swEncrypt.Write(plainText);
    //                }
    //                return Convert.ToBase64String(msEncrypt.ToArray());
    //            }
    //        }
    //    }
    //}

    //public static string DecryptString(string cipherText)
    //{
    //    var keyAndIV = GetKeyAndIV();
    //    string KlarText = null;

    //    try
    //    {
    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            aesAlg.Key = keyAndIV.Key;
    //            aesAlg.IV = keyAndIV.IV;
    //            aesAlg.Padding = PaddingMode.PKCS7;


    //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

    //            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
    //            {

    //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {

    //                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
    //                    {
    //                        // Die entschlüsselten Daten als Text lesen
    //                        KlarText =  srDecrypt.ReadToEnd();
    //                    }
    //                }
    //            }
    //        }

    //    }
    //    catch (CryptographicException ex)
    //    {
    //        throw new Exception("Fehler bei der Entschlüsselung: " + ex.Message, ex);
    //    }
    //    return KlarText;
    //}
