using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;

namespace ViannaWebCrawler.Controls.LoginControl
{
    public static class LoginManager
    {
        public static string GetUserName(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            string xpath = "//a[contains(@class, 'opcoes-user')]";
            var node = document.DocumentNode.SelectSingleNode(xpath);

            if (node == null)
                throw new NodeNotFoundException($"Could not found node {xpath}");

            string nome = node.InnerText;
            nome = nome.Trim();

            Regex reg = new Regex("\\s{2,}(&nbsp;)#.*$");
            nome = reg.Replace(nome, String.Empty);

            return nome;
        }
    }
}
