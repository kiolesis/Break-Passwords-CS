using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace Password
{
    class Program
    {
        static void Main(string[] args)
        {
            string Hash = "";
            Console.Write("Wpisz MD5 Hash : ");
            Hash = Console.ReadLine().ToUpper();

            if(!string.IsNullOrEmpty(Hash))
            {
                Console.WriteLine("Hash się zgadza, przejdźmy dalej.");
            }
            else
            {
                Console.WriteLine("To nie jest MD5 Hash!");
                goto Here;
            }

            string passwordList = "";
            Console.WriteLine("Wpisz nazwę pliku ze słownikiem haseł:");
            passwordList = Console.ReadLine();

            if(File.Exists(passwordList))
            {
                Console.WriteLine("Znaleziono słownik haseł!");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Nie można znaleźć słownika haseł!");
                Thread.Sleep(2000);
                goto Here;
            }
            
            string Pass = "";
            int counter = 0;
            bool closeLoop = true;

            using (StreamReader file = new StreamReader(passwordList))
            {
            
                while (closeLoop == true && (Pass = file.ReadLine()) != null)
                {

                    if (Md5Hash(Pass) == Hash)
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(Pass);
                        Console.ForegroundColor = ConsoleColor.Green;
                      
                        Console.WriteLine("Cracked Hash = " + Pass + "\n\r" + Md5Hash(Pass));

                        Console.ResetColor();
                        Console.ReadKey();
                        file.Close();
                        closeLoop = false;
                    }
                    else
                    {
                        Console.WriteLine(Pass);
                    }
                    counter++;
                    Console.Title = "Aktualne łamane hasło: " + counter.ToString();
                    Thread.Sleep(10);
                }
                file.Close();
                Console.ReadKey();
             
            }
        }
        
        public static string Md5Hash(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = MD5Provider.ComputeHash(new UTF8Encoding().GetBytes(inputString));

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));

            }
            return sb.ToString();
        }
    }
}
