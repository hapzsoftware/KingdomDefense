using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class TheEncryptionManager : MonoBehaviour
{
	public static readonly string KEY_FOR_ENCRYPTION = "nhatquanglova12344321";

	public static string EncryptData(string toEncrypt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
		RijndaelManaged rijndaelManaged = TheEncryptionManager.CreateRijndaelManaged();
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
		byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
		return Convert.ToBase64String(array, 0, array.Length);
	}

	public static string DecryptData(string toDecrypt)
	{
		byte[] array = Convert.FromBase64String(toDecrypt);
		RijndaelManaged rijndaelManaged = TheEncryptionManager.CreateRijndaelManaged();
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
		byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		return Encoding.UTF8.GetString(bytes);
	}

	private static RijndaelManaged CreateRijndaelManaged()
	{
		byte[] bytes = Encoding.UTF8.GetBytes(TheEncryptionManager.KEY_FOR_ENCRYPTION);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		byte[] array = new byte[16];
		Array.Copy(bytes, 0, array, 0, 16);
		rijndaelManaged.Key = array;
		rijndaelManaged.Mode = CipherMode.ECB;
		rijndaelManaged.Padding = PaddingMode.PKCS7;
		return rijndaelManaged;
	}
}
