using System.Collections.Generic;

namespace ViannaWebCrawler.Controls.LoginControl
{
    public class LoginData : IGetDictionary
    {

        public int Id { get; set; }
        public string Password { get; set; }
        public string SubmitValue { get; set; } = "Autenticar";

        public Dictionary<string, string> GetAsStringDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"id", Id.ToString() },
                {"password", Password},
                {"submit", SubmitValue}
            };
        }
    }
}
