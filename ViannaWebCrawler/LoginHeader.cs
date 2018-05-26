using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViannaWebCrawler
{
    public class LoginHeader : IGetDictionary
    {
        public string UserAgent { get; private set; }
        public string Host { get; private set; }
        public string Origin { get; private set; }
        public string Referer { get; private set; }

        //Uma tentativa de implementar singleton
        private LoginHeader() { }

        private static readonly LoginHeader _instance = new LoginHeader()
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.181 Safari/537.36",
            Host = "aluno.viannajr.edu.br",
            Origin = "http://aluno.viannajr.edu.br",
            Referer = "http://aluno.viannajr.edu.br/"
        };

        public static LoginHeader GetInstance { get => _instance; }

        public Dictionary<string, string> GetAsStringDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"User-Agent", _instance.UserAgent},
                {"Host", _instance.Host},
                {"Origin", _instance.Origin},
                {"Referer", _instance.Referer}
            };
        }
    }
}
