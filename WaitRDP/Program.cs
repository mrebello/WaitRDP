using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitRDP {
    internal class Program {
        static void Main(string[] args) {
            if (args.Length < 2) {
                Console.WriteLine("WaitRDP server port");
                System.Environment.Exit(0);
            }
            string url = "https://" + args[0] + ":" + args[1];

            Tenta(url);
        }

        static async Task Tentando(string url) {
            var c = false;
            while (!c) {
                try {
                    var ct = new CancellationTokenSource();
                    ct.CancelAfter(350);
                    var client = new HttpClient();
                    var r = client.GetAsync(url,ct.Token);
                    r.Wait(300);
                    if (r.IsCompleted) {
                        c = true;
                        Console.WriteLine("Conexão ok.");
                        System.Environment.Exit(0);
                    }
                    
                    //r.Dispose();
                    Console.Write(".");
                }
                catch (Exception e)  {   // when(e.Message.Contains("SSL"))
                    c = true;
                    Console.WriteLine("Conexão ok.");
                    System.Environment.Exit(0);
                }
            }
        }

        static async void Tenta(string url) {
            try {
                await Tentando(url);
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
