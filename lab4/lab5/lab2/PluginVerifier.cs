using System;
using System.IO;
using System.Reflection;

namespace lab2
{
    public class PluginVerifier
    {
        private readonly byte[] publicKey;

        public PluginVerifier(string publicKeyFile)
        {
            publicKey = File.ReadAllBytes(publicKeyFile);
        }

        public bool VerifyAssemblySignature(string assemblyFile)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                byte[] assemblyPublicKey = assembly.GetName().GetPublicKey();
                if (!ByteArraysAreEqual(publicKey, assemblyPublicKey))
                {
                    Console.WriteLine("Открытые ключи не совпадают.");
                    return false;
                }

                Console.WriteLine("Подпись сборки действительна.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке подписи сборки: {ex.Message}");
                return false;
            }
        }

        private bool ByteArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
    }
}