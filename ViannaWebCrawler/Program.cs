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

            //Todo: Remover antes de comitar
            LoginRequest loginRequest = new LoginRequest
            (
                new LoginData()
                {
                    Id = 0,
                    Password = ""
                }
            );

            var responseMessage = loginRequest.Login();
            var responseString = responseMessage.Content.ReadAsStringAsync().Result;

            GradebookRequest requestPage = new GradebookRequest(loginRequest.Client);

            var html = requestPage.GradebookPageRequest();

            var gradebook = GetGradebook(html);

            Console.ReadKey();
        }

        private static object GetGradebook(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tableNode = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='table table-striped']");

            return tableNode;
        }
    }

}
