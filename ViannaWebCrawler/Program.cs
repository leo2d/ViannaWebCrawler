using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace ViannaWebCrawler
{


    class Program
    {
        static void Main(string[] args)
        {
            StartCrawlerAsync();


            Console.ReadKey();
        }

        private static async Task StartCrawlerAsync()
        {
            var url = "http://aluno.viannajr.edu.br/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            //    var div =
            //        htmlDocument.DocumentNode.Descendants("div")
            //            .Where(x -> x.GetatributeValue("class", "").Equals("form-control")).ToList();
        }
    }

}
