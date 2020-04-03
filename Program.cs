using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudFlareUtilities;

namespace DiscordNitroGeneratorBasic
{
    class Program
    {
        public static string oldCode = string.Empty;
        static void Main(string[] args)
        {
            Console.Title = "Discord Nitro Generator";

            string delayy;
            Console.Write("Enter delay in ms. (3000): ");
            delayy = Console.ReadLine(); 
            int delay = Convert.ToInt32(delayy);
            Timer t = new Timer(TimerCallback, null, 0, delay);

            Console.ReadKey();

        }

        private static void TimerCallback(Object o)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            if (oldCode == finalString)
            {



            }

                else
                {
                    string noExist = "{*code*: 10038, *message*: *Unknown Gift Code*}";
                    string noExistFinal = noExist.Replace('*', '"');
                    var handler = new ClearanceHandler();

                    handler.MaxRetries = 3;

                    var client = new HttpClient(handler);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");

                    try
                    {

                        var content = client.GetStringAsync(new Uri("https://discordapp.com/api/v6/entitlements/gift-codes/" + finalString + "?with_application=false&with_subscription_plan=true")).Result;
                        Console.WriteLine(finalString + " / FOUNDED !" + Environment.NewLine);
                       
                   
                    
                        var s = File.Create("good.txt");
                    s.Dispose();
                    
                        File.WriteAllText("good.txt", finalString);
                    }


                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(finalString);
                        Console.ForegroundColor = ConsoleColor.White;
                        if (ex.InnerException.Message.Contains("404"))
                        {
                            Console.WriteLine( " : Invalid Nitro code" + Environment.NewLine);
                        }
                        if (ex.InnerException.Message.Contains("429"))
                        {
                            Console.WriteLine(" : Too many requests / 429" + Environment.NewLine);
                        }
                    
                }
            }
            GC.Collect();
        }
    }
}
