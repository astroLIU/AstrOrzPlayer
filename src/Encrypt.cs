using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using System.IO;
namespace AstrOrzPlayer
{
    public class Encrypt
    {
        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        public static string ROT13Encode(string InputText)
        {
            int i;
            char CurrentCharacter;
            int CurrentCharacterCode;
            string EncodedText = "";
            for (i = 0; i < InputText.Length; i++)
            {
                CurrentCharacter = System.Convert.ToChar(InputText.Substring(i, 1));
                CurrentCharacterCode = (int)CurrentCharacter;
                if (CurrentCharacterCode >= 97 && CurrentCharacterCode <= 109)
                {
                    CurrentCharacterCode = CurrentCharacterCode + 13;
                }
                else if (CurrentCharacterCode >= 110 && CurrentCharacterCode <= 122)
                {
                    CurrentCharacterCode = CurrentCharacterCode - 13;
                }
                else if (CurrentCharacterCode >= 65 && CurrentCharacterCode <= 77)
                {
                    CurrentCharacterCode = CurrentCharacterCode + 13;
                }
                else if (CurrentCharacterCode >= 78 && CurrentCharacterCode <= 90)
                {
                    CurrentCharacterCode = CurrentCharacterCode - 13;
                }
                EncodedText = EncodedText + (char)CurrentCharacterCode;
            }
            return EncodedText;
        } 
    }
}
