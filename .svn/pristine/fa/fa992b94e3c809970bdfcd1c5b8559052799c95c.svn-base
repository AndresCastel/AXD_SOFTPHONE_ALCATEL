using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Axede.Encryption
{
    public static class Crypto
    {
        // Arbitrary key and iv vector. 
        // You will want to generate (and protect) your own when using encryption.
        private static string actionKey = "EA81AA1D5FC1EC53E84F30AA746139EEBAFF8A9B76638895";
        private static string actionIv = "87AF7EA221F3FFF5";

        private static TripleDESCryptoServiceProvider des3;

        /// <summary>
        /// Default constructor. Initializes the DES3 encryption provider. 
        /// </summary>
        static Crypto()
        {
            des3 = new TripleDESCryptoServiceProvider();
            des3.Mode = CipherMode.CBC;
        }

        /// <summary>
        /// Generates a 24 byte Hex key.
        /// </summary>
        /// <returns>Generated Hex key.</returns>
        public static string GenerateKey()
        {
            // Length is 24
            des3.GenerateKey();
            return BytesToHex(des3.Key);
        }

        /// <summary>
        /// Generates an 8 byte Hex IV (Initialization Vector).
        /// </summary>
        /// <returns>Initialization vector.</returns>
        public static string GenerateIV()
        {
            // Length = 8
            des3.GenerateIV();
            return BytesToHex(des3.IV);
        }

        /// <summary>
        /// Converts a hex string to a byte array.
        /// </summary>
        /// <param name="hex">Hex string.</param>
        /// <returns>Byte array.</returns>
        private static byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                string code = hex.Substring(i * 2, 2);
                bytes[i] = byte.Parse(code, System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }

        /// <summary>
        /// Converts a byte array to a hex string.
        /// </summary>
        /// <param name="bytes">Byte array.</param>
        /// <returns>Converted hex string</returns>
        private static string BytesToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                hex.AppendFormat("{0:X2}", bytes[i]);
            return hex.ToString();
        }

        /// <summary>
        /// Encrypts a memory string (i.e. variable).
        /// </summary>
        /// <param name="data">String to be encrypted.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="iv">Encryption initialization vector.</param>
        /// <returns>Encrypted string.</returns>
        public static string Encrypt(string data, string key, string iv)
        {
            byte[] bdata = Encoding.ASCII.GetBytes(data);
            byte[] bkey = HexToBytes(key);
            byte[] biv = HexToBytes(iv);

            MemoryStream stream = new MemoryStream();
            CryptoStream encStream = new CryptoStream(stream,
                des3.CreateEncryptor(bkey, biv), CryptoStreamMode.Write);

            encStream.Write(bdata, 0, bdata.Length);
            encStream.FlushFinalBlock();
            encStream.Close();

            return BytesToHex(stream.ToArray());
        }

        /// <summary>
        /// Decrypts a memory string (i.e. variable).
        /// </summary>
        /// <param name="data">String to be decrypted.</param>
        /// <param name="key">Original encryption key.</param>
        /// <param name="iv">Original initialization vector.</param>
        /// <returns>Decrypted string.</returns>
        public static string Decrypt(string data, string key, string iv)
        {
            byte[] bdata = HexToBytes(data);
            byte[] bkey = HexToBytes(key);
            byte[] biv = HexToBytes(iv);

            MemoryStream stream = new MemoryStream();
            CryptoStream encStream = new CryptoStream(stream,
                des3.CreateDecryptor(bkey, biv), CryptoStreamMode.Write);

            encStream.Write(bdata, 0, bdata.Length);
            encStream.FlushFinalBlock();
            encStream.Close();

            return Encoding.ASCII.GetString(stream.ToArray());
        }

        /// <summary>
        /// Standard encrypt method for Patterns in Action.
        /// Uses the predefined key and iv.
        /// </summary>
        /// <param name="data">String to be encrypted.</param>
        /// <returns>Encrypted string</returns>
        public static string ActionEncrypt(string data)
        {
            return Encrypt(data, actionKey, actionIv);
        }

        /// <summary>
        /// Standard decrypt method for Patterns in Action.
        /// Uses the predefined key and iv.
        /// </summary>
        /// <param name="data">String to be decrypted.</param>
        /// <returns>Decrypted string.</returns>
        public static string ActionDecrypt(string data)
        {
            return Decrypt(data, actionKey, actionIv);
        }

        /// <summary>
        /// algoritmo de emcripcion MD5
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncriptMD5(string input, string key)
        {
            byte[] keyArray;
            byte[] arrayCadena = UTF32Encoding.UTF8.GetBytes(input);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] arrayResultado = cTransform.TransformFinalBlock(arrayCadena, 0, arrayCadena.Length);

            tdes.Clear();


            return Convert.ToBase64String(arrayResultado, 0, arrayResultado.Length);
        }
        /// <summary>
        /// desencripta la cadena en MD5
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesEncriptMD5(string input, string key)
        {
            byte[] keyArray;
            byte[] arrayCadena = Convert.FromBase64String(input);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(arrayCadena, 0, arrayCadena.Length);
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);

        }
    }
}
