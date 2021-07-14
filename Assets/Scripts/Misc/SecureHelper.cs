using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SecureHelper
{
    public static string Hash(string data) {
        byte[] textToBytes = Encoding.UTF8.GetBytes(data);
        SHA256Managed mySHA256 = new SHA256Managed();

        byte[] hashValue = mySHA256.ComputeHash(textToBytes);

        return GetHexStringFromHash(hashValue);
    }

    public static string GetHexStringFromHash(byte[] hash) {
        string hexString = String.Empty;

        foreach (byte b in hash) {
            hexString += b.ToString("x2");
        }

        return hexString;
    }

    public static string EncryptDecrypt(string data, int key) {
        StringBuilder input = new StringBuilder(data);
        StringBuilder output = new StringBuilder(data.Length);

        char character;

        for (int i = 0; i < data.Length; i++) {
            character = input[i];
            character = (char)(character ^ key);
            output.Append(character);
        }

        return output.ToString();
    }

    public static void SaveHash(string json) {
        string hashValue = SecureHelper.Hash(json);
        PlayerPrefs.SetString("HASH", hashValue);
    }

    public static bool VerifyHash(string json) {
        string defaultValue = "NO_HASH_GENERATED";
        string hashValue = SecureHelper.Hash(json);
        string hashStored = PlayerPrefs.GetString("HASH", defaultValue);

        return hashValue == hashStored || hashStored == defaultValue;
    }
}
